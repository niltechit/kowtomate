using CutOutWiz.Core;
using CutOutWiz.Services.Models.HR;
using CutOutWiz.Services.DbAccess;
using CutOutWiz.Data;
using CutOutWiz.Data.Repositories.Security;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Repositories.HR;
using CutOutWiz.Data.Entities.Security;
using CutOutWiz.Services.Models.Security;
using CutOutWiz.Data.Entities.HR;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CutOutWiz.Services.HR
{
    public class LeaveSubTypeService : ILeaveSubTypeService
    {
		private readonly IMapperHelperService _mapperHelperService;
		private readonly ILeaveSubTypeRepository _leaveSubTypeRepository;

		public LeaveSubTypeService(ILeaveSubTypeRepository leaveSubTypeRepository,
			IMapperHelperService mapperHelperService)
		{
			_leaveSubTypeRepository = leaveSubTypeRepository;
			_mapperHelperService = mapperHelperService;
		}

		/// <summary>
		/// Get All LeaveSubTypes
		/// </summary>
		/// <returns></returns>
		public async Task<List<LeaveSubTypeModel>> GetAll()
        {
			var entities = await _leaveSubTypeRepository.GetAll();
			return await _mapperHelperService.MapToListAsync<LeaveSubTypeEntity, LeaveSubTypeModel>(entities);
        }

        /// <summary>
        /// Get leaveType by leaveType Id
        /// </summary>
        /// <param name="LeaveSubTypeId"></param>
        /// <returns></returns>
        public async Task<LeaveSubTypeModel> GetById(int id)
        {
			var entity = await _leaveSubTypeRepository.GetById(id);
			return await _mapperHelperService.MapToSingleAsync<LeaveSubTypeEntity, LeaveSubTypeModel>(entity);
        }

        /// <summary>
        /// Insert leaveType
        /// </summary>
        /// <param name="leaveType"></param>
        /// <returns></returns>
        public async Task<Response<int>> Insert(LeaveSubTypeModel leaveType)
        {
			var response = new Core.Response<int>();

			if (leaveType == null)
			{
				response.Message = "LeaveType should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<LeaveSubTypeModel, LeaveSubTypeEntity>(leaveType);
			return await _leaveSubTypeRepository.Insert(entity);
        }

        /// <summary>
        /// Update LeaveSubType
        /// </summary>
        /// <param name="leaveType"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Update(LeaveSubTypeModel leaveType)
        {
			var response = new Core.Response<bool>();

			if (leaveType == null)
			{
				response.Message = "LeaveType should not be empty.";
				return response;
			}

			//TODO: Add more validation here

			var entity = await _mapperHelperService.MapToSingleAsync<LeaveSubTypeModel, LeaveSubTypeEntity>(leaveType);
			return await _leaveSubTypeRepository.Update(entity);
		}

        /// <summary>
        /// Delete LeaveSubType by id
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Delete(int id)
        {
			var response = new Response<bool>();

			if (id == 0)
			{
				response.Message = "Id should not be empty.";
				return response;
			}

			return await _leaveSubTypeRepository.Delete(id);
        }

        public async Task<Response<List<LeaveSubTypeModel>>> GetSubLeaveTypes(int leaveTypeId)
        {           
			var response = new Response<List<LeaveSubTypeModel>>();

            try
            {
                var subLeaveTypeEntities = await _leaveSubTypeRepository.GetSubLeaveTypes(leaveTypeId);				
				response.IsSuccess = true;
                response.Result = await _mapperHelperService.MapToListAsync<LeaveSubTypeEntity, LeaveSubTypeModel>(subLeaveTypeEntities);
            }
            catch(Exception ex)
            {
                response = new Response<List<LeaveSubTypeModel>>();
                response.IsSuccess = false;
                response.Message = ex.Message;
                
            }
            return response;
        }
    }
}
