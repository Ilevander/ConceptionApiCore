using System;
using System.Collections.Generic;
using System.Linq;
using ConceptionApiCore.Interfaces;
using Doctors.Data;
using Doctors.Models;

namespace ConceptionApiCore.Repository
{
    public class FeeRepository : IFeeRepository
    {
        private readonly DataContext _dbContext;

        public FeeRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<Fee> GetFees()
        {
            return _dbContext.Fees.ToList();
        }

        public Fee GetFee(Guid feeId)
        {
            return _dbContext.Fees.FirstOrDefault(f => f.FeeID == feeId);
        }

        public ICollection<Fee> GetFeesByDoctor(Guid doctorId)
        {
            return _dbContext.Fees.Where(f => f.DoctorID == doctorId).ToList();
        }

        public bool FeeExists(Guid feeId)
        {
            return _dbContext.Fees.Any(f => f.FeeID == feeId);
        }

        public bool CreateFee(Fee fee)
        {
            _dbContext.Fees.Add(fee);
            return Save();
        }

        public bool UpdateFee(Fee fee)
        {
            _dbContext.Fees.Update(fee);
            return Save();
        }

        public bool DeleteFee(Guid feeId)
        {
            var feeToDelete = _dbContext.Fees.FirstOrDefault(f => f.FeeID == feeId);
            if (feeToDelete != null)
            {
                _dbContext.Fees.Remove(feeToDelete);
                return Save();
            }
            return false;
        }

        public bool Save()
        {
            try
            {
                return _dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                // Handle exceptions if needed
                return false;
            }
        }
    }
}
