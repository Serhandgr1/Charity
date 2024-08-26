using AutoMapper;
using Entities.Enum;
using Entities.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Repositories.EFCore;

namespace AssociationWebApp.Controllers
{
	public class CausesController : Controller
	{
		private readonly RepositoryContext _context;
		private readonly IMapper _mapper;

		public CausesController(RepositoryContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public IActionResult Index()
		{
			var causes = _context.Blogs
				.Where(b => b.BlogTypeId == (int)BlogTypes.Causes)
				.ToList();

			var causesDto = _mapper.Map<List<BlogDto>>(causes);

			return View(causesDto);
		}
	}
}
