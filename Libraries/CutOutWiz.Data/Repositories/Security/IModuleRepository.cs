using CutOutWiz.Core;
using CutOutWiz.Data.DTOs.Security;
using CutOutWiz.Data.Entities.Security;

namespace CutOutWiz.Data.Repositories.Security
{
    public interface IModuleRepository
	{
        Task<Response<bool>> Delete(string objectId);
        Task<List<ModuleEntity>> GetAll();
        /// <summary>
        /// Get All Modules with details
        /// </summary>
        /// <returns></returns>
        Task<List<ModuleEntity>> GetAllWithDetails();
        Task<ModuleEntity> GetById(int moduleId);
        Task<ModuleEntity> GetByObjectId(string objectId);
        Task<Response<int>> Insert(ModuleEntity module);
        Task<Response<bool>> Update(ModuleEntity module);

        #region Module Permissions
        /// <summary>
        /// Get Permissions by Module object Id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task<List<string>> GetPermissionsByModuleObjectId(string moduleObjectId);
        /// <summary>
        /// Insert module permissions by module object id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task<Response<bool>> ModulePermissionInsertOrUpdateByModuleObjectId(string moduleObjectId, List<string> permissionObjectIds, int updatedByContactId);
        /// <summary>
        /// Get All Modules with permissions
        /// </summary>
        /// <returns></returns>
        Task<List<ModulePermissionDto>> GetModuleAllPermissions();
        Task<List<ModulePermissionDto>> GetModuleAllPermissions(byte companyType);
        #endregion
    }
}
