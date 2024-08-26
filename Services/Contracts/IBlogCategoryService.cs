using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
	public interface IBlogCategoryService
	{
		Task<List<BlogCategoryDto>> GetBlogCategoryList();
		Task<BlogCategoryDto> GetBlogCategoryById(int id);
		Task CreateBlogCategory(BlogCategoryDto blogDto);
		Task UpdateBlogCategory(BlogCategoryDto blogDto);
		Task DeleteBlogCategory(int id);
	}
}
