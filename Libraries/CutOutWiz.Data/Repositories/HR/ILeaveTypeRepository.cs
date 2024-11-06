using CutOutWiz.Core;
using  CutOutWiz.Data.Entities.HR;

namespace CutOutWiz.Data.Repositories.HR
{
    public interface ILeaveTypeRepository
    {
        Task<List<LeaveTypeEntity>> GetAll();
        Task<LeaveTypeEntity> GetById(int leaveTypeId);
        Task<Response<int>> Insert(LeaveTypeEntity leaveType);
        Task<Response<bool>> Update(LeaveTypeEntity leaveType);
        Task<Response<bool>> Delete(int id);
    }
}
