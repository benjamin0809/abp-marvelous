using System;
using System.Collections.Generic;
using System.Text;

namespace TalentMatrix.Authentication.External.Workflow
{
    public class WorkflowAuthUserInfo: ExternalAuthUserInfo
    {
        public string WorkflowToken { get; set; }
    }
}
