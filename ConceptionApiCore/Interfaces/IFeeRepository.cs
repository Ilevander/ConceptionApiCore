using System;
using System.Collections.Generic;
using Doctors.Models;

namespace ConceptionApiCore.Interfaces
{
    public interface IFeeRepository
    {
        // Retrieve all fees
        ICollection<Fee> GetFees();

        // Retrieve a fee by its ID
        Fee GetFee(int feeId);

        // Retrieve all fees for a specific doctor
        ICollection<Fee> GetFeesByDoctor(int doctorId);

        bool FeeExists(int feeId);

        bool CreateFee(Fee fee);

        bool UpdateFee(Fee fee);

        bool DeleteFee(int feeId);

        bool Save();
    }
}
