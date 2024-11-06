using CutOutWiz.Core;
using CutOutWiz.Data.DbAccess;
using CutOutWiz.Data.Entities.SOP;

namespace CutOutWiz.Data.Repositories.SOP
{
	public class SOPTempleateFileRepository : ISOPTempleateFileRepository
	{
		private readonly ISqlDataAccess _db;

		public SOPTempleateFileRepository(ISqlDataAccess db)
		{
			_db = db;
		}

		#region Sop Template File
		public async Task<List<SOPTemplateFileEntity>> GetSopTemplateFilesBySopTemplateId(int SOPTemplateId)
		{
			return await _db.LoadDataUsingProcedure<SOPTemplateFileEntity, dynamic>(storedProcedure: "dbo.[SP_TemplateFile_GetBySOPTemplateId]", new { SOPTemplateId = SOPTemplateId });
		}
		public async Task<List<SOPTemplateFileEntity>> GetSopTemplateFilesByTemplateId(int SOPTemplateId)
		{
			return await _db.LoadDataUsingProcedure<SOPTemplateFileEntity, dynamic>(storedProcedure: "dbo.[SP_TemplateFile_GetSOPTemplateByTemplateId]", new { SOPTemplateId = SOPTemplateId });
		}

		public async Task<Response<bool>> UpdateTemplateFile(string objectId)
		{
			var response = new Response<bool>();

			try
			{
				await _db.SaveDataUsingProcedure(storedProcedure: "dbo.[SP_SOP_Template_UpdateIsDeleteByObjectId]", new { ObjectId = objectId });

			}
			catch (Exception ex)
			{
				response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
			}
			return response;
		}

		public async Task<SOPTemplateFileEntity> GetSopTemplateFilesById(int fileId)
		{
			var result = await _db.LoadDataUsingProcedure<SOPTemplateFileEntity, dynamic>(storedProcedure: "dbo.SP_SOPTemplateFile_GetById", new { FileId = fileId });
			return result.FirstOrDefault();
		}

		#endregion
		public async Task<Response<int>> Insert(SOPTemplateFileEntity file)
		{
			var response = new Response<int>();
			try
			{
				var newId = await _db.SaveDataUsingProcedureAndReturnId<int, dynamic>(storedProcedure: "dbo.SP_SOP_TemplateFile_Insert", new
				{
					file.SOPTemplateId,
					file.FileName,
					file.FileType,
					file.ActualPath,
					file.ModifiedPath,
					file.IsDeleted,
					file.CreatedByContactId,
					file.ObjectId,
					file.RootFolderPath,
					file.ViewPath,
					file.FileByteString,
				});

				file.Id = newId;
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
