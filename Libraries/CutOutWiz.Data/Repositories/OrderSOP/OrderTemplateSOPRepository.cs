using CutOutWiz.Core;
using CutOutWiz.Data.Entities.OrderSOP;
using CutOutWiz.Data.DbAccess;

namespace CutOutWiz.Data.Repositories.OrderSOP
{
    public class OrderTemplateSOPRepository : IOrderTemplateSOPRepository
	{
        private readonly ISqlDataAccess _db;

        public OrderTemplateSOPRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        /// <summary>
        /// Get All Templates
        /// </summary>
        /// <returns></returns>

        public async Task<List<OrderSOPTemplateEntity>> GetAllById(int templateId)
        {
            var result = await _db.LoadDataUsingProcedure<OrderSOPTemplateEntity, dynamic>(storedProcedure: "dbo.SP_OrderSOP_Template_GetById", new { TemplateId = templateId });
            return result;
        }
        /// <summary>
        /// Get template by template Id
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public async Task<OrderSOPTemplateEntity> GetById(int templateId)
        {
            var result = await _db.LoadDataUsingProcedure<OrderSOPTemplateEntity, dynamic>(storedProcedure: "dbo.SP_OrderSOP_Template_GetById", new { TemplateId = templateId });
            return result.FirstOrDefault();
        }
        public async Task<OrderSOPTemplateEntity> GetByIdAndIsDeletedFalse(int templateId)
        {
            var result = await _db.LoadDataUsingProcedure<OrderSOPTemplateEntity, dynamic>(storedProcedure: "dbo.SP_OrderSOP_Template_GetByIdAndIsDeletedFalse", new { TemplateId = templateId });
            return result.FirstOrDefault();
        }


        /// <summary>
        /// Insert template
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public async Task<Response<int>> Insert(OrderSOPTemplateEntity template)
        {
            var response = new Response<int>();
            try
            {
                var newId = await _db.SaveDataUsingProcedureAndReturnId<short, dynamic>(storedProcedure: "dbo.SP_OrderSOP_Template_Insert", new
                {
                    template.BaseTemplateId,
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
        /// Delete Template by id
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Delete(OrderSOPTemplateEntity orderSOPTemplate)
        {
            var response = new Response<bool>();

            try
            {
                await _db.SaveDataUsingProcedure(storedProcedure: "dbo.SP_OrderSOP_DeleteById", new { Id = orderSOPTemplate.Id,IsDeleted= orderSOPTemplate.IsDeleted });
                response.IsSuccess = true;
                response.Message = StandardDataAccessMessages.SuccessMessaage;
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
            }

            return response;
        }

        public async Task<Response<bool>> UpdateSOPTemplateName(OrderSOPTemplateEntity orderSOPtemplate)
        {
            var response = new Response<bool>();

            try
            {
                await _db.SaveDataUsingProcedure(storedProcedure: "dbo.SP_OrderSOP_Template_UpdateForName", new
                {
                    orderSOPtemplate.Id,
                    orderSOPtemplate.Name,
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

		public async Task<Response<bool>> UpdateOrderSOPTemplateInstruction(OrderSOPTemplateEntity orderSOPtemplate)
		{
			var response = new Response<bool>();

			try
			{
				await _db.SaveDataUsingProcedure(storedProcedure: "dbo.SP_OrderSOP_Template_UpdateForInstruction", new
				{
					orderSOPtemplate.Id,
					orderSOPtemplate.Name,
                    orderSOPtemplate.Instruction,
                    orderSOPtemplate.UpdatedByContactId
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
	}
}
