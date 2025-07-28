using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DuyAnh.SaaS.Application.Shared.Languages.Dto;
using Microsoft.AspNetCore.Http;

namespace DuyAnh.SaaS.Application.Shared.Languages
{
	public interface ILanguagesAppService : IApplicationService
	{
		Task UploadFileJsonLanguage(IFormFile fileJson, string sourceName = SaaSConsts.LocalizationSourceName);
		Task<PagedResultDto<LanguagesListDto>> GetAllLanguages(GetAllLanguagesInput input);
		Task Create(CreateLanguagesDto input);
		Task<LanguagesListDto> GetAnLanguages(EditLanguagesDto input);
		Task Edit(EditLanguagesDto input);
	}
}
