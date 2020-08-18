using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentMatrix.Authorization.Users;
using TalentMatrix.Extension;
using TalentMatrix.Org.Dto;

namespace TalentMatrix.Org
{
    public class OrgAppService: IApplicationService, IOrgAppService
    {
        private readonly IAbpSession _abpSession;
        public readonly IRepository<Organization> _reposity;
        public readonly MyAppSession _myAppSession;
        public OrgAppService(IRepository<Organization> reposity,
             MyAppSession myAppSession,
             IAbpSession abpSession)
        {
            _abpSession = abpSession;
            _reposity = reposity;
            _myAppSession = myAppSession;
        }
        public List<Organization> GetOrgList()
        {
            List<Organization> res = _reposity.GetAllList();
           string email =  _myAppSession.GetUserEmail();
            string email1 = _abpSession.GetUserEmail();
            string UserRole = _myAppSession.UserRole;
            return res;
        }
        public List<Organization> GetOrg(string ParentCode)
        {
            List<Organization> output = null;
            try
            {
                string role = AbpSessionExtension2.GetUserEmail(_abpSession);
                output = _reposity.GetAll().Where(result => result.InnerCode.StartsWith(ParentCode)).ToList();
            }catch(Exception e)
            {
                Debug.Write(e.Message);
            }
            
            return output;
        }

        public async Task<List<ExcelDto>> ReadExcel(string ParentCode)
        {
            var dict = ExcelHandler.ReadExcel<ExcelDto>("E:\\export.xlsx");
            return dict;
        }

        public int CreateOrganization(OrganizationDto dto)
        {
            Organization org = new Organization()
            {
                CreationTime = DateTime.Now,
                CreatorUserId = _abpSession.UserId,
                Name = dto.Name,
                Description = dto.Description,
                InnerCode = dto.InnerCode
            };
            
            return _reposity.InsertAndGetId(org); 
        }
    }
}
