using CutOutWiz.Core;
using CutOutWiz.Services.Models.HR;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Repositories.HR;
using CutOutWiz.Data.Entities.HR;
using CutOutWiz.Data.DTOs.HR;

namespace CutOutWiz.Services.HR
{
    public class EmployeeLeaveService : IEmployeeLeaveService
    {
		private readonly IMapperHelperService _mapperHelperService;
		private readonly IEmployeeLeaveRepository _employeeLeaveRepository;

		public EmployeeLeaveService(IEmployeeLeaveRepository employeeLeaveRepository,
			IMapperHelperService mapperHelperService)
		{
			_employeeLeaveRepository = employeeLeaveRepository;
			_mapperHelperService = mapperHelperService;
		}

		public Task<Response<bool>> Delete(string objectId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EmployeeLeaveModel>> GetAll()
        {
			var entities = await _employeeLeaveRepository.GetAll();
			return await _mapperHelperService.MapToListAsync<EmployeeLeaveEntity, EmployeeLeaveModel>(entities);
        }

        public async Task<List<EmployeeLeaveViewModel>> GetAll<EmployeeLeaveViewModel>() where EmployeeLeaveViewModel : class
        {
			var dtos = await _employeeLeaveRepository.GetAllWithDetail();
			return await _mapperHelperService.MapToListAsync<EmployeeLeaveDto, EmployeeLeaveViewModel>(dtos);
        }

        public async Task<EmployeeLeaveModel> GetById(int id)
        {
			var entity = await _employeeLeaveRepository.GetById(id);
			return await _mapperHelperService.MapToSingleAsync<EmployeeLeaveEntity, EmployeeLeaveModel>(entity);
        }

        public Task<EmployeeLeaveModel> GetByObjectId(string objectId)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<int>> Insert(EmployeeLeaveModel employeeLeave)
        {
			var response = new Core.Response<int>();

			if (employeeLeave == null)
			{
				response.Message = "EmployeeLeave should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<EmployeeLeaveModel, EmployeeLeaveEntity>(employeeLeave);
			return await _employeeLeaveRepository.Insert(entity);
        }

        public async Task<Response<bool>> Update(EmployeeLeaveModel employeeLeave)
        {
			var response = new Core.Response<bool>();

			if (employeeLeave == null)
			{
				response.Message = "EmployeeLeave should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<EmployeeLeaveModel, EmployeeLeaveEntity>(employeeLeave);
			return await _employeeLeaveRepository.Update(entity);
        }
    }
}
