using CutOutWiz.Core;
using CutOutWiz.Services.Models.Security;
using CutOutWiz.Data.Repositories.Security;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Entities.Security;
using CutOutWiz.Data.DTOs.Security;

namespace CutOutWiz.Services.Security
{
    public class ModuleService:IModuleService
    {
		private readonly IMapperHelperService _mapperHelperService;
		private readonly IModuleRepository _moduleRepository;

		public ModuleService(IModuleRepository moduleRepository,
			IMapperHelperService mapperHelperService)
		{
			_moduleRepository = moduleRepository;
			_mapperHelperService = mapperHelperService;
		}

		/// <summary>
		/// Get All Modules
		/// </summary>
		/// <returns></returns>
		public async Task<List<ModuleModel>> GetAll()
        {
			var entities = await _moduleRepository.GetAll();
			return await _mapperHelperService.MapToListAsync<ModuleEntity, ModuleModel>(entities);
        }

        /// <summary>
        /// Get All Modules with details
        /// </summary>
        /// <returns></returns>
        public async Task<List<ModuleModel>> GetAllWithDetails()
        {
			var entities = await _moduleRepository.GetAllWithDetails();
			return await _mapperHelperService.MapToListAsync<ModuleEntity, ModuleModel>(entities);
        }
        /// <summary>
        /// Get module by module Id
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public async Task<ModuleModel> GetById(int moduleId)
        {
			var entity = await _moduleRepository.GetById(moduleId);
			return await _mapperHelperService.MapToSingleAsync<ModuleEntity, ModuleModel>(entity);
        }
        /// <summary>
        /// Get by Object Id
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public async Task<ModuleModel> GetByObjectId(string objectId)
        {
			var entity = await _moduleRepository.GetByObjectId(objectId);
			return await _mapperHelperService.MapToSingleAsync<ModuleEntity, ModuleModel>(entity);
		}
        /// <summary>
        /// Insert module
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public async Task<Response<int>> Insert(ModuleModel module)
        {
			var response = new Core.Response<int>();

			if (module == null)
			{
				response.Message = "Module should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<ModuleModel, ModuleEntity>(module);
			return await _moduleRepository.Insert(entity);
        }

        /// <summary>
        /// Update Module
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Update(ModuleModel module)
        {
			var response = new Response<bool>();

			if (module == null)
			{
				response.Message = "Module should not be empty.";
				return response;
			}

			var entity = await _mapperHelperService.MapToSingleAsync<ModuleModel, ModuleEntity>(module);
			return await _moduleRepository.Update(entity);
        }

        /// <summary>
        /// Delete Module by id
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Delete(string objectId)
        {
			var response = new Response<bool>();

			if (string.IsNullOrEmpty(objectId))
			{
				response.Message = "ObjectId should not be empty.";
				return response;
			}

			return await _moduleRepository.Delete(objectId);
		}

        #region Module Permissions
        /// <summary>
        /// Get Permissions by Module object Id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<List<string>> GetPermissionsByModuleObjectId(string moduleObjectId)
        {
            return await _moduleRepository.GetPermissionsByModuleObjectId(moduleObjectId);
        }

        /// <summary>
        /// Insert module permissions by module object id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<Response<bool>> ModulePermissionInsertOrUpdateByModuleObjectId(string moduleObjectId, List<string> permissionObjectIds, int updatedByContactId)
        {
			var response = new Response<bool>();

			if (string.IsNullOrEmpty(moduleObjectId))
			{
				response.Message = "ModuleObjectId Should not be empty.";
				return response;
			}

			return await _moduleRepository.ModulePermissionInsertOrUpdateByModuleObjectId(moduleObjectId, permissionObjectIds, updatedByContactId);
        }

        /// <summary>
        /// Get All Modules with details
        /// </summary>
        /// <returns></returns>

        public async Task<List<ModulePermissionViewModel>> GetModuleAllPermissions()
        {
			var entities = await _moduleRepository.GetModuleAllPermissions();
			return await _mapperHelperService.MapToListAsync<ModulePermissionDto, ModulePermissionViewModel>(entities);
        }

        public async Task<List<ModulePermissionViewModel>> GetModuleAllPermissions(byte companyType)
        {
			var entities = await _moduleRepository.GetModuleAllPermissions(companyType);
			return await _mapperHelperService.MapToListAsync<ModulePermissionDto, ModulePermissionViewModel>(entities);
        }

        public List<TreeNode> GetTreeNodes(List<ModulePermissionViewModel> permissions)
        {
            var parentNodes = permissions.GroupBy(g => g.ModuleObjectId).Select(s => s.First()).ToList();

            var nodes = parentNodes.Select(n => new TreeNode { NodeType= "Module", Id = n.ModuleObjectId, Name = n.ModuleName }).ToList();

            foreach(var parent in nodes)
            {
                parent.ChildNodes = permissions.Where(f => f.ModuleObjectId == parent.Id).OrderBy(o=>o.DisplayOrder).Select(n => new TreeNode { NodeType = "Permission", Id = n.PermissionObjectId,
                    Name = n.DisplayName }).ToList();
            }

            return nodes;
        }
        #endregion
    }
}
