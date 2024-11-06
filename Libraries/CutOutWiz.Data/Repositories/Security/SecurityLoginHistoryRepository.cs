using CutOutWiz.Core;
using CutOutWiz.Data.DbAccess;
using CutOutWiz.Data.Entities.Security;

namespace CutOutWiz.Data.Repositories.Security
{
	public class SecurityLoginHistoryRepository : ISecurityLoginHistoryRepository
	{
		private readonly ISqlDataAccess _db;

		public SecurityLoginHistoryRepository(ISqlDataAccess db)
		{
			_db = db;
		}

		public async Task<List<SecurityLoginHistoryEntity>> GetAll()
		{
            var result= await _db.LoadDataUsingProcedure<SecurityLoginHistoryEntity, dynamic>(storedProcedure: "dbo.SP_Security_GetSecurityLoginHistories", new { });
			return result.ToList();
        }

		public async Task<Response<int>> Insert(SecurityLoginHistoryEntity securityLoginHistory)
		{
			var response = new Response<int>();

			try
			{
				var newId = await _db.SaveDataUsingProcedureAndReturnId<int, dynamic>(storedProcedure: "dbo.SP_Security_InsertSecurityLoginHistory", new
				{
					securityLoginHistory.UserId,
					securityLoginHistory.Username,
					securityLoginHistory.ActionTime,
					securityLoginHistory.ActionType,
					securityLoginHistory.IPAddress,
					securityLoginHistory.DeviceInfo,
					securityLoginHistory.Status

				});

				securityLoginHistory.Id = (short)newId;
				response.Result = securityLoginHistory.Id;
				response.IsSuccess = true;
				response.Message = StandardDataAccessMessages.SuccessMessaage;

			}
			catch (Exception ex)
			{
				response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
			}

			return response;

		}

        public async Task<Response<int>> Update(SecurityLoginHistoryEntity securityLoginHistory)
        {
            var response = new Response<int>();

            try
            {
                var newId = await _db.SaveDataUsingProcedureAndReturnId<int, dynamic>(storedProcedure: "dbo.SP_Security_UpdateSecurityLoginHistoryById", new
                {
                    securityLoginHistory.Id,
                    securityLoginHistory.UserId,
                    securityLoginHistory.Username,
                    securityLoginHistory.ActionTime,
                    securityLoginHistory.ActionType,
                    securityLoginHistory.IPAddress,
                    securityLoginHistory.DeviceInfo,
                    securityLoginHistory.Status

                });

				response.Result = securityLoginHistory.Id;
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
