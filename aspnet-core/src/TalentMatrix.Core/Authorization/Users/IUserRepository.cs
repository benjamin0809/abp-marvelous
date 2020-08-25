using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TalentMatrix.Authorization.Users
{
    public interface IUserRepository: IRepository<User, long>
    {
        Task<List<string>> GetUserNames();
        public Task<List<object>> GetUsers();
    }
}
