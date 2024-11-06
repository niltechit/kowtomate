using CutOutWiz.Core;
using CutOutWiz.Services.Models.HR;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Repositories.HR;
using CutOutWiz.Data.Entities.HR;

namespace CutOutWiz.Services.HR
{
    public class LeaveTypeService : ILeaveTypeService
    {
		private readonly IMapperHelperService _mapperHelperService;
		private readonly ILeaveTypeRepository _leaveTypeRepository;

		public LeaveTypeService(ILeaveTypeRepository leaveTypeRepository,
			IMapperHelperService mapperHelperService)
		{
			_leaveTypeRepository = leaveTypeRepository;
			_mapperHelperService = mapperHelperService;
		}

		/// <summary>
		/// Get All LeaveTypes
		/// </summary>
		/// <returns></returns>
		public async Task<List<LeaveTypeModel>> GetAll()
        {
			var entities = await _leaveTypeRepository.GetAll();
			return await _mapperHelperService.MapToListAsync<LeaveTypeEntity, LeaveTypeModel>(entities);
        }

        /// <summary>
        /// Get leaveType by leaveType Id
        /// </summary>
        /// <param name="LeaveTypeId"></param>
        /// <returns></returns>
        public async Task<LeaveTypeModel> GetById(int leaveTypeId)
        {
			var entity = await _leaveTypeRepository.GetById(leaveTypeId);
			return await _mapperHelperService.MapToSingleAsync<LeaveTypeEntity, LeaveTypeModel>(entity);
        }

        /// <summary>
        /// Insert leaveType
        /// </summary>
        /// <param name="leaveType"></param>
        /// <returns></returns>
        public async Task<Response<int>> Insert(LeaveTypeModel leaveType)
        {
			var response = new Core.Response<int>();

			if (leaveType == null)
			{
				response.Message = "LeaveType should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<LeaveTypeModel, LeaveTypeEntity>(leaveType);
			return await _leaveTypeRepository.Insert(entity);
        }

        /// <summary>
        /// Update LeaveType
        /// </summary>
        /// <param name="leaveType"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Update(LeaveTypeModel leaveType)
        {
			var response = new Core.Response<bool>();

			if (leaveType == null)
			{
				response.Message = "LeaveType should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<LeaveTypeModel, LeaveTypeEntity>(leaveType);
			return await _leaveTypeRepository.Update(entity);
		}

        /// <summary>
        /// Delete LeaveType by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Delete(int id)
        {
			var response = new Response<bool>();

			if (id == 0)
			{
				response.Message = "ObjectId should not be empty.";
				return response;
			}

			return await _leaveTypeRepository.Delete(id);
        }

    }
}
