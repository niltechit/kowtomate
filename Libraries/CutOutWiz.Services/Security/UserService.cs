using CutOutWiz.Core.Utilities;
using CutOutWiz.Core;
using CutOutWiz.Services.Models.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CutOutWiz.Data.Entities.Security;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.DTOs.Security;
using CutOutWiz.Data.Repositories.Security;

namespace CutOutWiz.Services.Security
{
	public class UserService : IUserService
	{
		private readonly IMapperHelperService _mapperHelperService;
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository,
			IMapperHelperService mapperHelperService)
		{
			_userRepository = userRepository;
			_mapperHelperService = mapperHelperService;
		}

		/// <summary>
		/// Get All Users
		/// </summary>
		/// <returns></returns>
		public async Task<List<UserModel>> GetAll()
		{
			var entities = await _userRepository.GetAll();
			return await _mapperHelperService.MapToListAsync<UserEntity, UserModel>(entities);
		}

		/// <summary>
		/// Get user by user Id
		/// </summary>
		/// <param name="UserId"></param>
		/// <returns></returns>
		public async Task<UserModel> GetById(int userId)
		{
			var entity = await _userRepository.GetById(userId);
			return await _mapperHelperService.MapToSingleAsync<UserEntity, UserModel>(entity);
		}

		/// <summary>
		/// Get by Object Id
		/// </summary>
		/// <param name="UserId"></param>
		/// <returns></returns>
		public async Task<UserModel> GetByObjectId(string objectId)
		{
			var entity = await _userRepository.GetByObjectId(objectId);
			return await _mapperHelperService.MapToSingleAsync<UserEntity, UserModel>(entity);
		}

		public async Task<UserModel> GetUserByContactId(int contactId)
		{
			var entity = await _userRepository.GetUserByContactId(contactId);
			return await _mapperHelperService.MapToSingleAsync<UserEntity, UserModel>(entity);
		}

		/// <summary>
		/// Get User by COmpany Id , TODO: Review this method and use
		/// </summary>
		/// <param name="companyId"></param>
		/// <returns></returns>
		public async Task<UserModel> GetUserByCompanyId(int companyId)
		{
			var entity = await _userRepository.GetUserByCompanyId(companyId);
			return await _mapperHelperService.MapToSingleAsync<UserEntity, UserModel>(entity);
		}

