using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models
{
    public class AttendeeRequirement
    {
        public Gender[] AcceptableGenders { get; set; }

        public byte MinAge { get; set; }

        public byte MaxAge { get; set; }
    }
}
