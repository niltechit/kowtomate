using CutOutWiz.Core;
using CutOutWiz.Data.DTOs.Security;
using CutOutWiz.Data.Entities.Security;

namespace CutOutWiz.Data.Repositories.Security
{
	public interface IUserRepository
	{
		Task<Response<bool>> Delete(string objectId);
		Task<List<UserEntity>> GetAll();
		Task<UserEntity> GetById(int userId);
		Task<UserEntity> GetByObjectId(string objectId);
		Task<List<string>> GetRolesByUserObjectId(string userObjectId);
		Task<UserEntity> GetUserByCompanyId(int companyId);
		Task<UserEntity> GetUserByContactId(int contactId);
		Task<UserEntity> GetUserByUsername(string username);
		Task<UserEntity> GetUsername(string username);
		Task<Response<int>> Insert(UserEntity user);
		Task<Response<bool>> ResetPassword(UserDto user);
		Task<Response<bool>> Update(UserEntity user);
		Task<Response<bool>> UserRoleInsertOrUpdateByUserObjectId(string userObjectId, List<string> roleObjectIds, int updatedByContactId);
	}
}