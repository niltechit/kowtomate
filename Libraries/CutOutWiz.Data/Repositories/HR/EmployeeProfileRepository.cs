using CutOutWiz.Core;
using CutOutWiz.Data.DbAccess;
using CutOutWiz.Data.Entities.HR;

namespace CutOutWiz.Data.Repositories.HR
{
	public class EmployeeProfileRepository : IEmployeeProfileRepository
	{
		private readonly ISqlDataAccess _db;

		public EmployeeProfileRepository(ISqlDataAccess db)
		{
			_db = db;
		}
		public async Task<Response<bool>> Delete(string objectId)
		{
			var response = new Core.Response<bool>();

			try
			{
				await _db.SaveDataUsingProcedure(storedProcedure: "dbo.sp_hr_employeeProfile_deleteByid", new { id = objectId });
				response.IsSuccess = true;
				response.Message = StandardDataAccessMessages.SuccessMessaage;
			}
			catch (Exception ex)
			{
				response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
			}

			return response;
		}

		//public async Task<List<EmployeeProfile>> GetAll()
		//{
		//	return await _db.LoadDataUsingProcedure<EmployeeProfile, dynamic>(storedProcedure: "dbo.sp_hr_employeeProfile_getAll", new { });
		//}

		public async Task<List<EmployeeProfileEntity>> GetAll()
		{
			return await _db.LoadDataUsingProcedure<EmployeeProfileEntity, dynamic>(storedProcedure: "dbo.sp_hr_employeeProfile_getAll", new { });
		}

		public async Task<EmployeeProfileEntity> GetById(int id)
		{
			var result = await _db.LoadDataUsingProcedure<EmployeeProfileEntity, dynamic>(storedProcedure: "dbo.sp_hr_employeeProfile_getById", new { Id = id });
			return result.FirstOrDefault();
		}

		public async Task<EmployeeProfileEntity> GetByObjectId(string objectId)
		{
			throw new NotImplementedException();
		}

		public async Task<Response<int>> Insert(EmployeeProfileEntity entity)
		{
			var response = new Core.Response<int>();
			try
			{
				var newId = await _db.SaveDataUsingProcedureAndReturnId<short, dynamic>(storedProcedure: "dbo.sp_hr_employeeProfile_insert", new
				{
					entity.ContactId,
					entity.MonthlySalary,
					entity.YearlyBonus,
					entity.ShiftId,
					entity.DayOffMonday,
					entity.DayOffTuesday,
					entity.DayOffWednesday,
					entity.DayOffThursday,
					entity.DayOffFriday,
					entity.DayOffSaturday,
					entity.DayOffSunday,
					entity.Gender,
					entity.DateOfBirth,
					entity.FullAddress,
					entity.HireDate,
				});

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

		public async Task<Response<bool>> Update(EmployeeProfileEntity entity)
		{
			var response = new Core.Response<bool>();

			try
			{
				await _db.SaveDataUsingProcedure(storedProcedure: "dbo.sp_hr_employeeProfile_update", new
				{
					entity.Id,
					entity.ContactId,
					entity.MonthlySalary,
					entity.YearlyBonus,
					entity.ShiftId,
					entity.DayOffMonday,
					entity.DayOffTuesday,
					entity.DayOffWednesday,
					entity.DayOffThursday,
					entity.DayOffFriday,
					entity.DayOffSaturday,
					entity.DayOffSunday,
					entity.Gender,
					entity.DateOfBirth,
					entity.FullAddress,
					entity.HireDate,
				});

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
