using CutOutWiz.Core;
using CutOutWiz.Data.Entities.HR;

namespace CutOutWiz.Data.Repositories.HR
{
    public interface ILeaveSubTypeRepository
    {
        Task<List<LeaveSubTypeEntity>> GetAll();
        /// <summary>
        /// Get Sub leave type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<LeaveSubTypeEntity> GetById(int id);
        Task<Response<int>> Insert(LeaveSubTypeEntity leaveType);
        Task<Response<bool>> Update(LeaveSubTypeEntity leaveType);
        Task<Response<bool>> Delete(int id);
        /// <summary>
        /// Get List of Sub leave types by leave type id
        /// </summary>
        /// <param name="leaveTypeId"></param>
        /// <returns></returns>
        Task<List<LeaveSubTypeEntity>> GetSubLeaveTypes(int leaveTypeId);
    }
}
