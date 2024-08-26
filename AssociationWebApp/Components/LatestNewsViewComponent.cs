using AutoMapper;
using Entities.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;
using System.Linq;
using System.Threading.Tasks;

public class LatestNewsViewComponent : ViewComponent
{
    private readonly RepositoryContext _context;
    private readonly IMapper _mapper;

    public LatestNewsViewComponent(RepositoryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        // Son 3 blogu al
        var latestBlogs = await _context.Blogs
            .OrderByDescending(b => b.Id) // Son eklenenleri al
            .Take(3) // Sadece 3 blog al
            .ToListAsync(); // Listeye dönüştür

        // DTO'ya harca
        var blogDtos = _mapper.Map<List<BlogDto>>(latestBlogs);

        return View(blogDtos);
    }
}
