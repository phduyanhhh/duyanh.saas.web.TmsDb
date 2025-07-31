using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using DuyAnh.SaaS.Application.Shared.PlaceCategories;
using DuyAnh.SaaS.Entities;

namespace DuyAnh.SaaS.PlaceCategoriesAppSetting
{
	public class PlaceCategoriesAppSetting : SaaSAppServiceBase, IPlaceCategoriesAppSetting
	{
		private readonly IRepository<PlaceCategories, int> _repositoryPlaceCategories;

		public PlaceCategoriesAppSetting(IRepository<PlaceCategories, int> repositoryPlaceCategories)
		{
			_repositoryPlaceCategories = repositoryPlaceCategories;
		}

		public async Task Create()
		{

		}

	}
}
