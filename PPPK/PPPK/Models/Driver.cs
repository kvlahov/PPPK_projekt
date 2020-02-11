using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PPPK.Models
{
    public class Driver
    {
        [Key]
        public long IDDriver { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DriversLicence { get; set; }
    }
}