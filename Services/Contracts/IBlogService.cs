using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
	public interface IBlogService
	{
		Task<List<BlogDto>> GetBlogList();
		Task<BlogDto> GetBlogById(int id);
		Task CreateBlog(BlogDto blogDto);
		Task UpdateBlog(BlogDto blogDto);
		Task DeleteBlog(int id);
	}
}
