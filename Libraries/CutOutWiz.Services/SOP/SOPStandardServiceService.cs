using CutOutWiz.Core;
using CutOutWiz.Services.Models.SOP;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Repositories.SOP;
using CutOutWiz.Data.Entities.SOP;

namespace CutOutWiz.Services.SOP
{
    public class OrderSOPStandardServiceService : IOrderSOPStandardServiceService
    {
		private readonly IMapperHelperService _mapperHelperService;
		private readonly ISOPStandardServiceRepository _sOPStandardServiceRepositoty;

		public OrderSOPStandardServiceService(ISOPStandardServiceRepository sOPStandardServiceRepositoty,
			IMapperHelperService mapperHelperService)
		{
			_sOPStandardServiceRepositoty = sOPStandardServiceRepositoty;
			_mapperHelperService = mapperHelperService;
		}

        /// <summary>
        /// Get All StandardServices
        /// </summary>
        /// <returns></returns>
        public async Task<List<SOPStandardServiceModel>> GetAll()
        {
			var entities = await _sOPStandardServiceRepositoty.GetAll();
			return await _mapperHelperService.MapToListAsync<SOPStandardServiceEntity, SOPStandardServiceModel>(entities);
        }

        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public async Task<List<SOPStandardServiceModel>> GetListByTemplateId(int templateId)
        {
			var entities = await _sOPStandardServiceRepositoty.GetListByTemplateId(templateId);
			return await _mapperHelperService.MapToListAsync<SOPStandardServiceEntity, SOPStandardServiceModel>(entities);
        }

        /// <summary>
        /// Get standardService by standardService Id
        /// </summary>
        /// <param name="StandardServiceId"></param>
        /// <returns></returns>
        public async Task<SOPStandardServiceModel> GetById(int standardServiceId)
        {
			var entity = await _sOPStandardServiceRepositoty.GetById(standardServiceId);
			return await _mapperHelperService.MapToSingleAsync<SOPStandardServiceEntity, SOPStandardServiceModel>(entity);
        }

        /// <summary>
        /// Get by Object Id
        /// </summary>
        /// <param name="StandardServiceId"></param>
        /// <returns></returns>
        public async Task<SOPStandardServiceModel> GetByObjectId(string objectId)
        {
			var entity = await _sOPStandardServiceRepositoty.GetByObjectId(objectId);
			return await _mapperHelperService.MapToSingleAsync<SOPStandardServiceEntity, SOPStandardServiceModel>(entity);
        }

        /// <summary>
        /// Insert standardService
        /// </summary>
        /// <param name="standardService"></param>
        /// <returns></returns>
        public async Task<Response<int>> Insert(SOPStandardServiceModel standardService)
        {
			var response = new Core.Response<int>();

			if (standardService == null)
			{
				response.Message = "StandardService should not be empty.";
				return response;
			}

			//TODO: Add more validation here
			var entity = await _mapperHelperService.MapToSingleAsync<SOPStandardServiceModel, SOPStandardServiceEntity>(standardService);
			return await _sOPStandardServiceRepositoty.Insert(entity);
        }

        /// <summary>
        /// Update StandardService
        /// </summary>
        /// <param name="standardService"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Update(SOPStandardServiceModel standardService)
        {
			var response = new Response<bool>();

			if (standardService == null)
			{
				response.Message = "StandardService should not be empty.";
				return response;
			}

			//TODO: Add more validation here
			var entity = await _mapperHelperService.MapToSingleAsync<SOPStandardServiceModel, SOPStandardServiceEntity>(standardService);
			return await _sOPStandardServiceRepositoty.Update(entity);
        }

        /// <summary>
        /// Delete StandardService by id
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

			return await _sOPStandardServiceRepositoty.Delete(objectId);
        }

        public async Task<List<SOPTemplateServiceModel>> GetTemplateServiceByTemplateId(int sopTemplateId)
        {
			var entities = await _sOPStandardServiceRepositoty.GetTemplateServiceByTemplateId(sopTemplateId);
			return await _mapperHelperService.MapToListAsync<SOPTemplateServiceEntity, SOPTemplateServiceModel>(entities);
        }
    }
}
