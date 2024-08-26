using AutoMapper;
using Entities.Model;
using Entities.ModelDto;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EFCore
{
	public class BlogImageService : IBlogImageService
	{
		private readonly IRepositoryManager _repository;
		private readonly IMapper _mapper;
		public BlogImageService(IRepositoryManager repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task CreateBlogImage(BlogImageDto blogImageDto)
		{
			var blogImage = _mapper.Map<BlogImage>(blogImageDto);
			await _repository.BlogImage.Create(blogImage);
			_repository.Save();
		}

		public async Task DeleteBlogImage(int id)
		{
			var blogImage = _repository.BlogImage.GetById(id).Result;
			if (blogImage != null)
			{
				await _repository.BlogImage.Delete(blogImage);
				_repository.Save();
			}
		}

		public async Task<BlogImageDto> GetBlogImageById(int id)
		{
			var blogImage = await _repository.BlogImage.GetById(id);
			return _mapper.Map<BlogImageDto>(blogImage);
		}

		public async Task<List<BlogImageDto>> GetBlogImageList()
		{
			var blogImages = await _repository.BlogImage.Read(false);
			return _mapper.Map<List<BlogImageDto>>(blogImages);
		}

		public async Task UpdateBlogImage(BlogImageDto blogImageDto)
		{
			var blogImage = _mapper.Map<BlogImage>(blogImageDto);
			await _repository.BlogImage.Update(blogImage);
			_repository.Save();
		}


	}
}
