using CutOutWiz.Core;
using CutOutWiz.Services.Models.Security;
using CutOutWiz.Data.Repositories.Security;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Entities.Security;
using CutOutWiz.Data.DTOs.Security;

namespace CutOutWiz.Services.Security
{
    public class PermissionService : IPermissionService
    {
		private readonly IMapperHelperService _mapperHelperService;
		private readonly IPermissionRepository _permissionRepository;

		public PermissionService(IPermissionRepository permissionRepository,
			IMapperHelperService mapperHelperService)
		{
			_permissionRepository = permissionRepository;
			_mapperHelperService = mapperHelperService;
		}

		#region Permision
		/// <summary>
		/// Get All Permissions
		/// </summary>
		/// <returns></returns>
		public async Task<List<PermissionModel>> GetAll()
        {
			var entities = await _permissionRepository.GetAll();
			return await _mapperHelperService.MapToListAsync<PermissionEntity, PermissionModel>(entities);
        }

        /// <summary>
        /// Get All Permissions
        /// </summary>
        /// <returns></returns>
        public async Task<List<PermissionListModel>> GetAllWithDetails()
        {
			var entities = await _permissionRepository.GetAllWithDetails();
			return await _mapperHelperService.MapToListAsync<PermissionListDto, PermissionListModel>(entities);
        }

        /// <summary>
        /// Get All Permissions By Permission Ids
        /// </summary>
        /// <returns></returns>
        public async Task<PermissionListModel> GetDetailsByPermisisonId(string permissionObjectId)
        {
			var entity = await _permissionRepository.GetDetailsByPermisisonId(permissionObjectId);
			return await _mapperHelperService.MapToSingleAsync<PermissionListDto, PermissionListModel>(entity);
        }

        /// <summary>
        /// Get permission by permission Id
        /// </summary>
        /// <param name="PermissionId"></param>
        /// <returns></returns>
        public async Task<PermissionModel> GetById(int permissionId)
        {
			var entity = await _permissionRepository.GetById(permissionId);
			return await _mapperHelperService.MapToSingleAsync<PermissionEntity, PermissionModel>(entity);
        }

        /// <summary>
        /// Get by Object Id
        /// </summary>
        /// <param name="PermissionId"></param>
        /// <returns></returns>
        public async Task<PermissionModel> GetByObjectId(string objectId)
        {
			var entity = await _permissionRepository.GetByObjectId(objectId);
			return await _mapperHelperService.MapToSingleAsync<PermissionEntity, PermissionModel>(entity);
        }

        /// <summary>
        /// Insert permission
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<Response<int>> Insert(PermissionModel permission)
        {
			var response = new Core.Response<int>();

			if (permission == null)
			{
				response.Message = "Permission should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<PermissionModel, PermissionEntity>(permission);
			return await _permissionRepository.Insert(entity);
        }

        /// <summary>
        /// Update Permission
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Update(PermissionModel permission)
        {
			var response = new Core.Response<bool>();

			if (permission == null)
			{
				response.Message = "Permission should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<PermissionModel, PermissionEntity>(permission);
			return await _permissionRepository.Update(entity);
		}

        /// <summary>
        /// Delete Permission by id
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

			return await _permissionRepository.Delete(objectId);
		}

        #endregion

        #region User Permission
        /// <summary>
        ///  Get All Permissions by User Id
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<List<PermissionModel>> GetAllByUserId(string userObjectId)
        {
			var entities = await _permissionRepository.GetAllByUserId(userObjectId);
			return await _mapperHelperService.MapToListAsync<PermissionEntity, PermissionModel>(entities);
        }
        #endregion

        #region Module Permissions
        /// <summary>
        /// Get Modules by Permission object Id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<List<string>> GetModulesByPermissionObjectId(string permissionObjectId)
        {
			return await _permissionRepository.GetModulesByPermissionObjectId(permissionObjectId);
		}

        /// <summary>
        /// Insert module permissions by permission id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<Response<bool>> ModulePermissionInsertOrUpdateByPermissionObjectId(string permissionObjectId, List<string> moduleObjectIds, int updatedByContactId)
        {
			var response = new Response<bool>();

			if (string.IsNullOrEmpty(permissionObjectId))
			{
				response.Message = "PermissionObjectId Should not be empty.";
				return response;
			}

            return await _permissionRepository.ModulePermissionInsertOrUpdateByPermissionObjectId(permissionObjectId, moduleObjectIds, updatedByContactId);
        }
        #endregion


        #region Company TYpe Permissions
        /// <summary>
        /// Get Modules by Permission object Id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<List<string>> GetCompanyTypesByPermissionObjectId(string permissionObjectId)
        {
			return await _permissionRepository.GetCompanyTypesByPermissionObjectId(permissionObjectId);
		}

        /// <summary>
        /// Insert module permissions by permission id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<Response<bool>> CompanyTypePermissionInsertOrUpdateByPermissionObjectId(string permissionObjectId, List<string> companyTypeIds, int updatedByContactId)
        {
			var response = new Response<bool>();

			if (string.IsNullOrEmpty(permissionObjectId))
			{
				response.Message = "PermissionObjectId Should not be empty.";
				return response;
			}

			return await _permissionRepository.ModulePermissionInsertOrUpdateByPermissionObjectId(permissionObjectId, companyTypeIds, updatedByContactId);
        }
		#endregion

		public async Task<List<PermissionModel>> GetAllPermissionByRoleObjectId(string roleObjectId)
		{
			var entities = await _permissionRepository.GetAllPermissionByRoleObjectId(roleObjectId);
			return await _mapperHelperService.MapToListAsync<PermissionEntity, PermissionModel>(entities);
		}
	}
}
