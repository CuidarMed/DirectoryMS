using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPatientQuery
    {
        Task<Patient?> getPatientById(long id);
        Task<Patient?> getPatientByUserId(long userId);
        Task<List<Patient>> GetAllAsync();
    }
}
