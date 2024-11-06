using CutOutWiz.Core;
using CutOutWiz.Services.Models.HR;
using CutOutWiz.Data;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Repositories.HR;
using CutOutWiz.Data.Entities.Security;
using CutOutWiz.Services.Models.Security;
using CutOutWiz.Data.Entities.HR;
using Mailjet.Client.Resources;

namespace CutOutWiz.Services.HR
{
	public class EmployeeProfileService : IEmployeeProfileService
	{
		private readonly IMapperHelperService _mapperHelperService;
		private readonly IEmployeeProfileRepository _employeeProfileRepository;

		public EmployeeProfileService(IEmployeeProfileRepository employeeProfileRepository,
			IMapperHelperService mapperHelperService)
		{
			_employeeProfileRepository = employeeProfileRepository;
			_mapperHelperService = mapperHelperService;
		}

		public async Task<Response<bool>> Delete(string objectId)
		{
			var response = new Response<bool>();

			if (string.IsNullOrEmpty(objectId))
			{
				response.Message = "ObjectId should not be empty.";
				return response;
			}

			return await _employeeProfileRepository.Delete(objectId);
		}

        public async Task<List<EmployeeProfileModel>> GetAll<EmployeeProfileModel>() where EmployeeProfileModel : class
        {
			var entities = await _employeeProfileRepository.GetAll();
			return await _mapperHelperService.MapToListAsync<EmployeeProfileEntity, EmployeeProfileModel>(entities);
        }

        public async Task<EmployeeProfileModel> GetById(int id)
		{
			var entity = await _employeeProfileRepository.GetById(id);
			return await _mapperHelperService.MapToSingleAsync<EmployeeProfileEntity, EmployeeProfileModel>(entity);
		}

		public async Task<EmployeeProfileModel> GetByObjectId(string objectId)
		{
			throw new NotImplementedException();
		}

		public async Task<Response<int>> Insert(EmployeeProfileModel employeeProfileModel)
		{
			var response = new Core.Response<int>();

			if (employeeProfileModel == null)
			{
				response.Message = "EmployeeProfile should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<EmployeeProfileModel, EmployeeProfileEntity>(employeeProfileModel);
			return await _employeeProfileRepository.Insert(entity);
		}

		public async Task<Response<bool>> Update(EmployeeProfileModel employeeProfileModel)
		{
			var response = new Core.Response<bool>();

			if (employeeProfileModel == null)
			{
				response.Message = "EmployeeProfile should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<EmployeeProfileModel, EmployeeProfileEntity>(employeeProfileModel);
			return await _employeeProfileRepository.Update(entity);
		}

    }
}
