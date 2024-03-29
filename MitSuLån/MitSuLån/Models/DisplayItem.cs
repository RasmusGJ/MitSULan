﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MitSuLån.Models
{
    public class DisplayItem
    {
        public double EffektivKredit { get; set; } = 1.0;
        public double SamletLån { get; set; }
        public double Gebyr { get; set; } = 1.0;
        public double Piechartdata { get; set; } = 1.0;
    }
}
