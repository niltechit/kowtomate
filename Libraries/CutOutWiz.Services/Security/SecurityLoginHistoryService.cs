using CutOutWiz.Core;
using CutOutWiz.Services.Models.Security;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Entities.Security;
using CutOutWiz.Data.Repositories.Security;

namespace CutOutWiz.Services.Security
{
	public class SecurityLoginHistoryService : ISecurityLoginHistoryService
	{
		private readonly IMapperHelperService _mapperHelperService;
		private readonly ISecurityLoginHistoryRepository _securityLoginHistoryRepository;

		public SecurityLoginHistoryService(ISecurityLoginHistoryRepository securityLoginHistoryRepository,
			IMapperHelperService mapperHelperService)
		{
			_securityLoginHistoryRepository = securityLoginHistoryRepository;
			_mapperHelperService = mapperHelperService;
		}

		public async Task<List<SecurityLoginHistoryViewModel>> GetAll()
		{
			var entities = await _securityLoginHistoryRepository.GetAll();
			return await _mapperHelperService.MapToListAsync<SecurityLoginHistoryEntity, SecurityLoginHistoryViewModel>(entities);
        }

		public async Task<Response<int>> Insert(SecurityLoginHistoryModel securityLoginHistory)
		{
			var response = new Core.Response<int>();

			if (securityLoginHistory == null)
			{
				response.Message = "SecurityLoginHistory should not be empty.";
				return response;
			}

			//TODO: Add more validation here
			var entity = await _mapperHelperService.MapToSingleAsync<SecurityLoginHistoryModel, SecurityLoginHistoryEntity>(securityLoginHistory);
			return await _securityLoginHistoryRepository.Insert(entity);
		}

        public async Task<Response<int>> Update(SecurityLoginHistoryModel securityLoginHistory)
        {
			var response = new Core.Response<int>();

			if (securityLoginHistory == null)
			{
				response.Message = "SecurityLoginHistory should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<SecurityLoginHistoryModel, SecurityLoginHistoryEntity>(securityLoginHistory);
			return await _securityLoginHistoryRepository.Update(entity);
        }
    }
}
