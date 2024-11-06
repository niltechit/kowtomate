using CutOutWiz.Services.Models.Security;
using CutOutWiz.Core;

namespace CutOutWiz.Services.Security
{
	public interface ISecurityLoginHistoryService
	{
		Task<Response<int>> Insert(SecurityLoginHistoryModel securityLoginHistory);
		Task<Response<int>> Update(SecurityLoginHistoryModel securityLoginHistory);
		Task<List<SecurityLoginHistoryViewModel>> GetAll();
	}
}
