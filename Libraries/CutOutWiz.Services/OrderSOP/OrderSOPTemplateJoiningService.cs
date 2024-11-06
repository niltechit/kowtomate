using CutOutWiz.Data.Entities.OrderSOP;
using CutOutWiz.Data.Repositories.OrderSOP;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Services.Models.OrderSOP;

namespace CutOutWiz.Services.OrderSOP
{
	public class OrderSOPTemplateJoiningService : IOrderSOPTemplateJoiningService
	{
		private readonly IMapperHelperService _mapperHelperService;
		private readonly IOrderSOPTemplateJoiningRepository _orderSOPTemplateJoiningRepository;

		public OrderSOPTemplateJoiningService(IOrderSOPTemplateJoiningRepository orderSOPTemplateJoiningRepository,
			IMapperHelperService mapperHelperService)
		{
			_orderSOPTemplateJoiningRepository = orderSOPTemplateJoiningRepository;
			_mapperHelperService = mapperHelperService;
		}

		public async Task<List<Order_ClientOrder_SOP_TemplateModel>> GetByClientOrderId(int clientOrderId)
		{
			var entities = await _orderSOPTemplateJoiningRepository.GetByClientOrderId(clientOrderId);
			return await _mapperHelperService.MapToListAsync<Order_ClientOrder_SOP_TemplateEntity, Order_ClientOrder_SOP_TemplateModel>(entities);
		}
	}
}
