using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afloeser.Models
{
    public class ValidGroup
    {
        public string GroupName { get; set; }
        public string GroupValue { get; set; }

        public ValidGroup()
        { }

        public ValidGroup(string groupName, string groupValue)
        {
            GroupName = groupName;
            GroupValue = groupValue;
        }
    }
}