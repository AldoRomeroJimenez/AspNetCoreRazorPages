using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.ViewModels.Management
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            Users = new List<string>();
        }
        [Required]
        [Display(Description ="Nombre")]
        public string RoleName { get; set; }
        [Required]
        [Display(Description = "Descripcion")]
        public string Description { get; set; }
        
        public IList<string> Users { get; set; }
    }
}
