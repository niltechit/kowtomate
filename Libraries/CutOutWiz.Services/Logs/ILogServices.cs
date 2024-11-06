using CutOutWiz.Core;
using CutOutWiz.Services.Models.ClientCategoryServices;

namespace CutOutWiz.Services.Logs
{
    public interface ILogServices
    {
        Task<List<ActivityLogModel>> GetAll();
        Task<List<ActivityLogModel>> GetAllByContactObjectId(string contactObjectId);
        Task<Response<int>> Insert(ActivityLogModel activityLog);
        Task<List<ActivityLogModel>> GetByActivityLogFor(byte activityLogFor,int primaryId);
        Task<Response<int>> ClientCategoryChangeLogInsert(ClientCategoryChangeLogModel clientCategoryChangeLog);
	}
}
