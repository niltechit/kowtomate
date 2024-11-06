using CutOutWiz.Core;
using CutOutWiz.Data.Entities.OrderSOP;
using CutOutWiz.Data.Entities.SOP;

namespace CutOutWiz.Data.Repositories.OrderSOP
{
    public interface IOrderSOPStandardServiceRepository
	{
        Task<Response<bool>> Delete(string objectId);
        Task<List<OrderSOPStandardServiceEntity>> GetAll();
        Task<List<OrderSOPStandardServiceEntity>> GetListByOrderTemplateId(int templateId);
        //Task<OrderSOPStandardService> GetById(int standardServiceId);
        Task<SOPStandardServiceEntity> GetByObjectId(string objectId);
        Task<Response<int>> Insert(OrderSOPStandardServiceEntity orderStandardService);
        Task<Response<bool>> Update(SOPStandardServiceEntity standardService);
        Task<List<SOPTemplateServiceEntity>> GetTemplateServiceByTemplateId(int templateId);
		Task<OrderSOPStandardServiceEntity> GetByOrderSOPName(string orderSOPName);
	}
}
