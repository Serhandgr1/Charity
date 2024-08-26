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
    public class RepositoryQuestion : IRepositoryQuestion
    {
        private readonly RepositoryContext _context;

        public RepositoryQuestion(RepositoryContext context)
        {
            _context = context;
        }

        public async Task Create(Question entity)
        {
            await _context.Questions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Question>> Read(bool trackChanges)
        {
            return trackChanges
                ? await _context.Questions.Include(q => q.Answers).ToListAsync()
                : await _context.Questions.Include(q => q.Answers).AsNoTracking().ToListAsync();
        }

        public async Task Update(Question entity)
        {
            _context.Questions.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Question entity)
        {
            _context.Questions.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Question> GetById(int id)
        {
            return await _context.Questions.Include(q => q.Answers).FirstOrDefaultAsync(q => q.Id == id);
        }
        public async Task<List<Question>> GetBySurveyId(int id)
        {
            return await _context.Questions.Where(x =>  x.SurveyId == id).ToListAsync();
        }
    }
}
