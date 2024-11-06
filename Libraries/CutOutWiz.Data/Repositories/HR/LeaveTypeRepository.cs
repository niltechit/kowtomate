using CutOutWiz.Core;
using CutOutWiz.Data.DbAccess;
using CutOutWiz.Data.Entities.HR;

namespace CutOutWiz.Data.Repositories.HR
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ISqlDataAccess _db;

        public LeaveTypeRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        /// <summary>
        /// Get All LeaveTypes
        /// </summary>
        /// <returns></returns>
        public async Task<List<LeaveTypeEntity>> GetAll()
        {
            string query = "SELECT *FROM [dbo].[HR_LeaveType]";
            return await _db.LoadDataUsingQuery<LeaveTypeEntity, dynamic>(query, new { });
        }

        /// <summary>
        /// Get leaveType by leaveType Id
        /// </summary>
        /// <param name="LeaveTypeId"></param>
        /// <returns></returns>
        public async Task<LeaveTypeEntity> GetById(int leaveTypeId)
        {
            string query = "SELECT *FROM [dbo].[HR_LeaveType] WHERE Id= @LeaveTypeId";
            var result = await _db.LoadDataUsingQuery<LeaveTypeEntity, dynamic>(query, 
                new { LeaveTypeId = leaveTypeId });
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Insert leaveType
        /// </summary>
        /// <param name="leaveType"></param>
        /// <returns></returns>
        public async Task<Response<int>> Insert(LeaveTypeEntity leaveType)
        {
            var response = new Core.Response<int>();
            
            try
            {
                string sql = @"INSERT INTO [dbo].[HR_LeaveType] (Name, IsActive, CreatedByContactId, CreatedDate)
                            VALUES (@Name, @IsActive, @CreatedByContactId, @CreatedDate);
                            SELECT CAST(SCOPE_IDENTITY() as int)";

                var newId = await _db.SaveDataUsingQueryAndReturnId<int, dynamic>(
                    sql, new
                    {
                        Name = leaveType.Name,
                        IsActive = leaveType.IsActive,
                        CreatedByContactId = leaveType.CreatedByContactId,
                        CreatedDate = leaveType.CreatedDate
                    });

                leaveType.Id = newId;
                response.Result = newId;
                response.IsSuccess = true;
                response.Message = StandardDataAccessMessages.SuccessMessaage;

            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
            }

            return response;
        }

        /// <summary>
        /// Update LeaveType
        /// </summary>
        /// <param name="leaveType"></param>
        /// <returns></returns>
        public async Task<Response<bool>> Update(LeaveTypeEntity leaveType)
        {
            var response = new Core.Response<bool>();

            try
            {
                string sql = @"UPDATE [dbo].[HR_LeaveType] 
                               SET Name = @Name, 
                               IsActive = @IsActive, 
                               UpdatedByContactId = @UpdatedByContactId, 
                               UpdatedDate = @UpdatedDate 
                               WHERE Id = @Id";

                await _db.SaveDataUsingQuery(sql, new
                {
                    Id = leaveType.Id,
                    Name = leaveType.Name,
                    IsActive = leaveType.IsActive,
                    UpdatedByContactId = leaveType.UpdatedByContactId,
                    UpdatedDate = leaveType.UpdatedDate
                });

                response.IsSuccess = true;
                response.Message = StandardDataAccessMessages.SuccessMessaage;
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
            }

            return response;
        }

		/// <summary>
		/// Delete LeaveType by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<Response<bool>> Delete(int id)
        {
            var response = new Core.Response<bool>();

            try
            {
                string sql = "DELETE FROM [dbo].[HR_LeaveType] WHERE ID = @id";

                await _db.SaveDataUsingQuery(sql, new { id = id });

                response.IsSuccess = true;
                response.Message = StandardDataAccessMessages.SuccessMessaage;
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
            }

            return response;
        }

    }
}
