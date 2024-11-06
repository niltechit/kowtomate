using CutOutWiz.Core;
using CutOutWiz.Data.Entities.HR;

namespace CutOutWiz.Data.Repositories.HR
{
	public interface IEmployeeProfileRepository
	{
		Task<Response<bool>> Delete(string objectId);
		Task<List<EmployeeProfileEntity>> GetAll();
		Task<EmployeeProfileEntity> GetById(int id);
		Task<EmployeeProfileEntity> GetByObjectId(string objectId);
		Task<Response<int>> Insert(EmployeeProfileEntity entity);
		Task<Response<bool>> Update(EmployeeProfileEntity entity);
	}
}