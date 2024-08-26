using AutoMapper;
using Entities.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;

namespace AssociationWebApp.Components
{
    public class FeaturedNewsViewComponent : ViewComponent
    {
        private readonly RepositoryContext _context;
        private readonly IMapper _mapper;

        public FeaturedNewsViewComponent(RepositoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // En son eklenen blogu al
            var latestBlog = await _context.Blogs
                .OrderByDescending(b => b.Id)
                .FirstOrDefaultAsync();

            // DTO'ya harca
            var blogDto = _mapper.Map<BlogDto>(latestBlog);

            return View(blogDto);
        }
    }
}
