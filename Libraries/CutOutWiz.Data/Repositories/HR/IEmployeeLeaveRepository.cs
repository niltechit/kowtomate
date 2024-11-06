using CutOutWiz.Core;
using CutOutWiz.Data.DTOs.HR;
using CutOutWiz.Data.Entities.HR;

namespace CutOutWiz.Data.Repositories.HR
{
	public interface IEmployeeLeaveRepository
	{
		Task<Response<bool>> Delete(string objectId);
		Task<List<EmployeeLeaveEntity>> GetAll();
		Task<List<EmployeeLeaveDto>> GetAllWithDetail();
		Task<EmployeeLeaveEntity> GetById(int id);
		Task<EmployeeLeaveEntity> GetByObjectId(string objectId);
		Task<Response<int>> Insert(EmployeeLeaveEntity entity);
		Task<Response<bool>> Update(EmployeeLeaveEntity entity);
	}
}