using System;
using ToDo.Domain.Interface;
using ToDo.Infra.Data;

namespace ToDo.Web.Mvc.Extensions
{

	public static class ServicesExtension
	{

		public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
		{
			services.AddScoped<IItemRepository, ItemRepository>();

			return services;
		}

	}

}

