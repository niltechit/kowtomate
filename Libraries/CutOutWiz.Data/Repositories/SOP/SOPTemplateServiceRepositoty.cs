using CutOutWiz.Core;
using CutOutWiz.Data.DbAccess;
using CutOutWiz.Data.Entities.SOP;

namespace CutOutWiz.Data.Repositories.SOP
{
	public class SOPTemplateServiceRepositoty : ISOPTemplateServiceRepositoty
	{
		private readonly ISqlDataAccess _db;

		public SOPTemplateServiceRepositoty(ISqlDataAccess db)
		{
			_db = db;
		}

		/// <summary>
		/// Delete User by id
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<Response<bool>> DeleteBySOPTempalteId(int sopTemplateId)
		{
			var response = new Response<bool>();

			try
			{
				await _db.SaveDataUsingProcedure(storedProcedure: "dbo.SP_SOP_TemplateService_Delete", new { SopTemplateId = sopTemplateId });
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
		/// Insert user
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public async Task<Response<int>> Insert(SOPTemplateServiceEntity sOPTemplateServiceEntity)
		{
			var response = new Response<int>();

			try
			{
				var newId = await _db.SaveDataUsingProcedureAndReturnId<int, dynamic>(storedProcedure: "dbo.SP_SOP_TemplateService_Insert", new
				{
					sOPTemplateServiceEntity.SOPTemplateId,
					sOPTemplateServiceEntity.SOPStandardServiceId,
					sOPTemplateServiceEntity.Status,
					sOPTemplateServiceEntity.CreatedByContactId,
					sOPTemplateServiceEntity.IsDeleted,
					sOPTemplateServiceEntity.ObjectId
				});

				sOPTemplateServiceEntity.Id = newId;
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
	}
}
