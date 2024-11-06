using CutOutWiz.Core;
using CutOutWiz.Data.DbAccess;
using CutOutWiz.Data.DTOs.Security;
using CutOutWiz.Data.Entities.Security;

namespace CutOutWiz.Data.Repositories.Security
{
	public class UserRepository : IUserRepository
	{
		private readonly ISqlDataAccess _db;
		public UserRepository(ISqlDataAccess db)
		{
			_db = db;
		}

		/// <summary>
		/// Get All Users
		/// </summary>
		/// <returns></returns>
		public async Task<List<UserEntity>> GetAll()
		{
			return await _db.LoadDataUsingProcedure<UserEntity, dynamic>(storedProcedure: "dbo.SP_Security_User_GetAll", new { });
		}

		/// <summary>
		/// Get user by user Id
		/// </summary>
		/// <param name="UserId"></param>
		/// <returns></returns>
		public async Task<UserEntity> GetById(int userId)
		{
			var result = await _db.LoadDataUsingProcedure<UserEntity, dynamic>(storedProcedure: "dbo.SP_Security_User_GetById", new { UserId = userId });
			return result.FirstOrDefault();
		}

		/// <summary>
		/// Get by Object Id
		/// </summary>
		/// <param name="UserId"></param>
		/// <returns></returns>
		public async Task<UserEntity> GetByObjectId(string objectId)
		{
			var result = await _db.LoadDataUsingProcedure<UserEntity, dynamic>(storedProcedure: "dbo.SP_Security_User_GetByObjectId", new { ObjectId = objectId });
			return result.FirstOrDefault();
		}
		public async Task<UserEntity> GetUserByContactId(int contactId)
		{
			var result = await _db.LoadDataUsingProcedure<UserEntity, dynamic>(storedProcedure: "dbo.SP_Security_User_GetByContactId", new { ContactId = contactId });
			return result.FirstOrDefault();
		}
		public async Task<UserEntity> GetUserByCompanyId(int companyId)
		{
			var result = await _db.LoadDataUsingProcedure<UserEntity, dynamic>(storedProcedure: "dbo.SP_Security_User_GetByCompanyId", new { CompanyId = companyId });
			return result.FirstOrDefault();
		}
		/// <summary>
		/// Insert user
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public async Task<Response<int>> Insert(UserEntity user)
		{
			var response = new Response<int>();

			try
			{
				var newId = await _db.SaveDataUsingProcedureAndReturnId<int, dynamic>(storedProcedure: "dbo.SP_Security_User_Insert", new
				{
					user.CompanyId,
					user.ContactId,
					user.Username,
					user.ProfileImageUrl,
					user.PasswordHash,
					user.PasswordSalt,
					user.RegistrationToken,
					user.PasswordResetToken,
					user.LastLoginDateUtc,
					user.LastLogoutDateUtc,
					user.LastPasswordChangeUtc,
					user.Status,
					user.CreatedByContactId,
					user.ObjectId,
				});

				user.Id = (short)newId;
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

		/// <summary>
		/// Update User
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public async Task<Response<bool>> Update(UserEntity user)
		{
			var response = new Response<bool>();

			try
			{
				await _db.SaveDataUsingProcedure(storedProcedure: "dbo.SP_Security_User_Update", new
				{
					user.Id,
					user.CompanyId,
					user.ContactId,
					user.Username,
					user.ProfileImageUrl,
					user.PasswordHash,
					user.PasswordSalt,
					user.RegistrationToken,
					user.PasswordResetToken,
					user.LastLoginDateUtc,
					user.LastLogoutDateUtc,
					user.LastPasswordChangeUtc,
					user.Status,
					user.UpdatedByContactId
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

		public async Task<Response<bool>> ResetPassword(UserDto user)
		{
			var response = new Response<bool>();

			try
			{
				await _db.SaveDataUsingProcedure(storedProcedure: "dbo.SP_Security_User_Password_Reset", new
				{
					user.PasswordHash,
					user.PasswordSalt,
					user.PasswordResetToken,
					user.LastLoginDateUtc,
					user.LastLogoutDateUtc,
					user.LastPasswordChangeUtc,
					user.ObjectId
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
		/// <summary>
		/// Delete User by id
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<Response<bool>> Delete(string objectId)
		{
			var response = new Response<bool>();

			try
			{
				await _db.SaveDataUsingProcedure(storedProcedure: "dbo.SP_Security_User_Delete", new { ObjectId = objectId });
				response.IsSuccess = true;
				response.Message = StandardDataAccessMessages.SuccessMessaage;
			}
			catch (Exception ex)
			{
				response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
			}

			return response;
		}

		public async Task<UserEntity> GetUserByUsername(string username)
		{
			var results = await _db.LoadDataUsingProcedure<UserEntity, dynamic>(storedProcedure: "dbo.SP_Security_User_GetByUsername", new { Username = username });

			return results.FirstOrDefault();
		}

		public async Task<UserEntity> GetUsername(string username)
		{
			var query = $"SELECT * FROM [dbo].[Security_User] WITH(NOLOCK) WHERE Username = @username";
			var result = await _db.LoadDataUsingQuery<UserEntity, dynamic>(query, new { Username = username });
			return result.FirstOrDefault();
		}

		#region User Roles
		/// <summary>
		/// Get roles by user object id
		/// </summary>
		/// <param name="userObjectId"></param>
		/// <returns></returns>
		public async Task<List<string>> GetRolesByUserObjectId(string userObjectId)
		{
			var query = "SELECT RoleObjectId FROM [dbo].[Security_UserRole] p WITH(NOLOCK) WHERE p.UserObjectId = @UserObjectId";

			return await _db.LoadDataUsingQuery<string, dynamic>(query, new { UserObjectId = userObjectId });
		}

		/// <summary>
		/// Update user role
		/// </summary>
		/// <param name="userObjectId"></param>
		/// <param name="roleObjectIds"></param>
		/// <param name="updatedByContactId"></param>
		/// <returns></returns>
		public async Task<Response<bool>> UserRoleInsertOrUpdateByUserObjectId(string userObjectId, List<string> roleObjectIds, int updatedByContactId)
		{
			var response = new Response<bool>();
			try
			{
				await _db.SaveDataUsingProcedure(storedProcedure: "dbo.SP_Security_UserRole_InsertOrUpdateByUserObjectId", new
				{
					UserObjectId = userObjectId,
					RoleObjectIds = string.Join(",", roleObjectIds),
					UpdatedByContactId = updatedByContactId
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
		#endregion
	}
}
