using System;
namespace er_nea.Models
{
    public class Account
    {
        public Account()
        {

        }

        public double Balance { get; set; }

        public int Accoutnumber { get; set; }

        public int SubAccountNumber { get; set; }

        public string Mfid { get; set; }

        public string Cls { get; set; }

        public DateTime NavDate { get; set; }

        /// <summary>
        /// Negativer Ertragsausgleich. Berechnet durch Anwendung
        /// </summary>
        /// <value>The nea.</value>
        /// 
        ///
        public double Nea { get; set; } = 0;

        public double NeaAcc { get; set; } = 0;
    }
}
