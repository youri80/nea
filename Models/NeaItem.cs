using System;
namespace er_nea.Models
{
    public class NeaItem
    {
        public NeaItem()
        {
        }


        public DateTime PrevSellNavDate { get; set; }

        public DateTime SellNavDate { get; set; }

        public string Mfid { get; set; }

        public string Cls { get; set; }

        public double ShareParts { get; set}

        public double PrevShareParts { get; set}
    }
}
