using AutoMapper;
using Entities.Enum;
using Entities.ModelDto;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Repositories.EFCore;

namespace AssociationWebApp.Controllers
{
	public class EventsController : Controller
	{
		private readonly RepositoryContext _context;
		private readonly IMapper _mapper;

		public EventsController(RepositoryContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public IActionResult Index()
		{
			var events = _context.Blogs
				.Where(b => b.BlogTypeId == (int)BlogTypes.Events)
				.ToList();

			var eventsDto = _mapper.Map<List<BlogDto>>(events);

			return View(eventsDto);
		}
	}

}
