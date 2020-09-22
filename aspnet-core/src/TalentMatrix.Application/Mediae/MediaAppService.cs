using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Logging;
using Abp.Runtime.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeDetective.Extensions;
using TalentMatrix.Mediae.Dto;
using Abp.Configuration;
using Abp.UI;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Hosting;
using MimeDetective;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using TalentMatrix.Mediae;
using TalentMatrix.Configuration;
using SimpleCmsWithAbp.Mediae;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace TalentMatrix.Mediae
{
    public class MediaAppService: AsyncCrudAppService<Media, MediaDto, long, GetAllMediaInputDto, CreateMediaDto, UpdateMediaInputDto, MediaDto>
    {
        public MediaAppService(IRepository<Media, long> repository) : base(repository)
        {
        }

        public override async Task<PagedResultDto<MediaDto>> GetAllAsync(GetAllMediaInputDto input)
        {
            CheckGetAllPermission();
            var query = Repository.GetAll().Where(m=>input.Type.Contains((int)m.Type));

            if (!string.IsNullOrEmpty(input.Query)) query = query.Where(m => m.Description.Contains(input.Query));
            if (input.Year != null && input.Month != null)
            {
                query = query.Where(m => m.CreationTime.Year == input.Year && m.CreationTime.Month == input.Month);
            }
            if (input.Day != null)
            {
                query = query.Where(m => m.CreationTime.Day == input.Day);
            }

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);
            return new PagedResultDto<MediaDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList()
            );

        }

        public override async Task<MediaDto> CreateAsync([FromForm]CreateMediaDto input)
        {
            CheckCreatePermission();
            var stream = input.File.OpenReadStream();
            int allowUploadSize = 3666451;
            //var allowImageFileType = await SettingManager.GetSettingValueAsync(AppSettingNames.AllowImageFileType);
            //var allowAudioFileType = await SettingManager.GetSettingValueAsync(AppSettingNames.AllowAudioFileType);
            //var allowVideoFileType = await SettingManager.GetSettingValueAsync(AppSettingNames.AllowVideoFileType);
            //var allowUploadSize = await SettingManager.GetSettingValueAsync<int>(AppSettingNames.AllowUploadSize);
            var fileType = stream.GetFileType();
            var ext = fileType?.Extension;
            MediaType? type = MediaType.Image;
            //if (allowImageFileType.IndexOf($",{ext},", StringComparison.OrdinalIgnoreCase) >= 0)
            //{
            //    type = MediaType.Image;

            //}
            //else if (allowAudioFileType.IndexOf($",{ext},", StringComparison.OrdinalIgnoreCase) >= 0)
            //{
            //    type = MediaType.Audio;
            //}
            //else if (allowVideoFileType.IndexOf($",{ext},", StringComparison.OrdinalIgnoreCase) >= 0)
            //{
            //    type = MediaType.Video;
            //}
            // if (type == null) throw new UserFriendlyException("fileTypeNotAllow");
            if(stream.Length ==0 || stream.Length>allowUploadSize ) throw new UserFriendlyException("fileSizeNotAllow");
            var filename = Guid.NewGuid().ToString();
            var path = $"{filename.Substring(0, 2)}/{filename.Substring(2, 2)}";
            var dir = $"{Environment.CurrentDirectory}\\wwwroot\\upload\\{path}";
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            using (var fileStream = new FileStream($"{dir}\\{filename}.{ext}", FileMode.Create))
            {
                await input.File.CopyToAsync(fileStream);
            }
            if (type == MediaType.Image)
            {
                using (var image = Image.Load<Rgba32>($"{dir}\\{filename}.{ext}"))
                {
                    image.Mutate(x => x
                        .Resize(160, 160));
                    image.Save($"{dir}\\thumbnail_{filename}.{ext}");
                }
            }
            var entity = new Media()
            {
                Filename = $"{filename}.{ext}",
                Description = input.File.FileName,
                Size = (int)stream.Length,
                Path = path,
                Type = type ?? MediaType.Image,
                CreatorUserId = AbpSession.UserId
            };
            await Repository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToEntityDto(entity);

        }


 
    }
}