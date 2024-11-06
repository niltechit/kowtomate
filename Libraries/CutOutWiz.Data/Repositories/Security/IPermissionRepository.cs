using CutOutWiz.Core;
using CutOutWiz.Data.DTOs.Security;
using CutOutWiz.Data.Entities.Security;

namespace CutOutWiz.Data.Repositories.Security
{
    public interface IPermissionRepository
	{
        Task<Response<bool>> Delete(string objectId);
        Task<List<PermissionEntity>> GetAll();
        Task<List<PermissionListDto>> GetAllWithDetails();
        /// <summary>
        /// Get All Permissions By Permission Ids
        /// </summary>
        /// <returns></returns>
        Task<PermissionListDto> GetDetailsByPermisisonId(string permissionObjectId);
        Task<PermissionEntity> GetById(int permissionId);
        Task<PermissionEntity> GetByObjectId(string objectId);
        Task<Response<int>> Insert(PermissionEntity permission);
        Task<Response<bool>> Update(PermissionEntity permission);

        /// <summary>
        ///  Get All Permissions by User Id
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        Task<List<PermissionEntity>> GetAllByUserId(string userObjectId);

        #region Module Permissions
        /// <summary>
        /// Get Modules by Permission object Id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task<List<string>> GetModulesByPermissionObjectId(string permissionObjectId);

        /// <summary>
        /// Insert module permissions by permission id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task<Response<bool>> ModulePermissionInsertOrUpdateByPermissionObjectId(string permissionObjectId, List<string> moduleObjectIds, int updatedByContactId);
        #endregion

        #region Company TYpe Permissions
        /// <summary>
        /// Get Modules by Permission object Id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task<List<string>> GetCompanyTypesByPermissionObjectId(string permissionObjectId);

        /// <summary>
        /// Insert module permissions by permission id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task<Response<bool>> CompanyTypePermissionInsertOrUpdateByPermissionObjectId(string permissionObjectId, List<string> companyTypeIds, int updatedByContactId);
        #endregion

        Task<List<PermissionEntity>> GetAllPermissionByRoleObjectId(string roleObjectId);

	}
}