		/// <summary>
		/// Insert user
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public async Task<Response<int>> Insert(UserModel user)
		{
			var response = new Core.Response<int>();

			if (user == null)
			{
				response.Message = "user should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<UserModel, UserEntity>(user);
			return await _userRepository.Insert(entity);
		}

		/// <summary>
		/// Update User
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public async Task<Response<bool>> Update(UserModel user)
		{
			var response = new Response<bool>();

			if (user == null)
			{
				response.Message = "user should not be empty.";
				return response;
			}

			var entity = await _mapperHelperService.MapToSingleAsync<UserModel, UserEntity>(user);
			return await _userRepository.Update(entity);
		}

		public async Task<Response<bool>> ResetPassword(UserViewModel user)
		{
			var entity = await _mapperHelperService.MapToSingleAsync<UserViewModel, UserDto>(user);
			return await _userRepository.ResetPassword(entity);
		}

		/// <summary>
		/// Delete User by objectId
		/// </summary>
		/// <param name="objectId"></param>
		/// <returns></returns>
		public async Task<Response<bool>> Delete(string objectId)
		{
			var response = new Response<bool>();

			if (string.IsNullOrEmpty(objectId))
			{
				response.Message = "ObjectId should not be empty.";
				return response;
			}

			return await _userRepository.Delete(objectId);
		}

		public async Task<UserModel> GetUserByUsername(string username)
		{
			var entity = await _userRepository.GetUserByUsername(username);
			return await _mapperHelperService.MapToSingleAsync<UserEntity, UserModel>(entity);
		}

		/// <summary>
		/// TODO: Possible duplicate method like GetUserByUsername
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		public async Task<UserModel> GetUsername(string username)
		{
			var entity = await _userRepository.GetUsername(username);
			return await _mapperHelperService.MapToSingleAsync<UserEntity, UserModel>(entity);
		}

		/// <summary>
		/// Hash a password using a random salt.
		/// </summary>
		/// <param name="pass"></param>
		/// <param name="salt"></param>
		/// <returns></returns>
		public string GetHashPassword(string pass, string salt)
		{
			var bytes = Encoding.Unicode.GetBytes(pass);
			var src = Encoding.Unicode.GetBytes(salt)
;
			var dst = new byte[src.Length + bytes.Length];
			Buffer.BlockCopy(src, 0, dst, 0, src.Length);
			Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
			var algorithm = HashAlgorithm.Create("SHA1");

			if (algorithm == null)
				return String.Empty;

			var inArray = algorithm.ComputeHash(dst);
			return Convert.ToBase64String(inArray);
		}

		/// <summary>
		/// Creates a random password salt
		/// </summary>
		/// <returns></returns>
		public string CreateRandomSalt()
		{
			var saltBytes = new Byte[4];
			var rng = new RNGCryptoServiceProvider();
			rng.GetBytes(saltBytes);
			return Convert.ToBase64String(saltBytes);
		}

		#region User Roles
		/// <summary>
		/// Get Roles by User object Id
		/// </summary>
		/// <param name="role"></param>
		/// <returns></returns>
		public async Task<List<string>> GetRolesByUserObjectId(string userObjectId)
		{
			return await _userRepository.GetRolesByUserObjectId(userObjectId);
		}

		/// <summary>
		/// Insert user roles by user object id
		/// </summary>
		/// <param name="role"></param>
		/// <returns></returns>
		public async Task<Response<bool>> UserRoleInsertOrUpdateByUserObjectId(string userObjectId, List<string> roleObjectIds, int updatedByContactId)
		{
			var response = new Response<bool>();

			if (string.IsNullOrEmpty(userObjectId))
			{
				response.Message = "UserObjectId Should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			return await _userRepository.UserRoleInsertOrUpdateByUserObjectId(userObjectId, roleObjectIds, updatedByContactId);
		}

		#endregion

		public async Task<Response<UserModel>> ValidateUserPassword(UserViewModel userInfo)
		{
			var response = new Response<UserModel>();

			// Retrieve the user by their ID
			var user = await GetById(userInfo.UserId);

			// Check if the user exists
			if (user == null)
			{
				response.IsSuccess = false;
				response.Message = "User not found.";
				return response;
			}

			// Extract stored password salt and hash
			var storedPasswordSalt = user.PasswordSalt;
			var storedPasswordHash = user.PasswordHash;

			// Verify the provided password against the stored hash and salt
			var isVerified = await VerifyPassword(userInfo.PreviousPassword, storedPasswordHash, storedPasswordSalt);

			if (isVerified)
			{
				response.Result = user;
				response.IsSuccess = true;
			}
			else
			{
				response.IsSuccess = false;
				response.Message = "Password verification failed.";
			}

			return response;
		}

		public Task<bool> VerifyPassword(string providedPassword, string storedPasswordHash, string storedPasswordSalt)
		{
			// Hash the provided password with the stored salt
			var providedPasswordHash = GetHashPassword(providedPassword, storedPasswordSalt);

			// Compare the provided password hash with the stored password hash
			var isMatch = providedPasswordHash == storedPasswordHash;

			// Return a completed Task with the result
			return Task.FromResult(isMatch);
		}

		public async Task<Response<bool>> ChangePassword(UserViewModel userInfo, Claim? claim = null)
		{
			var response = new Response<bool>();

			try
			{
				if (string.IsNullOrWhiteSpace(userInfo.ObjectId))
				{
                    var user = await GetById(userInfo.UserId);
					if (user != null)
					{ 
						userInfo.ObjectId = user.ObjectId;
					}
                    // Check if the user exists
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "User not found.";
                        return response;
                    }
                }
                var salt = CreateRandomSalt();
                var hashedPassword = GetHashPassword(userInfo.Password, salt);
                userInfo.PasswordHash = hashedPassword;
                userInfo.PasswordSalt = salt;

                if (claim!=null && !string.IsNullOrWhiteSpace(claim.Value))
				{
					// Assume ResetPassword method returns a Task<Response<bool>>

					if (claim.Value == PermissionConstants.Security_UserPasswordChangeForAdmin)
					{
						var result = await ResetPassword(userInfo);

						if (result.IsSuccess)
						{
							response.IsSuccess = true;
						}
						else
						{
							response.IsSuccess = false;
							response.Message = "Failed to reset password.";
						}
					}
					else
					{
                        response.IsSuccess = false;
                        response.Message = "You dont have enough permission for changes employee password !";
                    }

                    
					return response;
                }

				var validateUser = await ValidateUserPassword(userInfo);

				if (validateUser.Result != null && validateUser.IsSuccess)
				{
					userInfo.ObjectId = validateUser.Result.ObjectId;

					// Assume ResetPassword method returns a Task<Response<bool>>
					var result = await ResetPassword(userInfo);

					if (result.IsSuccess)
					{
						response.IsSuccess = true;
					}
					else
					{
						response.IsSuccess = false;
						response.Message = "Failed to reset password.";
					}
				}
				else
				{
					response.IsSuccess = false;
					response.Message = "User validation failed.";
				}
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Message = "An error occurred while changing password.";
				// Log the exception for debugging and auditing
				// logger.LogError(ex, "An error occurred while changing password.");
			}

			return response;
		}

	}
}
