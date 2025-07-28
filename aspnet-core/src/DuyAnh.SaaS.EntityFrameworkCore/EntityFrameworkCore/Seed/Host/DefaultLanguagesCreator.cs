using Abp.Localization;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DuyAnh.SaaS.EntityFrameworkCore.Seed.Host;

public class DefaultLanguagesCreator
{
    public static List<ApplicationLanguage> InitialLanguages => GetInitialLanguages();

    private readonly SaaSDbContext _context;

    private static List<ApplicationLanguage> GetInitialLanguages()
    {
        var tenantId = SaaSConsts.MultiTenancyEnabled ? null : (int?)MultiTenancyConsts.DefaultTenantId;
        return new List<ApplicationLanguage>
        {
            new ApplicationLanguage(tenantId, "en", "English", "famfamfam-flags us"),
						new ApplicationLanguage(tenantId, "vi", "Việt Nam", "famfamfam-flags vn")
				};
    }

    public DefaultLanguagesCreator(SaaSDbContext context)
    {
        _context = context;
    }

    public void Create()
    {
        CreateLanguages();
    }

    private void CreateLanguages()
    {
        foreach (var language in InitialLanguages)
        {
            AddLanguageIfNotExists(language);
        }
    }

    private void AddLanguageIfNotExists(ApplicationLanguage language)
    {
        if (_context.Languages.IgnoreQueryFilters().Any(l => l.TenantId == language.TenantId && l.Name == language.Name))
        {
            return;
        }

        _context.Languages.Add(language);
        _context.SaveChanges();
    }
}
