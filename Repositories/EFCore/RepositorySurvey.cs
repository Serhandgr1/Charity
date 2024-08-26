using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositorySurvey : IRepositorySurvey
    {
        private readonly RepositoryContext _context;

        public RepositorySurvey(RepositoryContext context)
        {
            _context = context;
        }

        public async Task Create(Survey entity)
        {
            await _context.Surveys.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Survey>> Read(bool trackChanges)
        {
            return trackChanges
                ? await _context.Surveys.ToListAsync()
                : await _context.Surveys.AsNoTracking().ToListAsync();
        }
        
        public async Task Update(Survey entity)
        {
            _context.Surveys.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Survey entity)
        {
            _context.Surveys.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Survey> GetById(int id)
        {
            return await _context.Surveys.Include(q => q.Id).FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Survey> GetByName(string name)
        {
            return await _context.Surveys.Where( x => x.Name == name).FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
