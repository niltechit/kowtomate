using CutOutWiz.Core;
using CutOutWiz.Data.DbAccess;
using CutOutWiz.Data.DTOs.Security;
using CutOutWiz.Data.Entities.Security;

namespace CutOutWiz.Data.Repositories.Security
{
    public class ModuleRepository : IModuleRepository
	{
        private readonly ISqlDataAccess _db;

        public ModuleRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        /// <summary>
        /// Get All Modules
        /// </summary>
        /// <returns></returns>
        public async Task<List<ModuleEntity>> GetAll()
        {
            return await _db.LoadDataUsingProcedure<ModuleEntity, dynamic>(storedProcedure: "dbo.SP_Security_Module_GetAll", new { });
        }

        /// <summary>
        /// Get All Modules with details
        /// </summary>
        /// <returns></returns>
        public async Task<List<ModuleEntity>> GetAllWithDetails()
        {
            return await _db.LoadDataUsingProcedure<ModuleEntity, dynamic>(storedProcedure: "dbo.SP_Security_Module_GetAllWithDetails", new { });
        }
        /// <summary>
        /// Get module by module Id
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public async Task<ModuleEntity> GetById(int moduleId)
        {
            var result = await _db.LoadDataUsingProcedure<ModuleEntity, dynamic>(storedProcedure: "dbo.SP_Security_Module_GetById", new { ModuleId = moduleId });
            return result.FirstOrDefault();
        }
        /// <summary>
        /// Get by Object Id
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public async Task<ModuleEntity> GetByObjectId(string objectId)
        {
            var result = await _db.LoadDataUsingProcedure<ModuleEntity, dynamic>(storedProcedure: "dbo.SP_Security_Module_GetByObjectId", new { ObjectId = objectId });
            return result.FirstOrDefault();
        }
        /// <summary>
        /// Insert module
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public async Task<Response<int>> Insert(ModuleEntity module)
        {
            var response = new Response<int>();

            try
            {
                var newId = await _db.SaveDataUsingProcedureAndReturnId<int, dynamic>(storedProcedure: "dbo.SP_Security_Module_Insert", new
                {
                    module.Name,
                    module.Icon,
                    module.Status,
                    module.CreatedByContactId,
                    module.ObjectId
                });

                module.Id = (short)newId;
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
        /// Update Module
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Update(ModuleEntity module)
        {
            var response = new Response<bool>();

            try
            {
                await _db.SaveDataUsingProcedure(storedProcedure: "dbo.SP_Security_Module_Update", new
                {
                    module.Id,
                    module.Name,
                    module.Icon,
                    module.DisplayOrder,
                    module.Status,
                    module.UpdatedByContactId
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
        /// Delete Module by id
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Delete(string objectId)
        {
            var response = new Response<bool>();

            try
            {
                await _db.SaveDataUsingProcedure(storedProcedure: "dbo.SP_Security_Module_Delete", new { ObjectId = objectId });
                response.IsSuccess = true;
                response.Message = StandardDataAccessMessages.SuccessMessaage;
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
            }

            return response;
        }


        #region Module Permissions
        /// <summary>
        /// Get Permissions by Module object Id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<List<string>> GetPermissionsByModuleObjectId(string moduleObjectId)
        {
            var query = "SELECT PermissionObjectId FROM [dbo].[Security_ModulePermission] p WITH(NOLOCK) WHERE p.ModuleObjectId = @ModuleObjectId";

            return await _db.LoadDataUsingQuery<string, dynamic>(query, new { ModuleObjectId = moduleObjectId });
        }

        /// <summary>
        /// Insert module permissions by module object id
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<Response<bool>> ModulePermissionInsertOrUpdateByModuleObjectId(string moduleObjectId, List<string> permissionObjectIds, int updatedByContactId)
        {
            var response = new Response<bool>();
            try
            {
                await _db.SaveDataUsingProcedure(storedProcedure: "dbo.SP_Security_ModulePermission_InsertOrUpdateByModuleObjectId", new
                {
                    ModuleObjectId = moduleObjectId,
                    PermissionObjectIds = string.Join(",", permissionObjectIds),
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

        /// <summary>
        /// Get All Modules with details
        /// </summary>
        /// <returns></returns>

        public async Task<List<ModulePermissionDto>> GetModuleAllPermissions()
        {
            return await _db.LoadDataUsingProcedure<ModulePermissionDto, dynamic>(storedProcedure: "dbo.SP_Security_ModulePermission_GetAll", new { });
        }

        public async Task<List<ModulePermissionDto>> GetModuleAllPermissions(byte companyType)
        {
            return await _db.LoadDataUsingProcedure<ModulePermissionDto, dynamic>(storedProcedure: "dbo.SP_Security_ModulePermission_GetAll", new { CompanyType = companyType });
        }
        #endregion
    }
}
