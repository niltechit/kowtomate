using CutOutWiz.Core.Utilities;
using CutOutWiz.Core;
using CutOutWiz.Data.Entities.DynamicReport;
using CutOutWiz.Data.DTOs.DynamicReport;

namespace CutOutWiz.Data.Repositories.DynamicReport
{
    public interface IGridViewSetupRepository
	{
        Task<Response<bool>> Delete(string objectId);
        Task<GridViewFilterEntity> GetById(int gridVewFilterId);
        Task<List<GridViewSetupColumnSlimDto>> GetColumnsByGridViewSetupId(int gridViewSetupId);
        Task<List<GridViewFilterEntity>> GetGridViewFiltersBySetupId(int contactId, int gridViewSetupId);
        Task<List<GridViewSetupEntity>> GetListByContactId(Enums.GridViewFor gridViewFor, int contactId);
        Task<Response<bool>> GridViewFilterDelete(int gridViewFilterId);
        Task<Response<int>> GridViewFilterInsert(GridViewFilterEntity gridVewFilter);
        Task<Response<int>> GridViewFilterUpdate(GridViewFilterEntity gridVewFilter);
        Task<Response<int>> Insert(GridViewSetupEntity gridViewSetup);
        Task<Response<bool>> TemplateColumnsInsertOrUpdateBySetupId(int gridViewSetupId, List<GridViewSetupColumnSlimDto> columns);
        Task<Response<int>> Update(GridViewSetupEntity gridViewSetup);

        /// <summary>
        /// Get List by dynamicReportInfoId and contact id
        /// </summary>
        /// <returns></returns>
        Task<List<GridViewSetupEntity>> GetListByDynamicReportIdContactId(int dynamicReportInfoId, int contactId);
    }
}