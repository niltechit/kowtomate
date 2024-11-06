using CutOutWiz.Core;
using CutOutWiz.Services.Models.OrderSOP;
using CutOutWiz.Services.Models.SOP;
using CutOutWiz.Data;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Repositories.OrderSOP;
using CutOutWiz.Data.Entities.Security;
using CutOutWiz.Services.Models.Security;
using CutOutWiz.Data.Entities.OrderSOP;
using CutOutWiz.Data.Entities.SOP;

namespace CutOutWiz.Services.OrderSOP
{
    public class OrderSOPStandardService : IOrderSOPStandardService
    {
		private readonly IMapperHelperService _mapperHelperService;
		private readonly IOrderSOPStandardServiceRepository _orderSOPStandardRepository;

		public OrderSOPStandardService(IOrderSOPStandardServiceRepository orderSOPStandardRepository,
			IMapperHelperService mapperHelperService)
		{
			_orderSOPStandardRepository = orderSOPStandardRepository;
			_mapperHelperService = mapperHelperService;
		}

		/// <summary>
		/// Get All StandardServices
		/// </summary>
		/// <returns></returns>
		public async Task<List<OrderSOPStandardServiceModel>> GetAll()
        {
			var entities = await _orderSOPStandardRepository.GetAll();
			return await _mapperHelperService.MapToListAsync<OrderSOPStandardServiceEntity, OrderSOPStandardServiceModel>(entities);
        }

        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public async Task<List<OrderSOPStandardServiceModel>> GetListByOrderTemplateId(int templateId)
        {
			var entities = await _orderSOPStandardRepository.GetListByOrderTemplateId(templateId);
			return await _mapperHelperService.MapToListAsync<OrderSOPStandardServiceEntity, OrderSOPStandardServiceModel>(entities);
        }

   //     /// <summary>
   //     /// Get standardService by standardService Id
   //     /// </summary>
   //     /// <param name="StandardServiceId"></param>
   //     /// <returns></returns>
   //     public async Task<OrderSOPStandardServiceModel> GetById(int standardServiceId)
   //     {
			//var entity = await _orderSOPStandardRepository.GetById(standardServiceId);
			//return await _mapperHelperService.MapToSingleAsync<UserEntity, UserModel>(entity);

			//var result = await _db.LoadDataUsingProcedure<OrderSOPStandardService, dynamic>(storedProcedure: "dbo.SP_SOP_StandardService_GetById", new { StandardServiceId = standardServiceId });
   //         return result.FirstOrDefault();
   //     }

        /// <summary>
        /// Get by Object Id
        /// </summary>
        /// <param name="StandardServiceId"></param>
        /// <returns></returns>
        public async Task<SOPStandardServiceModel> GetByObjectId(string objectId)
        {
			var entity = await _orderSOPStandardRepository.GetByObjectId(objectId);
			return await _mapperHelperService.MapToSingleAsync<SOPStandardServiceEntity, SOPStandardServiceModel>(entity);
        }

        /// <summary>
        /// Insert standardService
        /// </summary>
        /// <param name="standardService"></param>
        /// <returns></returns>
        public async Task<Response<int>> Insert(OrderSOPStandardServiceModel orderStandardService)
        {
			var response = new Response<int>();

			if (orderStandardService == null)
			{
				response.Message = "OrderSOPStandardService should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<OrderSOPStandardServiceModel, OrderSOPStandardServiceEntity>(orderStandardService);
			return await _orderSOPStandardRepository.Insert(entity);
        }

        /// <summary>
        /// Update StandardService
        /// </summary>
        /// <param name="standardService"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Update(SOPStandardServiceModel standardService)
        {
			var response = new Response<bool>();

			if (standardService == null)
			{
				response.Message = "OrderSOPStandardService should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<SOPStandardServiceModel, SOPStandardServiceEntity>(standardService);
			return await _orderSOPStandardRepository.Update(entity);
		}

        /// <summary>
        /// Delete StandardService by id
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Delete(string objectId)
        {
			var response = new Response<bool>();

			if (string.IsNullOrEmpty(objectId))
			{
				response.Message = "ObjectId should not be empty.";
				return response;
			}

			return await _orderSOPStandardRepository.Delete(objectId);
        }

        public async Task<List<SOPTemplateServiceModel>> GetTemplateServiceByTemplateId(int sopTemplateId)
        {
			var entities = await _orderSOPStandardRepository.GetTemplateServiceByTemplateId(sopTemplateId);
			return await _mapperHelperService.MapToListAsync<SOPTemplateServiceEntity, SOPTemplateServiceModel>(entities);
        }

		public async Task<OrderSOPStandardServiceModel> GetByOrderSOPName(string orderSOPName)
		{
			var entity = await _orderSOPStandardRepository.GetByOrderSOPName(orderSOPName);
			return await _mapperHelperService.MapToSingleAsync<OrderSOPStandardServiceEntity, OrderSOPStandardServiceModel>(entity);
		}

	}
}
