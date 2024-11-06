using CutOutWiz.Core;
using CutOutWiz.Services.Models.Security;
using CutOutWiz.Data.Repositories.Security;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Entities.Security;
using CutOutWiz.Data.DTOs.Security;

namespace CutOutWiz.Services.Security
{
    public class MenuService : IMenuService
    {
		private readonly IMenuRepository _menuRepository;
		private readonly IMapperHelperService _mapperHelperService;

		public MenuService(IMenuRepository menuRepository, IMapperHelperService mapperHelperService)
		{
			_menuRepository = menuRepository;
			_mapperHelperService = mapperHelperService;
		}

		/// <summary>
		/// Get All Menus
		/// </summary>
		/// <returns></returns>
		public async Task<List<MenuModel>> GetAll()
        {
			var entities = await _menuRepository.GetAll();
			return await _mapperHelperService.MapToListAsync<MenuEntity, MenuModel>(entities);
		}

        /// <summary>
        /// Get All Menus
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuListModel>> GetListWithDetails()
        {
			var entities = await _menuRepository.GetListWithDetails();
			return await _mapperHelperService.MapToListAsync<MenuListDto, MenuListModel>(entities);

		}

		/// <summary>
		/// Get Side Menu by User Object Id
		/// </summary>
		/// <param name="userObjectId"></param>
		/// <returns></returns>
		public async Task<List<SideMenuListModel>> GetSideMenuByUserObjectId(string userObjectId)
		{
			var sideMenus = await _menuRepository.GetSideMenuByUserObjectId(userObjectId);
			return await _mapperHelperService.MapToListAsync<SideMenuListDto, SideMenuListModel>(sideMenus);
		}

		/// <summary>
		/// Get Menu by Id
		/// </summary>
		/// <param name="menuId"></param>
		/// <returns></returns>
		public async Task<MenuModel> GetById(int menuId)
		{
			var entity = await _menuRepository.GetById(menuId);
			return await _mapperHelperService.MapToSingleAsync<MenuEntity, MenuModel>(entity);
		}

		/// <summary>
		/// Get Menu by Object Id
		/// </summary>
		/// <param name="objectId"></param>
		/// <returns></returns>
		public async Task<MenuModel> GetByObjectId(string objectId)
		{
			var menuEntity = await _menuRepository.GetByObjectId(objectId);
			return await _mapperHelperService.MapToSingleAsync<MenuEntity, MenuModel>(menuEntity);
		}

        /// <summary>
        /// Insert menu
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public async Task<Response<int>> Insert(MenuModel menu)
        {
			var response = new Core.Response<int>();

			if (menu == null)
			{
				response.Message = "Menu should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<MenuModel, MenuEntity>(menu);
			return await _menuRepository.Insert(entity);
        }

        /// <summary>
        /// Update Menu
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Update(MenuModel menu)
        {
			var response = new Core.Response<bool>();

			if (menu == null)
			{
				response.Message = "Menu should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<MenuModel, MenuEntity>(menu);
			return await _menuRepository.Update(entity);
		}

        /// <summary>
        /// Delete Menu by id
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Delete(string objectId)
        {
			var response = new Response<bool>();

			if (string.IsNullOrEmpty(objectId))
			{
				response.Message = "ObjectId should not be empty.";
				return response;
			}

			return await _menuRepository.Delete(objectId);
        }


        #region Menu Permissions
        /// <summary>
        /// Get Permissions by Menu object Id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<List<string>> GetPermissionsByMenuObjectId(string menuObjectId)
        {
			return await _menuRepository.GetPermissionsByMenuObjectId(menuObjectId);
		}

        /// <summary>
        /// Insert menu permissions by menu object id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<Response<bool>> MenuPermissionInsertOrUpdateByMenuObjectId(string menuObjectId, List<string> permissionObjectIds, int updatedByContactId)
        {
            var response = new Response<bool>();

			if (string.IsNullOrEmpty(menuObjectId))
			{
				response.Message = "MenuObjectId is required.";
				return response;
			}

            return await _menuRepository.MenuPermissionInsertOrUpdateByMenuObjectId(menuObjectId, permissionObjectIds, updatedByContactId);
        }
        #endregion

    }
}
