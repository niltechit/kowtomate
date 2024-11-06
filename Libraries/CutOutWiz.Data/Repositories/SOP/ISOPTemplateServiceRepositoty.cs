using CutOutWiz.Core;
using CutOutWiz.Data.Entities.SOP;

namespace CutOutWiz.Data.Repositories.SOP
{
	public interface ISOPTemplateServiceRepositoty
	{
		Task<Response<bool>> DeleteBySOPTempalteId(int sopTemplateId);
		Task<Response<int>> Insert(SOPTemplateServiceEntity sOPTemplateServiceEntity);
	}
}