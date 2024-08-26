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
    public class RepositoryAnswer : IRepositoryAnswer
    {
        private readonly RepositoryContext _context;

        public RepositoryAnswer(RepositoryContext context)
        {
            _context = context;
        }

        public async Task Create(Answer entity)
        {
            await _context.Answers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Answer>> Read(bool trackChanges)
        {
            return trackChanges
                ? await _context.Answers.Include(a => a.Question).ToListAsync()
                : await _context.Answers.Include(a => a.Question).AsNoTracking().ToListAsync();
        }

        public async Task Update(Answer entity)
        {
            _context.Answers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Answer entity)
        {
            _context.Answers.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Answer> GetById(int id)
        {
            return await _context.Answers.Include(a => a.Question).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Answer>> GetAllAnswersByQuestionId(int questionId)
        {
            return await _context.Answers
                                 .Where(a => a.QuestionId == questionId)
                                 .ToListAsync();
        }
    }
}
