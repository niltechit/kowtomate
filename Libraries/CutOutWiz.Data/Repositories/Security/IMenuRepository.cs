using CutOutWiz.Core;
using CutOutWiz.Data.DTOs.Security;
using CutOutWiz.Data.Entities.Security;

namespace CutOutWiz.Data.Repositories.Security
{
    public interface IMenuRepository
	{
        Task<Response<bool>> Delete(string objectId);
        Task<List<MenuEntity>> GetAll();
        Task<List<MenuListDto>> GetListWithDetails();
        /// <summary>
        /// Get Side Menu by Login User Object Id
        /// </summary>
        /// <returns></returns>
        Task<List<SideMenuListDto>> GetSideMenuByUserObjectId(string UserObjectId);
        Task<MenuEntity> GetById(int menuId);
        Task<MenuEntity> GetByObjectId(string objectId);
        Task<Response<int>> Insert(MenuEntity menu);
        Task<Response<bool>> Update(MenuEntity menu);

        #region Menu Permissions
        /// <summary>
        /// Get Permissions by Menu object Id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task<List<string>> GetPermissionsByMenuObjectId(string menuObjectId);

        /// <summary>
        /// Insert menu permissions by menu object id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task<Response<bool>> MenuPermissionInsertOrUpdateByMenuObjectId(string menuObjectId, List<string> permissionObjectIds, int updatedByContactId);
        #endregion
    }
}