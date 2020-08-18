using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace TalentMatrix.Org
{
    public class Organization: FullAuditedEntity

    {
        public string Name { get; set; }
        public string InnerCode { get; set; }
        public string Description { get; set; }
    }
}
