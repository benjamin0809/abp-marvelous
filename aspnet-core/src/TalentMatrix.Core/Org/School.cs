using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TalentMatrix.Org
{
    public class School: Entity<int>
    {
        public string Name { get; set; }

        public string Address { get; set; }
        /// <summary>
        /// 学校里面的学生们
        /// </summary>
        public List<Student> Students { get; set; }
    }
}
