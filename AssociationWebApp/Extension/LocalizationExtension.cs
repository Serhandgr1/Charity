using Microsoft.Extensions.Caching.Distributed;
using Services.Contracts;
using Services.EFCore;

namespace AssociationWebApp.Extension
{
	public static class LocalizationExtension
	{
		private static readonly IDistributedCache? _distributedCache;
		private static readonly IBlogCategoryService _categoryService;
      
        public static string GetLocalization(string key, string language)
		{
			Localizer localizer = new Localizer(_distributedCache);
			var data= localizer.GetValue(key, language);
			return localizer.GetValue(key, language);
		}

        public static string GetCategoryName(int id)
        {
            var category = _categoryService.GetBlogCategoryById(id).Result;
            return category.Title;
        }

    }
}
