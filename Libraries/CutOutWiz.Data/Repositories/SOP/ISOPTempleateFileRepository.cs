using CutOutWiz.Core;
using CutOutWiz.Data.Entities.SOP;

namespace CutOutWiz.Data.Repositories.SOP
{
	public interface ISOPTempleateFileRepository
	{
		Task<SOPTemplateFileEntity> GetSopTemplateFilesById(int fileId);
		Task<List<SOPTemplateFileEntity>> GetSopTemplateFilesBySopTemplateId(int SOPTemplateId);
		Task<List<SOPTemplateFileEntity>> GetSopTemplateFilesByTemplateId(int SOPTemplateId);
		Task<Response<int>> Insert(SOPTemplateFileEntity file);
		Task<Response<bool>> UpdateTemplateFile(string objectId);
	}
}