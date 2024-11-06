using CutOutWiz.Core;
using CutOutWiz.Data.Entities.OrderSOP;
using CutOutWiz.Data.DbAccess;

namespace CutOutWiz.Data.Repositories.OrderSOP
{
    public class OrderSOPTemplateServiceRepositoty : IOrderSOPTemplateServiceRepositoty
	{
        private readonly ISqlDataAccess _db;

        public OrderSOPTemplateServiceRepositoty(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<Response<int>> Insert(OrderSOPTemplateServiceEntity orderSOPtemplate)
        {
            var response = new Response<int>();
            try
            {
                var newId = await _db.SaveDataUsingProcedureAndReturnId<short, dynamic>(storedProcedure: "dbo.SP_OrderSOP_TemplateService_Insert", new
                {
                    orderSOPtemplate.OrderSOPTemplateId,
                    orderSOPtemplate.OrderSOPStandardServiceId,
                    orderSOPtemplate.Status,
                    orderSOPtemplate.IsDeleted,
                    orderSOPtemplate.CreatedByContactId,
                    orderSOPtemplate.ObjectId,
                    orderSOPtemplate.BaseTemplateId,
                    orderSOPtemplate.BaseSOPServiceId,
                });

                orderSOPtemplate.Id = newId;
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
		public async Task<OrderSOPStandardServiceEntity> GetByOrderSOPName(string orderSOPName)
		{
			var result = await _db.LoadDataUsingProcedure<OrderSOPStandardServiceEntity, dynamic>(storedProcedure: "dbo.SP_OrderSOP_StandardService_GetByOrderSOPName", new { Name = orderSOPName });
			return result.FirstOrDefault();
		}
	}
}
