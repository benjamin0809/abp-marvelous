using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TalentMatrix.Org
{
    public class Student: Entity<int>
    {
        public string Name { get; set; }
        /// <summary>
        /// 学生所在的学校
        /// </summary>
        public School School { get; set; }
    }
}
