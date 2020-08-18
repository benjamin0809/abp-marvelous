using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace TalentMatrix.Localization
{
    public static class TalentMatrixLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(TalentMatrixConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(TalentMatrixLocalizationConfigurer).GetAssembly(),
                        "TalentMatrix.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
