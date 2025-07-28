using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;

namespace DuyAnh.SaaS.Entities
{
	public class PlaceCategories : FullAuditedEntity<int>
	{
		public string Name { get; set; }
		public string Slug { get; set; }
		public string? IconUrl { get; set; }
		public string? Color { get; set; }
		public int? ParentId { get; set; }
	}
}
