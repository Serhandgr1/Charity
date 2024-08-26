using AutoMapper;
using Entities.Enum;
using Entities.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Repositories.EFCore;

namespace AssociationWebApp.Components
{
	public class EventsViewComponent : ViewComponent
	{
		private readonly RepositoryContext _context;
		private readonly IMapper _mapper;

		public EventsViewComponent(RepositoryContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public IViewComponentResult Invoke()
		{
			var eventEntity = _context.Blogs
				.Where(b => b.BlogTypeId == (int)BlogTypes.Events)
				.FirstOrDefault();

			var eventDto = _mapper.Map<BlogDto>(eventEntity);

			return View(eventDto);
		}
	}


}
