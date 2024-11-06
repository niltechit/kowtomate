using CutOutWiz.Core;
using CutOutWiz.Services.Models.SOP;
using CutOutWiz.Data;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Repositories.SOP;
using CutOutWiz.Data.Entities.SOP;

namespace CutOutWiz.Services.SOP
{
    public class OrderSOPTemplateService : IOrderSOPTemplateService
    {
		private readonly IMapperHelperService _mapperHelperService;
		private readonly ISOPTemplateRepositoty _orderSOPTemplateRepository;
        private readonly ISOPTempleateFileRepository _sOPTempleateFileRepository;
        private readonly ISOPTemplateServiceRepositoty _sOPTemplateServiceRepositoty;

		public OrderSOPTemplateService(ISOPTemplateRepositoty orderSOPTemplateRepository,
			ISOPTempleateFileRepository sOPTempleateFileRepository,
			ISOPTemplateServiceRepositoty sOPTemplateServiceRepositoty,
			IMapperHelperService mapperHelperService)
		{
			_orderSOPTemplateRepository = orderSOPTemplateRepository;
            _sOPTempleateFileRepository = sOPTempleateFileRepository;
            _sOPTemplateServiceRepositoty = sOPTemplateServiceRepositoty;
			_mapperHelperService = mapperHelperService;
		}

        #region SOP Template
        /// <summary>
        /// Get All Templates
        /// </summary>
        /// <returns></returns>
        public async Task<List<SOPTemplateModel>> GetAll()
        {
			var entities = await _orderSOPTemplateRepository.GetAll();
			return await _mapperHelperService.MapToListAsync<SOPTemplateEntity, SOPTemplateModel>(entities);
        }
        public async Task<List<SOPTemplateModel>> GetAllById(int templateId)
        {
			var entities = await _orderSOPTemplateRepository.GetAllById(templateId);
			return await _mapperHelperService.MapToListAsync<SOPTemplateEntity, SOPTemplateModel>(entities);
		}

        public async Task<List<SOPTemplateModel>> GetAllByCompany(int companyId)
        {
			var entities = await _orderSOPTemplateRepository.GetAllByCompany(companyId);
			return await _mapperHelperService.MapToListAsync<SOPTemplateEntity, SOPTemplateModel>(entities);
        }

        public async Task<List<SOPTemplateModel>> GetAllPendingSopByCompany(int companyId)
        {
			var entities = await _orderSOPTemplateRepository.GetAllPendingSopByCompany(companyId);
			return await _mapperHelperService.MapToListAsync<SOPTemplateEntity, SOPTemplateModel>(entities);
        }

        /// <summary>
        /// Get template by template Id
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public async Task<SOPTemplateModel> GetById(int templateId)
        {
			var entity = await _orderSOPTemplateRepository.GetById(templateId);
			return await _mapperHelperService.MapToSingleAsync<SOPTemplateEntity, SOPTemplateModel>(entity);
        }

        /// <summary>
        /// Get by Object Id
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public async Task<SOPTemplateModel> GetByObjectId(string objectId)
        {
			var entity = await _orderSOPTemplateRepository.GetByObjectId(objectId);
			return await _mapperHelperService.MapToSingleAsync<SOPTemplateEntity, SOPTemplateModel>(entity);
        }

		/// <summary>
		/// TODO: possible duplicate methods with GetByObjectId
		/// </summary>
		/// <param name="objectId"></param>
		/// <returns></returns>
		public async Task<SOPTemplateViewModel> GetByObjectID(string objectId)
        {
			var entity = await _orderSOPTemplateRepository.GetByObjectId(objectId);
			return await _mapperHelperService.MapToSingleAsync<SOPTemplateEntity, SOPTemplateViewModel>(entity);
        }

        /// <summary>
        /// Insert template
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public async Task<Response<int>> Insert(SOPTemplateModel sopTemplate)
        {
			var response = new Response<int>();

			if (sopTemplate == null)
			{
				response.Message = "SOPTemplate should not be empty.";
				return response;
			}

			//TODO: Add more validation here

            try
            {
				var sopTemplateEntity = await _mapperHelperService.MapToSingleAsync<SOPTemplateModel, SOPTemplateEntity>(sopTemplate);
				var sopTemplateInsertResponse = await _orderSOPTemplateRepository.Insert(sopTemplateEntity);

				if (!sopTemplateInsertResponse.IsSuccess)
				{
					return sopTemplateInsertResponse;
				}

				//Add services
				if (sopTemplate.SopTemplateServiceList != null && sopTemplate.SopTemplateServiceList.Any())
                {
                    foreach (var serviceModel in sopTemplate.SopTemplateServiceList)
                    {
                        serviceModel.SOPTemplateId = sopTemplateInsertResponse.Result;

						var serviceEntity = await _mapperHelperService.MapToSingleAsync<SOPTemplateServiceModel, SOPTemplateServiceEntity>(serviceModel);
						await _sOPTemplateServiceRepositoty.Insert(serviceEntity);
					}
                }

                //Add File
                if (sopTemplate.SopTemplateFileList != null && sopTemplate.SopTemplateFileList.Any())
                {
                    await SopFileInsert(sopTemplate.SopTemplateFileList, sopTemplateInsertResponse.Result);
                }

                response.Result = sopTemplateInsertResponse.Result;
                response.IsSuccess = true;
                response.Message = "Successfully Saved.";
            }
            catch (Exception ex)
            {
                response.Message = $"Problem on save data. Exception: {ex.Message}";
            }

            return response;
        }

