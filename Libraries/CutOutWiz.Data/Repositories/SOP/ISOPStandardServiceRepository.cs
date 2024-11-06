using CutOutWiz.Core;
using CutOutWiz.Data.Entities.SOP;

namespace CutOutWiz.Data.Repositories.SOP
{
    public interface ISOPStandardServiceRepository
	{
        Task<Response<bool>> Delete(string objectId);
        Task<List<SOPStandardServiceEntity>> GetAll();
        Task<List<SOPStandardServiceEntity>> GetListByTemplateId(int templateId);
        Task<SOPStandardServiceEntity> GetById(int standardServiceId);
        Task<SOPStandardServiceEntity> GetByObjectId(string objectId);
        Task<Response<int>> Insert(SOPStandardServiceEntity standardService);
        Task<Response<bool>> Update(SOPStandardServiceEntity standardService);
        Task<List<SOPTemplateServiceEntity>> GetTemplateServiceByTemplateId(int templateId);
    }
}
