using Abp;
using Abp.Application.Services;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using TalentMatrix.Authorization.Users;
using TalentMatrix.Extension;
using TalentMatrix.Org.Dto;

namespace TalentMatrix.Org
{
    public class UserExtension
    {
        public string FullName { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string InnerCode { get; set; }
    }
    public class OrgAppService : IApplicationService, IOrgAppService
    {
        private readonly IAbpSession _abpSession;
        public readonly IRepository<Organization> _reposity;
        public readonly IRepository<OrgUser> _orgReposity;
        public readonly IRepository<User, long> _userReposity;
        public readonly MyAppSession _myAppSession;
        public OrgAppService(IRepository<Organization> reposity,
            IRepository<OrgUser> orgReposity,
             IRepository<User, long> userReposity,
        MyAppSession myAppSession,
             IAbpSession abpSession)
        {
            _orgReposity = orgReposity;
            _abpSession = abpSession;
            _reposity = reposity;
            _myAppSession = myAppSession;
            _userReposity = userReposity;
        }

        public List<UserExtension> getAll()
        {

            var query = from user in _userReposity.GetAll()
                        join orguser in _orgReposity.GetAll()
                        on user.Id equals orguser.UserId
                        join org in _reposity.GetAll()
                        on orguser.OrgId equals org.Id
                        select new UserExtension
                        {
                            FullName = user.FullName,
                            UserId = user.Id,
                            UserName = user.UserName,
                            InnerCode = org.InnerCode
                        };
            return query.ToList();
        }

        public List<UserExtension> getAllsByLinq(string fullname)
        {

            var query = from user in _userReposity.GetAll()
                        join orguser in _orgReposity.GetAll()
                        on user.Id equals orguser.UserId
                        join org in _reposity.GetAll()
                        on orguser.OrgId equals org.Id
                        select new UserExtension
                        {
                            FullName = user.FullName,
                            UserId = user.Id,
                            UserName = user.UserName,
                            InnerCode = org.InnerCode
                        };

            return query.WhereIf(!string.IsNullOrEmpty(fullname), s => s.FullName.Contains(fullname)).ToList();
        }

        public List<UserExtension> getAllsByLambda(string fullname)
        {

            var query = _userReposity.GetAll()
               .Join(
                       _orgReposity.GetAll(),
                       user => user.Id,
                       orguser => orguser.UserId,
                       (user, orguser) => new
                       {
                           orguser.OrgId,
                           user.FullName,
                           UserId = user.Id,
                           user.UserName
                       })
                     .Join(
                        _reposity.GetAll(),
                       user_orguser => user_orguser.OrgId,
                       org => org.Id,
                       (user_orguser, org) => new UserExtension
                       {
                           FullName = user_orguser.FullName,
                           UserId = user_orguser.UserId,
                           UserName = user_orguser.UserName,
                           InnerCode = org.InnerCode
                       });
        return query.ToList();
        }
        public List<Organization> GetOrgList()
        {
            List<Organization> res = _reposity.GetAllList();
            string email = _myAppSession.GetUserEmail();
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
            }
            catch (Exception e)
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
