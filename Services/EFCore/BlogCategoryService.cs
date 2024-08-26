using AutoMapper;
using Entities.Model;
using Entities.ModelDto;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Services.EFCore
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public BlogCategoryService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateBlogCategory(BlogCategoryDto blogCategoryDto)
        {
            try
            {
                var blogCategory = _mapper.Map<BlogCategory>(blogCategoryDto);
                await _repository.BlogCategory.Create(blogCategory);
                _repository.Save();
            }
            catch (Exception ex)
            {


            }

        }

        public async Task DeleteBlogCategory(int id)
        {
            var blogCategory = _repository.BlogCategory.GetById(id).Result;
            if (blogCategory != null)
            {
                await _repository.BlogCategory.Delete(blogCategory);
                _repository.Save();
            }
        }

        public async Task<BlogCategoryDto> GetBlogCategoryById(int id)
        {
            var blogCategory = await _repository.BlogCategory.GetById(id);
            return _mapper.Map<BlogCategoryDto>(blogCategory);
        }

        public async Task<List<BlogCategoryDto>> GetBlogCategoryList()
        {
            var blogCategorys = await _repository.BlogCategory.Read(false);
            return _mapper.Map<List<BlogCategoryDto>>(blogCategorys);
        }

        public async Task UpdateBlogCategory(BlogCategoryDto blogCategoryDto)
        {
            var blogCategory = _mapper.Map<BlogCategory>(blogCategoryDto);
            await _repository.BlogCategory.Update(blogCategory);
            _repository.Save();
        }


    }
}
