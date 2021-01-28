using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MitSuLån.Models
{
    public class SUData
    {
        public SUData()
        {
            Rente = 4.0 / 12.0 / 100.0;
            TillægsRente = 1.0 / 12.0 / 100.0;
            Diskonto = 0.0;
        }

        //Bruger Defineret
        public int AntalMåneder { get; set; }
        public double MånedligSU { get; set; }
        public int MånederFørAfbetaling { get; set; }
        public int MåndeligAfbetaling { get; set; }
        public int AntalTilbageBetalingsMåneder { get; set; } = 0;
        public double GennemsnitligAfbetaling { get; set; }
        public bool ClientSideChecker { get; set; } = true;

        //Renter
        public double Rente { get; set; } 
        public double TillægsRente { get; set; }
        public double Diskonto { get; set; }
    }
}
