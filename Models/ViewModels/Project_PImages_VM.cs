using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FL_ACME.Models.ViewModels
{
    public class Project_PImages_VM
    {
        public Project_Property ProjectProperty { get; set; }

        public List<Project_Images_Property> ProjectImageList { get; set; }

    }
}