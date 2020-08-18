using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace TalentMatrix.Org
{
    public class MyService
    {
        private readonly IAbpSession _session;

        public MyService(IAbpSession session)
        {
            _session = session;
        }

        public void Test()
        {
            using (_session.Use(42, null))
            {
                var tenantId = _session.TenantId; //42
                var userId = _session.UserId; //null
            }
        }
    }
}
