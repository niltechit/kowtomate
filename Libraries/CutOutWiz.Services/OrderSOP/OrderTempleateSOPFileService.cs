using CutOutWiz.Core;
using CutOutWiz.Services.Models.OrderSOP;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Repositories.OrderSOP;
using CutOutWiz.Data.Entities.OrderSOP;

namespace CutOutWiz.Services.OrderSOP
{
	public class OrderTempleateSOPFileService : IOrderTempleateSOPFileService
    {
		private readonly IMapperHelperService _mapperHelperService;
		private readonly IOrderSOPTemplateFileRepository _orderTempleateSOPFileRepository;

		public OrderTempleateSOPFileService(IOrderSOPTemplateFileRepository orderTempleateSOPFileRepository,
			IMapperHelperService mapperHelperService)
		{
			_orderTempleateSOPFileRepository = orderTempleateSOPFileRepository;
			_mapperHelperService = mapperHelperService;
		}

		public async Task<Response<int>> Insert(OrderSOPTemplateFile file)
        {
			var response = new Core.Response<int>();

			if (file == null)
			{
				response.Message = "File should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<OrderSOPTemplateFile, OrderSOPTemplateFileEntity>(file);
			return await _orderTempleateSOPFileRepository.Insert(entity);
        }

        public async Task<List<OrderSOPTemplateFile>> GetOrderSopTemplateFilesByOrderSopTemplateId(int sOPTemplateId)
        {
			var entities = await _orderTempleateSOPFileRepository.GetOrderSopTemplateFilesByOrderSopTemplateId(sOPTemplateId);
			return await _mapperHelperService.MapToListAsync<OrderSOPTemplateFileEntity, OrderSOPTemplateFile>(entities);			
        }

        public async Task<OrderSOPTemplateFile> GetById(int fileId)
        {
			var entity = await _orderTempleateSOPFileRepository.GetById(fileId);
			return await _mapperHelperService.MapToSingleAsync<OrderSOPTemplateFileEntity, OrderSOPTemplateFile>(entity);
        }

        public async Task<OrderSOPTemplateFile> GetByOrderSOPTemplateIdAndFileName(OrderSOPTemplateFile model)
        {
			var entity = await _orderTempleateSOPFileRepository.GetByOrderSOPTemplateIdAndFileName(model.OrderSOPTemplateId, model.FileName);
			return await _mapperHelperService.MapToSingleAsync<OrderSOPTemplateFileEntity, OrderSOPTemplateFile>(entity);
        }
        
    }
}
