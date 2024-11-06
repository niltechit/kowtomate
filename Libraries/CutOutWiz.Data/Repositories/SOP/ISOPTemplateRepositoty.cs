using CutOutWiz.Core;
using CutOutWiz.Data.DTOs.SOP;
using CutOutWiz.Data.Entities.SOP;

namespace CutOutWiz.Data.Repositories.SOP
{
    public interface ISOPTemplateRepositoty
	{
        Task<Response<bool>> Delete(string objectId);
        Task<List<SOPTemplateEntity>> GetAll();
        Task<List<SOPTemplateEntity>> GetAllById(int templateId);
        Task<List<SOPTemplateEntity>> GetAllByCompany(int companyId);
        Task<List<SOPTemplateEntity>> GetAllPendingSopByCompany(int companyId);
        Task<SOPTemplateEntity> GetById(int templateId);
        Task<SOPTemplateEntity> GetByObjectId(string objectId);
        Task<SOPTemplateDto> GetByObjectID(string objectId);
        Task<Response<int>> Insert(SOPTemplateEntity template);
        Task<Response<bool>> Update(SOPTemplateEntity template);
        //Task<List<SOPTemplateFileEntity>> GetSopTemplateFilesBySopTemplateId(int SOPTemplateId);
        //Task<List<SOPTemplateFileEntity>> GetSopTemplateFilesByTemplateId(int SOPTemplateId);
        //Task<SOPTemplateFileEntity> GetSopTemplateFilesById(int fileId);
        //Task<Response<bool>> UpdateTemplateFile(string objectId);
    }
}
