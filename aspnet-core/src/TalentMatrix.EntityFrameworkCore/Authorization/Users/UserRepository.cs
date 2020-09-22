using Abp.Data;
using Abp.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TalentMatrix.EntityFrameworkCore;
using TalentMatrix.EntityFrameworkCore.Repositories;

namespace TalentMatrix.Authorization.Users
{
    public class UserRepository : TalentMatrixRepositoryBase<User, long>, IUserRepository
    {
        private readonly IActiveTransactionProvider _transactionProvider;

        public UserRepository(IDbContextProvider<TalentMatrixDbContext> dbContextProvider, IActiveTransactionProvider transactionProvider)
            : base(dbContextProvider)
        {
            _transactionProvider = transactionProvider;
        }

        //TODO: Make async!
        public async Task<List<string>> GetUserNames()
        {
            EnsureConnectionOpen();

            using (var command = CreateCommand("GetUsernames", CommandType.StoredProcedure))
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<string>();

                    while (dataReader.Read())
                    {
                        result.Add(dataReader["UserName"].ToString());
                    }

                    return result;
                }
            }
        }

        public async Task<List<User>> GetUsers()
        {
            string sql = @"Delete from [TalentMatrixDB].[dbo].[Organization] where id = @Id;SELECT * FROM [TalentMatrixDB].[dbo].[AbpUsers];";
            SqlParameter[] parameters = { new SqlParameter("@Id", 55) };

            using (var command = CreateCommand(sql, CommandType.Text, parameters))
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<object>();
                    var res = ToList<User>(dataReader);
                    User user = res.FirstOrDefault();
                    return res;
                }
            }
        }

        public List<T> ToList<T>(DbDataReader dr) where T : class, new()
        {
            var result = new List<T>();
            var properties = typeof(T).GetProperties().ToList();
            while (dr.Read())
            {
                var obj = new T();

                foreach (var property in properties)
                {
                    try
                    {
                        //Oracle字段为大写
                        var id = dr.GetOrdinal(property.Name.ToUpper());
                        if (!dr.IsDBNull(id))
                        {
                            if (dr.GetValue(id) != DBNull.Value)
                            {
                                property.SetValue(obj, dr.GetValue(id));
                            }
                        }
                    }
                    catch
                    {
                    }
                }

                result.Add(obj);
            }
            return result;
        }


        private DbCommand CreateCommand(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = Context.Database.GetDbConnection().CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = GetActiveTransaction();

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }

        private void EnsureConnectionOpen()
        {
            var connection = Context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        private DbTransaction GetActiveTransaction()
        {
            return (DbTransaction)_transactionProvider.GetActiveTransaction(new ActiveTransactionProviderArgs
            {
                {"ContextType", typeof(TalentMatrixDbContext) },
                {"MultiTenancySide", MultiTenancySide }
            });
        }
    }
}
