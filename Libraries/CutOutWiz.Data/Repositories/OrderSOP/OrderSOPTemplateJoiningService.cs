using CutOutWiz.Data.DbAccess;
using CutOutWiz.Data.Entities.OrderSOP;

namespace CutOutWiz.Data.Repositories.OrderSOP
{
	public class OrderSOPTemplateJoiningRepository : IOrderSOPTemplateJoiningRepository
	{
		private readonly ISqlDataAccess _db;

		public OrderSOPTemplateJoiningRepository(ISqlDataAccess db)
		{
			_db = db;
		}
		public async Task<List<Order_ClientOrder_SOP_TemplateEntity>> GetByClientOrderId(int clientOrderId)
		{
			return await _db.LoadDataUsingProcedure<Order_ClientOrder_SOP_TemplateEntity, dynamic>(storedProcedure: "dbo.SP_Order_ClientOrder_OrderSOP_Template_GetByClientOrderId", new { Order_ClientOrder_Id = clientOrderId });
		}
	}
}
