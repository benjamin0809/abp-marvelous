using System;
using System.Collections.Generic;
using System.Text;
using TalentMatrix.Org.Dto;

namespace TalentMatrix.Org
{
    public interface IOrgAppService
    {
        public List<Organization> GetOrgList();
        public int CreateOrganization(OrganizationDto dto);
    }
}
