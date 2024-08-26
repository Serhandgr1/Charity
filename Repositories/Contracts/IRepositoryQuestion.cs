using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryQuestion : IRepositoryBase<Question>
    {
        Task<List<Question>> GetBySurveyId(int id);
    }
}
