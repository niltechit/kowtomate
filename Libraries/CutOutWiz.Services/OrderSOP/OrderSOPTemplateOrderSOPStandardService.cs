using CutOutWiz.Core;
using CutOutWiz.Services.Models.OrderSOP;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Repositories.OrderSOP;
using CutOutWiz.Data.Entities.OrderSOP;

namespace CutOutWiz.Services.OrderSOP
{
    public class OrderSOPTemplateOrderSOPStandardService : IOrderSOPTemplateOrderSOPStandardService
    {
		private readonly IMapperHelperService _mapperHelperService;
		private readonly IOrderSOPTemplateServiceRepositoty _orderSOPTemplateServiceRepository;

		public OrderSOPTemplateOrderSOPStandardService(IOrderSOPTemplateServiceRepositoty orderSOPTemplateServiceRepository,
			IMapperHelperService mapperHelperService)
		{
			_orderSOPTemplateServiceRepository = orderSOPTemplateServiceRepository;
			_mapperHelperService = mapperHelperService;
		}

		public async Task<Response<int>> Insert(OrderSOPTemplateServiceModel orderSOPtemplate)
        {
			var response = new Core.Response<int>();

			if (orderSOPtemplate == null)
			{
				response.Message = "OrderSOPTemplateService should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<OrderSOPTemplateServiceModel, OrderSOPTemplateServiceEntity>(orderSOPtemplate);
			return await _orderSOPTemplateServiceRepository.Insert(entity);
        }

		public async Task<OrderSOPStandardServiceModel> GetByOrderSOPName(string orderSOPName)
		{
			var entity = await _orderSOPTemplateServiceRepository.GetByOrderSOPName(orderSOPName);
			return await _mapperHelperService.MapToSingleAsync<OrderSOPStandardServiceEntity, OrderSOPStandardServiceModel>(entity);
		}
	}
}
