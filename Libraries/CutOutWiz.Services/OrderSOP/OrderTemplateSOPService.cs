using CutOutWiz.Core;
using CutOutWiz.Services.Models.OrderSOP;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Repositories.OrderSOP;
using CutOutWiz.Data.Entities.OrderSOP;

namespace CutOutWiz.Services.OrderSOP
{
    public class OrderTemplateSOPService:IOrderTemplateSOPService
    {
		private readonly IMapperHelperService _mapperHelperService;
		private readonly IOrderTemplateSOPRepository _orderTemplateSOPRepository;

		public OrderTemplateSOPService(IOrderTemplateSOPRepository orderTemplateSOPRepository,
			IMapperHelperService mapperHelperService)
		{
			_orderTemplateSOPRepository = orderTemplateSOPRepository;
			_mapperHelperService = mapperHelperService;
		}

		/// <summary>
		/// Get All Templates
		/// </summary>
		/// <returns></returns>
		public async Task<List<OrderSOPTemplateModel>> GetAllById(int templateId)
        {
			var entities = await _orderTemplateSOPRepository.GetAllById(templateId);
			return await _mapperHelperService.MapToListAsync<OrderSOPTemplateEntity, OrderSOPTemplateModel>(entities);
		}

        /// <summary>
        /// Get template by template Id
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public async Task<OrderSOPTemplateModel> GetById(int templateId)
        {
			var entity = await _orderTemplateSOPRepository.GetById(templateId);
			return await _mapperHelperService.MapToSingleAsync<OrderSOPTemplateEntity, OrderSOPTemplateModel>(entity);
        }

        public async Task<OrderSOPTemplateModel> GetByIdAndIsDeletedFalse(int templateId)
        {
			var entity = await _orderTemplateSOPRepository.GetByIdAndIsDeletedFalse(templateId);
			return await _mapperHelperService.MapToSingleAsync<OrderSOPTemplateEntity, OrderSOPTemplateModel>(entity);
        }

        /// <summary>
        /// Insert template
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public async Task<Response<int>> Insert(OrderSOPTemplateModel template)
        {
			var response = new Core.Response<int>();

			if (template == null)
			{
				response.Message = "OrderSOPTemplate should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<OrderSOPTemplateModel, OrderSOPTemplateEntity>(template);
			return await _orderTemplateSOPRepository.Insert(entity);
        }

        /// <summary>
        /// Delete Template by id
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Delete(OrderSOPTemplateModel orderSOPTemplate)
        {
			var response = new Response<bool>();

			if (orderSOPTemplate == null)
			{
				response.Message = "Id should not be empty.";
				return response;
			}

			var entity = await _mapperHelperService.MapToSingleAsync<OrderSOPTemplateModel, OrderSOPTemplateEntity>(orderSOPTemplate);
			return await _orderTemplateSOPRepository.Delete(entity);
        }

        public async Task<Response<bool>> UpdateSOPTemplateName(OrderSOPTemplateModel orderSOPtemplate)
        {
			var response = new Response<bool>();

			if (orderSOPtemplate == null)
			{
				response.Message = "Id should not be empty.";
				return response;
			}

			var entity = await _mapperHelperService.MapToSingleAsync<OrderSOPTemplateModel, OrderSOPTemplateEntity>(orderSOPtemplate);
			return await _orderTemplateSOPRepository.UpdateSOPTemplateName(entity);
        }

		public async Task<Response<bool>> UpdateOrderSOPTemplateInstruction(OrderSOPTemplateModel orderSOPtemplate)
		{
			var response = new Response<bool>();

			if (orderSOPtemplate == null)
			{
				response.Message = "Id should not be empty.";
				return response;
			}

			var entity = await _mapperHelperService.MapToSingleAsync<OrderSOPTemplateModel, OrderSOPTemplateEntity>(orderSOPtemplate);
			return await _orderTemplateSOPRepository.UpdateOrderSOPTemplateInstruction(entity);
		}
	}
}
