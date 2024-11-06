
using CutOutWiz.Data.Entities.OrderSOP;

namespace CutOutWiz.Data.Repositories.OrderSOP
{
	public interface IOrderSOPTemplateJoiningRepository
	{
		Task<List<Order_ClientOrder_SOP_TemplateEntity>> GetByClientOrderId(int clientOrderId);
	}
}
