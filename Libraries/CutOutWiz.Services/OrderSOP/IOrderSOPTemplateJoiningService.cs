using CutOutWiz.Services.Models.OrderSOP;

namespace CutOutWiz.Services.OrderSOP
{
	public interface IOrderSOPTemplateJoiningService
	{
		Task<List<Order_ClientOrder_SOP_TemplateModel>> GetByClientOrderId(int clientOrderId);
	}
}
