﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PPPK.Models
{
    public class TravelOrderType
    {
        [Key]
        public long IDTravelOrderType { get; set; }
        public string Type { get; set; }
    }
}