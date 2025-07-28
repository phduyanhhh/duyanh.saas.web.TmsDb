using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace DuyAnh.SaaS.Localization;

public static class SaaSLocalizationConfigurer
{
    public static void Configure(ILocalizationConfiguration localizationConfiguration)
    {
        localizationConfiguration.Sources.Add(
            new DictionaryBasedLocalizationSource(SaaSConsts.LocalizationSourceName,
                new XmlEmbeddedFileLocalizationDictionaryProvider(
                    typeof(SaaSLocalizationConfigurer).GetAssembly(),
                    "DuyAnh.SaaS.Localization.SourceFiles"
                )
            )
        );
    }
}
