using CutOutWiz.Core;
using CutOutWiz.Data.Entities.Security;

namespace CutOutWiz.Data.Repositories.Security
{
	public interface ISecurityLoginHistoryRepository
	{
		Task<Response<int>> Insert(SecurityLoginHistoryEntity securityLoginHistory);
		Task<Response<int>> Update(SecurityLoginHistoryEntity securityLoginHistory);
		Task<List<SecurityLoginHistoryEntity>> GetAll();
	}
}
