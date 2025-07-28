using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuyAnh.SaaS.Application.Shared.Languages.Dto
{
	public class CreateLanguagesDto
	{
		public string Key { get; set; }
		public string Value { get; set; }
		public string LanguageName { get; set; }
		public string Source { get; set; } = "SaaS";
	}
}
