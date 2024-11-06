using CutOutWiz.Core;
using CutOutWiz.Data.Entities.OrderSOP;
using CutOutWiz.Data.DbAccess;
using CutOutWiz.Data.Entities.SOP;

namespace CutOutWiz.Data.Repositories.OrderSOP
{
    public class OrderSOPStandardServiceRepository : IOrderSOPStandardServiceRepository
	{
        private readonly ISqlDataAccess _db;

        public OrderSOPStandardServiceRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        /// <summary>
        /// Get All StandardServices
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrderSOPStandardServiceEntity>> GetAll()
        {
            return await _db.LoadDataUsingProcedure<OrderSOPStandardServiceEntity, dynamic>(storedProcedure: "dbo.SP_OrderSOP_StandardService_GetAll", new { });
        }

        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public async Task<List<OrderSOPStandardServiceEntity>> GetListByOrderTemplateId(int templateId)
        {
            return await _db.LoadDataUsingProcedure<OrderSOPStandardServiceEntity, dynamic>(storedProcedure: "dbo.SP_OrderSOP_StandardService_GetListByOrderTemplateId", new { orderTemplateId = templateId });
        }

        ///// <summary>
        ///// Get standardService by standardService Id
        ///// </summary>
        ///// <param name="StandardServiceId"></param>
        ///// <returns></returns>
        //public async Task<SOPStandardServiceEntity> GetById(int standardServiceId)
        //{
        //    var result = await _db.LoadDataUsingProcedure<OrderSOPStandardServiceEntity, dynamic>(storedProcedure: "dbo.SP_SOP_StandardService_GetById", new { StandardServiceId = standardServiceId });
        //    return result.FirstOrDefault();
        //}

        /// <summary>
        /// Get by Object Id
        /// </summary>
        /// <param name="StandardServiceId"></param>
        /// <returns></returns>
        public async Task<SOPStandardServiceEntity> GetByObjectId(string objectId)
        {
            var result = await _db.LoadDataUsingProcedure<SOPStandardServiceEntity, dynamic>(storedProcedure: "dbo.SP_SOP_StandardService_GetByObjectId", new { ObjectId = objectId });
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Insert standardService
        /// </summary>
        /// <param name="standardService"></param>
        /// <returns></returns>
        public async Task<Response<int>> Insert(OrderSOPStandardServiceEntity orderStandardService)
        {
            var response = new Response<int>();
            try
            {
                var newId = await _db.SaveDataUsingProcedureAndReturnId<short, dynamic>(storedProcedure: "dbo.SP_OrderSOP_StandardService_Insert", new
                {
                    orderStandardService.Name,
                    orderStandardService.SortOrder,
                    orderStandardService.Status,
                    orderStandardService.IsDeleted,
                    orderStandardService.CreatedByContactId,
                    orderStandardService.ObjectId,
                    orderStandardService.ParentSopServiceId,
                    orderStandardService.BaseSOPServiceId,
                });

                orderStandardService.Id = newId;
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
        /// Update StandardService
        /// </summary>
        /// <param name="standardService"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Update(SOPStandardServiceEntity standardService)
        {
            var response = new Response<bool>();

            try
            {
                await _db.SaveDataUsingProcedure(storedProcedure: "dbo.SP_SOP_StandardService_Update", new
                {
                    standardService.Id,
                    standardService.Name,
                    standardService.SortOrder,
                    standardService.Status,
                    standardService.IsDeleted,
                    standardService.UpdatedByContactId
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
        /// Delete StandardService by id
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Delete(string objectId)
        {
            var response = new Response<bool>();

            try
            {
                await _db.SaveDataUsingProcedure(storedProcedure: "dbo.SP_SOP_StandardService_Delete", new { ObjectId = objectId });
                response.IsSuccess = true;
                response.Message = StandardDataAccessMessages.SuccessMessaage;
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
            }

            return response;
        }

        public async Task<List<SOPTemplateServiceEntity>> GetTemplateServiceByTemplateId(int SopTemplateId)
        {
            var result = await _db.LoadDataUsingProcedure<SOPTemplateServiceEntity, dynamic>(storedProcedure: "dbo.SP_SOP_TemplateService_TemplateId", new { SopTemplateId = SopTemplateId.ToString() });
            return result.ToList();
        }

		public async Task<OrderSOPStandardServiceEntity> GetByOrderSOPName(string orderSOPName)
		{
			var result = await _db.LoadDataUsingProcedure<OrderSOPStandardServiceEntity, dynamic>(storedProcedure: "dbo.SP_OrderSOP_StandardService_GetByOrderSOPName", new { Name = orderSOPName });
			return result.FirstOrDefault();
		}
	}
}
