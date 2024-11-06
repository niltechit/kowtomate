using CutOutWiz.Data.Entities.OrderSOP;
using CutOutWiz.Core;

namespace CutOutWiz.Data.Repositories.OrderSOP
{
    public interface IOrderSOPTemplateServiceRepositoty
	{
        Task<Response<int>> Insert(OrderSOPTemplateServiceEntity orderSOPtemplate);
		Task<OrderSOPStandardServiceEntity> GetByOrderSOPName(string orderSOPName);
	}
}