        /// <summary>
        /// Update Template
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Update(SOPTemplateModel template)
        {
            var response = new Response<bool>();

			if (template == null)
			{
				response.Message = "SOPTemplate should not be empty.";
				return response;
			}

			try
			{

				var sopTemplateEntity = await _mapperHelperService.MapToSingleAsync<SOPTemplateModel, SOPTemplateEntity>(template);
				var sopTemplateInsertResponse = await _orderSOPTemplateRepository.Update(sopTemplateEntity);

				if (!sopTemplateInsertResponse.IsSuccess)
				{
					return sopTemplateInsertResponse;
				}

                //Add services
                if (template.SopTemplateServiceList != null && template.SopTemplateServiceList.Any())
                {
					await _sOPTemplateServiceRepositoty.DeleteBySOPTempalteId(template.Id);

                    foreach (var serviceModel in template.SopTemplateServiceList)
                    {
						serviceModel.SOPTemplateId = template.Id;
						var serviceEntity = await _mapperHelperService.MapToSingleAsync<SOPTemplateServiceModel, SOPTemplateServiceEntity>(serviceModel);
						await _sOPTemplateServiceRepositoty.Insert(serviceEntity);
					}
                }

                //Add File
                if (template.SopTemplateFileList != null && template.SopTemplateFileList.Any())
                {
                    await SopFileInsert(template.SopTemplateFileList, template.Id);
                }

                response.IsSuccess = true;
                response.Message = StandardDataAccessMessages.SuccessMessaage;
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
            }
            return response;
        }

        /// <summary>
        /// Delete Template by id
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Delete(string objectId)
        {
			var response = new Response<bool>();

			if (string.IsNullOrEmpty(objectId))
			{
				response.Message = "ObjectId should not be empty.";
				return response;
			}

			return await _orderSOPTemplateRepository.Delete(objectId);
        }

        #endregion Sop Template

        #region Sop Template File
        private async Task<bool> SopFileInsert(List<SOPTemplateFile> SopTemplateFileList, int sopTemplateId)
        {
            foreach (var file in SopTemplateFileList)
            {
                try
                {
                    file.SOPTemplateId = sopTemplateId;
					var fileEntity = await _mapperHelperService.MapToSingleAsync<SOPTemplateFile, SOPTemplateFileEntity>(file);
					var fileInsertResponse = await _sOPTempleateFileRepository.Insert(fileEntity);
                }
                catch
                {

                }
            }

            return true;

        }

        public async Task<List<SOPTemplateFile>> GetSopTemplateFilesBySopTemplateId(int sOPTemplateId)
        {
			var entities = await _sOPTempleateFileRepository.GetSopTemplateFilesBySopTemplateId(sOPTemplateId);
			return await _mapperHelperService.MapToListAsync<SOPTemplateFileEntity, SOPTemplateFile>(entities);
        }

        /// <summary>
        /// TODO: possible duplcation
        /// </summary>
        /// <param name="sOPTemplateId"></param>
        /// <returns></returns>
        public async Task<List<SOPTemplateFile>> GetSopTemplateFilesByTemplateId(int sOPTemplateId)
        {
			var entities = await _sOPTempleateFileRepository.GetSopTemplateFilesByTemplateId(sOPTemplateId);
			return await _mapperHelperService.MapToListAsync<SOPTemplateFileEntity, SOPTemplateFile>(entities);
        }

        /// <summary>
        /// TODO: need to rename method with proper naming
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public async Task<Response<bool>> UpdateTemplateFile(string objectId)
        {
			var response = new Response<bool>();

			if (string.IsNullOrEmpty(objectId))
			{
				response.Message = "ObjectId should not be empty.";
				return response;
			}

			return await _sOPTempleateFileRepository.UpdateTemplateFile(objectId);
        }

        public async Task<SOPTemplateFile> GetSopTemplateFilesById(int fileId)
        {
			var entity = await _sOPTempleateFileRepository.GetSopTemplateFilesById(fileId);
			return await _mapperHelperService.MapToSingleAsync<SOPTemplateFileEntity, SOPTemplateFile>(entity);
        }

        #endregion
    }
}
