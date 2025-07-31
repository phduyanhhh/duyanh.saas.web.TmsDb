using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;

namespace DuyAnh.SaaS.Entities
{
	[Table("PlaceCategories", Schema = SaaSConsts.DefaultSchema)]
	public class PlaceCategories : FullAuditedEntity<int>
	{
		[Required]
		[MinLength(2)]
		[MaxLength(255)]
		public string Name { get; set; }
		[Required]
		public string Slug { get; set; }
		public string? IconUrl { get; set; }
		public string? Color { get; set; }
		public int? ParentId { get; set; }
	}
}
