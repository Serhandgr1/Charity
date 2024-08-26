using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
	public interface IBlogImageService
	{
		Task<List<BlogImageDto>> GetBlogImageList();
		Task<BlogImageDto> GetBlogImageById(int id);
		Task CreateBlogImage(BlogImageDto blogImageDto);
		Task UpdateBlogImage(BlogImageDto blogImageDto);
		Task DeleteBlogImage(int id);
	}
}
