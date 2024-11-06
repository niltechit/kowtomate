using CutOutWiz.Services.Models.OrderSOP;
using CutOutWiz.Core;

namespace CutOutWiz.Services.OrderSOP
{
    public interface IOrderSOPTemplateOrderSOPStandardService
    {
        Task<Response<int>> Insert(OrderSOPTemplateServiceModel orderSOPtemplate);
		Task<OrderSOPStandardServiceModel> GetByOrderSOPName(string orderSOPName);
	}
}
