using CutOutWiz.Core;
using CutOutWiz.Services.Models.UI;
using static CutOutWiz.Core.Utilities.Enums;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Entities.DynamicReport;
using CutOutWiz.Data.DTOs.DynamicReport;
using CutOutWiz.Data.Repositories.DynamicReport;

namespace CutOutWiz.Services.UI
{
    public class GridViewSetupService : IGridViewSetupService
    {
		private readonly IMapperHelperService _mapperHelperService;
		private readonly IGridViewSetupRepository _gridViewSetupRepository;

		public GridViewSetupService(IGridViewSetupRepository gridViewSetupRepository,
			IMapperHelperService mapperHelperService)
		{
			_gridViewSetupRepository = gridViewSetupRepository;
			_mapperHelperService = mapperHelperService;
		}

		/// <summary>
		/// Get List by Contact Id
		/// </summary>
		/// <returns></returns>
		public async Task<List<GridViewSetupModel>> GetListByContactId(GridViewFor gridViewFor, int contactId)
        {
			var entities = await _gridViewSetupRepository.GetListByContactId(gridViewFor, contactId);
			return await _mapperHelperService.MapToListAsync<GridViewSetupEntity, GridViewSetupModel>(entities);
        }

		/// <summary>
		/// Get List by dynamicReportInfoId and contact id
		/// </summary>
		/// <param name="dynamicReportInfoId"></param>
		/// <param name="contactId"></param>
		/// <returns></returns>
		public async Task<List<GridViewSetupModel>> GetListByDynamicReportIdContactId(int dynamicReportInfoId, int contactId)
        {
			var entities = await _gridViewSetupRepository.GetListByDynamicReportIdContactId(dynamicReportInfoId, contactId);
			return await _mapperHelperService.MapToListAsync<GridViewSetupEntity, GridViewSetupModel>(entities);
        }
       
        /// <summary>
        /// Get Columns List By Primary Id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<List<GridViewSetupColumnSlimModel>> GetColumnsByGridViewSetupId(int gridViewSetupId)
        {
			var entities = await _gridViewSetupRepository.GetColumnsByGridViewSetupId(gridViewSetupId);
			return await _mapperHelperService.MapToListAsync<GridViewSetupColumnSlimDto, GridViewSetupColumnSlimModel>(entities);
        }

		/// <summary>
		/// Insert gridViewSetup
		/// </summary>
		/// <param name="gridViewSetupModel"></param>
		/// <returns></returns>
		public async Task<Response<int>> Insert(GridViewSetupModel gridViewSetupModel)
        {
			var response = new Response<int>();

			if (gridViewSetupModel == null)
			{
				response.Message = "GridViewSetup Should not be null.";
				return response;
			}

			var entity = await _mapperHelperService.MapToSingleAsync<GridViewSetupModel, GridViewSetupEntity>(gridViewSetupModel);
			return await _gridViewSetupRepository.Insert(entity);
        }

        /// <summary>
        /// Update GridViewSetup
        /// </summary>
        /// <param name="gridViewSetup"></param>
        /// <returns></returns>
        public async Task<Response<int>> Update(GridViewSetupModel gridViewSetupModel)
        {
			var response = new Response<int>();

			if (gridViewSetupModel == null)
			{
				response.Message = "GridViewSetup Should not be null.";
				return response;
			}

			var entity = await _mapperHelperService.MapToSingleAsync<GridViewSetupModel, GridViewSetupEntity>(gridViewSetupModel);
			return await _gridViewSetupRepository.Update(entity);
		}

		/// <summary>
		/// Delete GridViewSetup by objectId
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

			return await _gridViewSetupRepository.Delete(objectId);
		}

        /// <summary>
        /// Insert module permissions by permission id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<Response<bool>> TemplateColumnsInsertOrUpdateBySetupId(int gridViewSetupId, List<GridViewSetupColumnSlimModel> columns)
        {
            var response = new Response<bool>();
            if (columns == null || !columns.Any())
            {
				return response;
			}

			var columnsDtos = await _mapperHelperService.MapToListAsync<GridViewSetupColumnSlimModel, GridViewSetupColumnSlimDto> (columns);
			return await _gridViewSetupRepository.TemplateColumnsInsertOrUpdateBySetupId(gridViewSetupId, columnsDtos);
        }

        #region Grid View Filter
        /// <summary>
        /// Insert grid View Filter
        /// </summary>
        /// <param name="gridVewFilter"></param>
        /// <returns></returns>
        public async Task<Response<int>> GridViewFilterInsert(GridViewFilterModel gridVewFilter)
        {
			var response = new Core.Response<int>();

			if (gridVewFilter == null)
			{
				response.Message = "GridViewFilter should not be empty.";
				return response;
			}

			var entity = await _mapperHelperService.MapToSingleAsync<GridViewFilterModel, GridViewFilterEntity>(gridVewFilter);
			return await _gridViewSetupRepository.GridViewFilterInsert(entity);
        }

        /// <summary>
        /// Update GridViewSetup
        /// </summary>
        /// <param name="gridViewSetup"></param>
        /// <returns></returns>
        public async Task<Response<int>> GridViewFilterUpdate(GridViewFilterModel gridVewFilter)
        {
			var response = new Core.Response<int>();

			if (gridVewFilter == null)
			{
				response.Message = "GridViewFilter should not be empty.";
				return response;
			}

			var entity = await _mapperHelperService.MapToSingleAsync<GridViewFilterModel, GridViewFilterEntity>(gridVewFilter);
			return await _gridViewSetupRepository.GridViewFilterUpdate(entity);
        }

        /// <summary>
        /// Get gridVewFilter by Id
        /// </summary>
        /// <param name="FileServerId"></param>
        /// <returns></returns>
        public async Task<GridViewFilterModel> GetById(int gridVewFilterId)
        {
			var entity = await _gridViewSetupRepository.GetById(gridVewFilterId);
			return await _mapperHelperService.MapToSingleAsync<GridViewFilterEntity, GridViewFilterModel>(entity);
        }

        /// <summary>
        /// Get GridVewFilter List by gridViewSetupId
        /// </summary>
        /// <returns></returns>
        public async Task<List<GridViewFilterModel>> GetGridViewFiltersBySetupId(int contactId, int gridViewSetupId)
        {
			var entities = await _gridViewSetupRepository.GetGridViewFiltersBySetupId(contactId, gridViewSetupId);
			return await _mapperHelperService.MapToListAsync<GridViewFilterEntity, GridViewFilterModel>(entities);
        }

        /// <summary>
        /// Delete GridViewSetup by id
        /// </summary>
        /// <param name="gridViewSetupId"></param>
        /// <returns></returns>
        public async Task<Response<bool>> GridViewFilterDelete(int gridViewFilterId)
        {
			var response = new Response<bool>();

			if (gridViewFilterId == 0)
			{
				response.Message = "GridViewFilterId should not be empty.";
				return response;
			}

			return await _gridViewSetupRepository.GridViewFilterDelete(gridViewFilterId);
        }

        #endregion
    }
}
