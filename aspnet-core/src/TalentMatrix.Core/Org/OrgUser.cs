using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace TalentMatrix.Org
{
    public class OrgUser: AuditedEntity
    {
        public int UserId { get; set; }
        public int OrgId { get; set; }
    }
}
