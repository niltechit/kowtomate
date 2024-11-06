using CutOutWiz.Core;
using CutOutWiz.Data.DbAccess;
using CutOutWiz.Data.DTOs.HR;
using CutOutWiz.Data.Entities.HR;

namespace CutOutWiz.Data.Repositories.HR
{
	public class EmployeeLeaveRepository : IEmployeeLeaveRepository
	{
		private readonly ISqlDataAccess _db;

		public EmployeeLeaveRepository(ISqlDataAccess db)
		{
			_db = db;
		}

		public Task<Response<bool>> Delete(string objectId)
		{
			throw new NotImplementedException();
		}

		public async Task<List<EmployeeLeaveEntity>> GetAll()
		{
			return await _db.LoadDataUsingProcedure<EmployeeLeaveEntity, dynamic>(storedProcedure: "dbo.sp_hr_employeeLeave_getAll", new { });
		}

		public async Task<List<EmployeeLeaveDto>> GetAllWithDetail()
		{
			return await _db.LoadDataUsingProcedure<EmployeeLeaveDto, dynamic>(storedProcedure: "dbo.sp_hr_employeeLeave_getAll", new { });
		}

		public async Task<EmployeeLeaveEntity> GetById(int id)
		{
			var result = await _db.LoadDataUsingProcedure<EmployeeLeaveEntity, dynamic>(storedProcedure: "dbo.sp_EmployeeLeave_GetById", new { id = id });
			return result.FirstOrDefault();
		}

		public Task<EmployeeLeaveEntity> GetByObjectId(string objectId)
		{
			throw new NotImplementedException();
		}

		public async Task<Response<int>> Insert(EmployeeLeaveEntity entity)
		{
			var response = new Core.Response<int>();
			try
			{
				var newId = await _db.SaveDataUsingProcedureWithGeneric<EmployeeLeaveEntity>(storedProcedure: "dbo.sp_employee_leave_insert", entity, "default");

				entity.Id = newId;
				response.Result = newId;
				response.IsSuccess = true;
				response.Message = StandardDataAccessMessages.SuccessMessaage;

			}
			catch (Exception ex)
			{
				response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
			}

			return response;
		}

		public async Task<Response<bool>> Update(EmployeeLeaveEntity entity)
		{
			var response = new Core.Response<bool>();
			try
			{
				var newId = await _db.UpdateDataUsingProcedureWithGeneric<EmployeeLeaveEntity>(storedProcedure: "dbo.sp_employee_leave_update", entity, "default");

				response.Result = newId;
				response.IsSuccess = true;
				response.Message = StandardDataAccessMessages.SuccessMessaage;

			}
			catch (Exception ex)
			{
				response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
			}

			return response;
		}
	}
}
