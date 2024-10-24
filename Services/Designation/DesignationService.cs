using DemoWebAPI.Data;
using DemoWebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPI.Services.Designation
{
    public class DesignationService : IDesignationService
    {
        private readonly ApplicationDBContext _context;

        public DesignationService(ApplicationDBContext context) => _context = context;

        public async Task<TblDesignation> AddDesignation(TblDesignation designation)
        {
            _context.Designations.Add(designation);
            await _context.SaveChangesAsync();
            return designation;
        }

        public async Task<bool> DeleteDesignation(int id)
        {
            var designation = await _context.Designations.FindAsync(id);
            if (designation != null)
            {
                return true;
            }
            return false;
        }

        public async Task<TblDesignation> GetDesignation(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Designations.Include(navigationPropertyPath: d => d.Employees).FirstOrDefaultAsync(d => d.DesId == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<TblDesignation>> GetDesignations()
        {
            return await _context.Designations.Include(_d => _d.Employees).ToListAsync();
        }

        public async Task<TblDesignation> UpdateDesignation(TblDesignation designation)
        {
            _context.Designations.Update(designation);
            await _context.SaveChangesAsync();
            return designation;
        }
    }
}
