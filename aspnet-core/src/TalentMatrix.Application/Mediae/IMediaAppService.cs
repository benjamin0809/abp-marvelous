using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TalentMatrix.Mediae.Dto;

namespace SimpleCmsWithAbp.Mediae
{
    public interface IMediaAppService :IAsyncCrudAppService<MediaDto, long>
    {
        
    }
}