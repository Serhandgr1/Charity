using AutoMapper;
using Entities.Enum;
using Entities.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Repositories.EFCore;

namespace AssociationWebApp.Components
{
	public class CausesViewComponent : ViewComponent
	{
		private readonly RepositoryContext _context;
		private readonly IMapper _mapper;

		public CausesViewComponent(RepositoryContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public IViewComponentResult Invoke()
		{
			var causes = _context.Blogs
				.Where(b => b.BlogTypeId == (int)BlogTypes.Causes)
				.ToList();

			var causesDto = _mapper.Map<List<BlogDto>>(causes);

			return View(causesDto);
		}
	}
}
