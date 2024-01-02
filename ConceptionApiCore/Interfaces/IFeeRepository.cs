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
        Fee GetFee(Guid feeId);

        // Retrieve all fees for a specific doctor
        ICollection<Fee> GetFeesByDoctor(Guid doctorId);

        bool FeeExists(Guid feeId);

        bool CreateFee(Fee fee);

        bool UpdateFee(Fee fee);

        bool DeleteFee(Guid feeId);

        bool Save();
    }
}
