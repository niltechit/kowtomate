using CutOutWiz.Core;
using CutOutWiz.Data.Entities.OrderSOP;

namespace CutOutWiz.Data.Repositories.OrderSOP
{
	public interface IOrderSOPTemplateFileRepository
	{
        Task<Response<int>> Insert(OrderSOPTemplateFileEntity file);
        Task<List<OrderSOPTemplateFileEntity>> GetOrderSopTemplateFilesByOrderSopTemplateId(int SOPTemplateId);
        Task<OrderSOPTemplateFileEntity> GetById(int fileId);
		Task<OrderSOPTemplateFileEntity> GetByOrderSOPTemplateIdAndFileName(int orderSOPTemplateId, string fileName);

	}
}
