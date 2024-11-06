using CutOutWiz.Core;
using CutOutWiz.Data.Entities.HR;

namespace CutOutWiz.Data.Repositories.HR
{
    public interface IDesignationRepository
    {
        Task<Response<bool>> Delete(string objectId);
        Task<List<DesignationEntity>> GetAll();
        Task<DesignationEntity> GetById(int companyId);
        Task<DesignationEntity> GetByObjectId(string objectId);
        Task<Response<int>> Insert(DesignationEntity company);
        Task<Response<bool>> Update(DesignationEntity company);
    }
}
