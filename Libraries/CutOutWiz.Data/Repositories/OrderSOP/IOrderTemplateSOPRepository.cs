using CutOutWiz.Core;
using CutOutWiz.Data.Entities.OrderSOP;

namespace CutOutWiz.Data.Repositories.OrderSOP
{
    public interface IOrderTemplateSOPRepository
	{
        Task<Response<bool>> Delete(OrderSOPTemplateEntity orderSOPTemplate);
        Task<List<OrderSOPTemplateEntity>> GetAllById(int templateId);
        Task<OrderSOPTemplateEntity> GetById(int templateId);
        Task<OrderSOPTemplateEntity> GetByIdAndIsDeletedFalse(int templateId);
        Task<Response<int>> Insert(OrderSOPTemplateEntity orderSOPtemplate);
        Task<Response<bool>> UpdateSOPTemplateName(OrderSOPTemplateEntity orderSOPtemplate);
        Task<Response<bool>> UpdateOrderSOPTemplateInstruction(OrderSOPTemplateEntity orderSOPtemplate);
    }
}
