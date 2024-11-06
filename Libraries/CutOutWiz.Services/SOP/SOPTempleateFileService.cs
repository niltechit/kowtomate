using CutOutWiz.Services.Models.SOP;
using CutOutWiz.Core;
using CutOutWiz.Data.Repositories.SOP;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Data.Entities.SOP;

namespace CutOutWiz.Services.SOP
{
	public class OrderSOPTempleateFileService : IOrderSOPTempleateFileService
    {
		private readonly IMapperHelperService _mapperHelperService;
		private readonly ISOPTempleateFileRepository _sOPTempleateFileRepository;

		public OrderSOPTempleateFileService(ISOPTempleateFileRepository sOPTempleateFileRepository,
			IMapperHelperService mapperHelperService)
		{
			_sOPTempleateFileRepository = sOPTempleateFileRepository;
			_mapperHelperService = mapperHelperService;
		}

		public async Task<Response<int>> Insert(SOPTemplateFile file)
        {
			var response = new Core.Response<int>();

			if (file == null)
			{
				response.Message = "File should not be empty.";
				return response;
			}

			//TODO: Add more validation here
			var entity = await _mapperHelperService.MapToSingleAsync<SOPTemplateFile, SOPTemplateFileEntity>(file);
			return await _sOPTempleateFileRepository.Insert(entity);
        }
    }
}
