using CutOutWiz.Core;
using CutOutWiz.Data.Entities.OrderSOP;
using CutOutWiz.Data.DbAccess;

namespace CutOutWiz.Data.Repositories.OrderSOP
{
	public class OrderSOPTemplateFileRepository : IOrderSOPTemplateFileRepository
	{
        private readonly ISqlDataAccess _db;
        public OrderSOPTemplateFileRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<Response<int>> Insert(OrderSOPTemplateFileEntity file)
        {
            var response = new Response<int>();
            try
            {
                var newId = await _db.SaveDataUsingProcedureAndReturnId<short, dynamic>(storedProcedure: "dbo.SP_OrderSOP_TemplateFile_Insert", new
                {
                    file.OrderSOPTemplateId,
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
                    file.BaseSOPTemplateFileId,
                    file.BaseTemplateId
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
        public async Task<List<OrderSOPTemplateFileEntity>> GetOrderSopTemplateFilesByOrderSopTemplateId(int SOPTemplateId)
        {
            return await _db.LoadDataUsingProcedure<OrderSOPTemplateFileEntity, dynamic>(storedProcedure: "dbo.SP_OrderSOPTemplateFile_GetBySOPTemplateId", new { SOPTemplateId = SOPTemplateId });
        }
        public async Task<OrderSOPTemplateFileEntity> GetById(int fileId)
        {
            var result = await _db.LoadDataUsingProcedure<OrderSOPTemplateFileEntity, dynamic>(storedProcedure: "dbo.SP_OrderSOPTemplateFile_GetById", new { Id = fileId });
            return result.FirstOrDefault();
        }
        public async Task<OrderSOPTemplateFileEntity> GetByOrderSOPTemplateIdAndFileName(int orderSOPTemplateId , string fileName)
        {
            var result = await _db.LoadDataUsingProcedure<OrderSOPTemplateFileEntity, dynamic>(storedProcedure: "dbo.SP_OrderSOPTemplateFile_GetByOrderSOPTemplateIdAndFileName", new 
            {
				OrderSOPTemplateId = orderSOPTemplateId,
                FileName= fileName
			});
            return result.FirstOrDefault();
        }
        
    }
}
