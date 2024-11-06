using CutOutWiz.Core;
using CutOutWiz.Services.Models.HR;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Repositories.HR;
using CutOutWiz.Data.Entities.HR;

namespace CutOutWiz.Services.HR
{
    public class DesignationService : IDesignationService
    {
		private readonly IMapperHelperService _mapperHelperService;
		private readonly IDesignationRepository _designationRepository;

		public DesignationService(IDesignationRepository designationRepository,
			IMapperHelperService mapperHelperService)
		{
			_designationRepository = designationRepository;
			_mapperHelperService = mapperHelperService;
		}

		/// <summary>
		/// Get All Designations
		/// </summary>
		/// <returns></returns>
		public async Task<List<DesignationModel>> GetAll()
        {
			var entities = await _designationRepository.GetAll();
			return await _mapperHelperService.MapToListAsync<DesignationEntity, DesignationModel>(entities);
        }

        /// <summary>
        /// Get designation by designation Id
        /// </summary>
        /// <param name="DesignationId"></param>
        /// <returns></returns>
        public async Task<DesignationModel> GetById(int designationId)
        {
			var entity = await _designationRepository.GetById(designationId);
			return await _mapperHelperService.MapToSingleAsync<DesignationEntity, DesignationModel>(entity);
        }

        /// <summary>
        /// Get by Object Id
        /// </summary>
        /// <param name="DesignationId"></param>
        /// <returns></returns>
        public async Task<DesignationModel> GetByObjectId(string objectId)
        {
			var entity = await _designationRepository.GetByObjectId(objectId);
			return await _mapperHelperService.MapToSingleAsync<DesignationEntity, DesignationModel>(entity);
		}

        /// <summary>
        /// Insert designation
        /// </summary>
        /// <param name="designation"></param>
        /// <returns></returns>
        public async Task<Response<int>> Insert(DesignationModel designation)
        {
			var response = new Core.Response<int>();

			if (designation == null)
			{
				response.Message = "Designation should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<DesignationModel, DesignationEntity>(designation);
			return await _designationRepository.Insert(entity);
        }

        /// <summary>
        /// Update Designation
        /// </summary>
        /// <param name="designation"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Update(DesignationModel designation)
        {
			var response = new Core.Response<bool>();

			if (designation == null)
			{
				response.Message = "Designation should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<DesignationModel, DesignationEntity>(designation);
			return await _designationRepository.Update(entity);
		}

        /// <summary>
        /// Delete Designation by id
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

			return await _designationRepository.Delete(objectId);
		}

    }
}
