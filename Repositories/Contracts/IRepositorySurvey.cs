using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositorySurvey : IRepositoryBase<Survey>
    {
        Task<Survey> GetByName(string name);
    }
}
