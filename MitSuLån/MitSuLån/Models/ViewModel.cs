using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MitSuLån.Models
{
    public class ViewModel
    {
        public DisplayItem Studie { get; set; } = new DisplayItem();
        public DisplayItem Mellem { get; set; } = new DisplayItem();
        public DisplayItem Tilbagebetaling { get; set; } = new DisplayItem();
        public SUData SUData { get; set; } = new SUData();
        public double SamletLån { get; set; }
        public double GebyrAfbetaling { get; set; }
    }
}
