using System.Threading.Tasks;
using TalentMatrix.Configuration.Dto;

namespace TalentMatrix.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
