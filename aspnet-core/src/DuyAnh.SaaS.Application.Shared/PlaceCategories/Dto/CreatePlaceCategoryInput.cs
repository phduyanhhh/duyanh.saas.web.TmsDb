using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuyAnh.SaaS.Application.Shared.PlaceCategories.Dto
{
	public class CreatePlaceCategoryInput
	{
		public string Name { get; set; }
		public string Slug { get; set; }
		public string? IconUrl { get; set; }
		public string? Color { get; set; }
		public int? ParentId { get; set; }
	}
}
