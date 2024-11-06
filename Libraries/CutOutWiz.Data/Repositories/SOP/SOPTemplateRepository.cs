using CutOutWiz.Core;
using CutOutWiz.Data.DbAccess;
using CutOutWiz.Data.DTOs.SOP;
using CutOutWiz.Data.Entities.SOP;

namespace CutOutWiz.Data.Repositories.SOP
{
    public class SOPTemplateRepository : ISOPTemplateRepositoty
	{
        private readonly ISqlDataAccess _db;

        public SOPTemplateRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        #region SOP Template
        /// <summary>
        /// Get All Templates
        /// </summary>
        /// <returns></returns>
        public async Task<List<SOPTemplateEntity>> GetAll()
        {
            return await _db.LoadDataUsingProcedure<SOPTemplateEntity, dynamic>(storedProcedure: "dbo.SP_SOP_Template_GetAll", new { });
        }
        public async Task<List<SOPTemplateEntity>> GetAllById(int templateId)
        {
            var result = await _db.LoadDataUsingProcedure<SOPTemplateEntity, dynamic>(storedProcedure: "dbo.SP_SOP_Template_GetById", new { TemplateId = templateId });
            return result;
        }
        public async Task<List<SOPTemplateEntity>> GetAllByCompany(int companyId)
        {
            return await _db.LoadDataUsingProcedure<SOPTemplateEntity, dynamic>(storedProcedure: "dbo.[SP_SOP_Template_GetAllByCompanyId]", new { companyId = companyId });
        }
        public async Task<List<SOPTemplateEntity>> GetAllPendingSopByCompany(int companyId)
        {
            return await _db.LoadDataUsingProcedure<SOPTemplateEntity, dynamic>(storedProcedure: "dbo.SP_SOP_Template_GetAllPendingSop", new { companyId = companyId });
        }
        /// <summary>
        /// Get template by template Id
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public async Task<SOPTemplateEntity> GetById(int templateId)
        {
            var result = await _db.LoadDataUsingProcedure<SOPTemplateEntity, dynamic>(storedProcedure: "dbo.SP_SOP_Template_GetById", new { TemplateId = templateId });
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Get by Object Id
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public async Task<SOPTemplateEntity> GetByObjectId(string objectId)
        {
            var result = await _db.LoadDataUsingProcedure<SOPTemplateEntity, dynamic>(storedProcedure: "dbo.SP_SOP_Template_GetByObjectId", new { ObjectId = objectId });
            return result.FirstOrDefault();
        }
        public async Task<SOPTemplateDto> GetByObjectID(string objectId)
        {
            var result = await _db.LoadDataUsingProcedure<SOPTemplateDto, dynamic>(storedProcedure: "dbo.SP_SOP_Template_GetByObjectId", new { ObjectId = objectId });
            return result.FirstOrDefault();
        }
        /// <summary>
        /// Insert template
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public async Task<Response<int>> Insert(SOPTemplateEntity template)
        {
            var response = new Response<int>();
            try
            {
                var newId = await _db.SaveDataUsingProcedureAndReturnId<short, dynamic>(storedProcedure: "dbo.SP_SOP_Template_Insert", new
                {
                    template.Name,
                    template.CompanyId,
                    template.FileServerId,
                    template.Version,
                    template.ParentTemplateId,
                    template.Instruction,
                    template.UnitPrice,
                    template.InstructionModifiedByContactId,
                    template.Status,
                    template.IsDeleted,
                    template.CreatedByContactId,
                    template.ObjectId,
                });

                if (newId == 0)
                {
                    response.Message = StandardDataAccessMessages.ErrorOnAddMessaage;
                    return response;
                }

                template.Id = newId;
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
        /// Update Template
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Update(SOPTemplateEntity template)
        {
            var response = new Response<bool>();

            try
            {
                await _db.SaveDataUsingProcedure(storedProcedure: "dbo.SP_SOP_Template_Update", new
                {
                    template.Id,
                    template.Name,
                    template.CompanyId,
                    template.FileServerId,
                    template.Version,
                    template.ParentTemplateId,
                    template.Instruction,
                    template.UnitPrice,
                    template.InstructionModifiedByContactId,
                    template.Status,
                    template.IsDeleted,
                    template.UpdatedByContactId
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
        /// Delete Template by id
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Delete(string objectId)
        {
            var response = new Response<bool>();

            try
            {
                await _db.SaveDataUsingProcedure(storedProcedure: "dbo.SP_SOP_Template_DeleteByObjectId", new { ObjectId = objectId });
                response.IsSuccess = true;
                response.Message = StandardDataAccessMessages.SuccessMessaage;
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
            }

            return response;
        }

        #endregion Sop Template
    }
}
