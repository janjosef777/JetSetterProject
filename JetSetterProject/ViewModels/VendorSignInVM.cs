using JetSetterProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetSetterProject.ViewModels
{
    public class VendorSignInVM
    {
        public LoginVM LoginVM { get; set; }
        public RegisterVM RegisterVM { get; set; }
        public Vendor Vendor { get; set; }

    }
}
