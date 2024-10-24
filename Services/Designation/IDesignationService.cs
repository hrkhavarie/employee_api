using DemoWebAPI.Model;

namespace DemoWebAPI.Services.Designation
{
    public interface IDesignationService
    {
        Task<IEnumerable<TblDesignation>> GetDesignations();
        Task<TblDesignation> GetDesignation(int id);
        Task<TblDesignation> AddDesignation(TblDesignation designation);
        Task<TblDesignation> UpdateDesignation(TblDesignation designation);
        Task<bool> DeleteDesignation(int id);
    }
}
