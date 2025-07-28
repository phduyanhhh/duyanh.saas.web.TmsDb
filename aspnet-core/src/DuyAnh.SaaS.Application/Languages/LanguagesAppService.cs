using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.EntityFrameworkCore;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.UI;
using DuyAnh.SaaS.Application.Shared.Languages;
using DuyAnh.SaaS.Application.Shared.Languages.Dto;
using DuyAnh.SaaS.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace DuyAnh.SaaS.Languages
{
	public class LanguagesAppService : SaaSAppServiceBase, ILanguagesAppService
	{
		private readonly IApplicationLanguageManager _languageManager;
		private readonly IApplicationLanguageTextManager _languageTextManager;
		private readonly IDbContextProvider<SaaSDbContext> _dbContextProvider;

		public LanguagesAppService(
			IApplicationLanguageManager languageManager,
			IApplicationLanguageTextManager languageTextManager,
			IDbContextProvider<SaaSDbContext> dbContextProvider
		)
		{
			_languageManager = languageManager;
			_languageTextManager = languageTextManager;
			_dbContextProvider = dbContextProvider;
		}
		
		public async Task UploadFileJsonLanguage(IFormFile fileJson, string sourceName = SaaSConsts.LocalizationSourceName)
		{
			if (fileJson == null || fileJson.Length == 0)
			{
				throw new UserFriendlyException("File không hợp lệ hoặc trống.");
			}

			using var stream = fileJson.OpenReadStream();
			using var reader = new StreamReader(stream);
			var content = await reader.ReadToEndAsync();

			if (string.IsNullOrWhiteSpace(content))
			{
				throw new UserFriendlyException("Nội dung file trống.");
			}

			JObject jsonObject;
			try
			{
				jsonObject = JObject.Parse(content);
			}
			catch
			{
				throw new UserFriendlyException("File JSON không đúng định dạng.");
			}

			var languageName = jsonObject["languageName"]?.ToString();
			if (string.IsNullOrWhiteSpace(languageName))
			{
				throw new UserFriendlyException("Thiếu 'languageName' trong JSON.");
			}

			JObject translationsObject = jsonObject["translations"] as JObject;
			if (translationsObject == null)
			{
				throw new UserFriendlyException("Thiếu hoặc sai định dạng 'translations' trong JSON.");
			}

			var translations = translationsObject.ToObject<Dictionary<string, string>>();

			try
			{
				var culture = new CultureInfo(languageName);
				foreach (var kvp in translations)
				{
					if (string.IsNullOrWhiteSpace(kvp.Key) || string.IsNullOrWhiteSpace(kvp.Value))
					{
						continue;
					}

					await _languageTextManager.UpdateStringAsync(
							AbpSession.TenantId,
							sourceName,
							culture,
							kvp.Key,
							kvp.Value
					);
				}
			}
			catch (CultureNotFoundException)
			{
				throw new UserFriendlyException($"'{languageName}' không phải là mã ngôn ngữ hợp lệ.");
			}
		}

		public async Task<PagedResultDto<LanguagesListDto>> GetAllLanguages(GetAllLanguagesInput input)
		{
			// get all language
			var dbContext = await _dbContextProvider.GetDbContextAsync();
			// Truy vấn từ bảng AbpLanguageTexts
			var query = dbContext.Set<ApplicationLanguageText>()
				.WhereIf(AbpSession.TenantId.HasValue, x => x.TenantId == AbpSession.TenantId)
				.Select(x => new LanguagesListDto
				{
					LanguageName = x.LanguageName,
					Source = x.Source,
					Key = x.Key,
					Value = x.Value
				});

			if (!string.IsNullOrWhiteSpace(input.Search))
			{
				query = query.Where(x =>
					x.Key.Contains(input.Search) ||
					x.Value.Contains(input.Search));
			}

			if (!string.IsNullOrWhiteSpace(input.LanguageName))
			{
				query = query.Where(x => x.LanguageName == input.LanguageName);
			}

			if (!string.IsNullOrWhiteSpace(input.SourceName))
			{
				query = query.Where(x => x.Source == input.SourceName);
			}

			if (!string.IsNullOrWhiteSpace(input.Sorting))
			{
				query = query.OrderBy(input.Sorting); // ví dụ: Key asc
			}
			else
			{
				query = query.OrderBy(x => x.Key);
			}

			// Đếm tổng số bản ghi
			var totalCount = await query.CountAsync();

			// Phân trang
			var items = await query
				.Skip(input.SkipCount)
				.Take(input.MaxResultCount)
				.ToListAsync();

			return new PagedResultDto<LanguagesListDto>(totalCount, items);
		}
		public async Task Create(CreateLanguagesDto input)
		{
			await _languageTextManager.UpdateStringAsync(
				AbpSession.TenantId,
				input.Source,
				new CultureInfo(input.LanguageName),
				input.Key,
				input.Value
			);
		}

		public async Task<LanguagesListDto> GetAnLanguages(EditLanguagesDto input)
		{
			var existingText = _languageTextManager.GetStringOrNull(
						tenantId: null,
						sourceName: input.Source,
						culture: new CultureInfo(input.LanguageName),
						key: input.Key,
						tryDefaults: false
				);
			return new LanguagesListDto
			{
				LanguageName = input.LanguageName,
				Source = input.Source,
				Key = input.Key,
				Value = existingText
			};
		}

		public async Task Edit(EditLanguagesDto input)
		{
			var existingText = _languageTextManager.GetStringOrNull(
						tenantId: null,
						sourceName: input.Source,
						culture: new CultureInfo(input.LanguageName),
						key: input.Key,
						tryDefaults: false
				);

			if (existingText != null)
			{
				await _languageTextManager.UpdateStringAsync(
						tenantId: null,
						sourceName: input.Source,
						culture: new CultureInfo(input.LanguageName),
						key: input.Key,
						value: input.Value
				);
			}
		}
	}
}
