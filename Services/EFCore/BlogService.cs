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
	public class BlogService : IBlogService
	{
		private readonly IRepositoryManager _repository;
		private readonly IMapper _mapper;
		public BlogService(IRepositoryManager repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task CreateBlog(BlogDto blogDto)
		{
			var blog = _mapper.Map<Blog>(blogDto);
			await _repository.Blog.Create(blog);
			_repository.Save();
		}

		public async Task DeleteBlog(int id)
		{
			var blog = _repository.Blog.GetById(id).Result;
			if (blog != null)
			{
				await _repository.Blog.Delete(blog);
				_repository.Save();
			}
		}

		public async Task<BlogDto> GetBlogById(int id)
		{
			var blog = await _repository.Blog.GetById(id);
			return _mapper.Map<BlogDto>(blog);
		}

		public async Task<List<BlogDto>> GetBlogList()
		{
			var blogs = await _repository.Blog.Read(false);
			return _mapper.Map<List<BlogDto>>(blogs);
		}

		public async Task UpdateBlog(BlogDto blogDto)
		{
			var blog = _mapper.Map<Blog>(blogDto);
			await _repository.Blog.Update(blog);
			_repository.Save();
		}


	}
}
