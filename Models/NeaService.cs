using System;
using System.Collections.Generic;
using System.Linq;

namespace er_nea.Models
{
    public class NeaService
    {
        public NeaService()
        {
        }

        public List<Account> Balances { get; set; }


        public void Calculate(DateTime navdate, IEnumerable<NeaItem> sellDateList)
        {
            var neaDates = from w in sellDateList
                           where w.SellNavDate <= navdate
                           orderby w.SellNavDate
                           select w;

            var res = CreateNeaResultList(navdate);

            //Durchlaufen der Verkaufstage chronologisch
            foreach (var neadate in neaDates)
            {
                var pct = neadate.PrevShareParts / neadate.ShareParts;

                var tmpbalance =
                    from n in Balances
                    where n.NavDate == neadate.PrevSellNavDate || n.NavDate == neadate.SellNavDate
                    group n by new { n.Accoutnumber, n.SubAccountNumber, n.NavDate } into g
                    select new { g.Key.Accoutnumber, g.Key.SubAccountNumber, g.Key.NavDate, Balance = g.Sum(n => n.Balance) } ;

                // Berechne jedes Konto am Verkaufstag
                foreach (var acc in tmpbalance.Where(n=> n.NavDate == neadate.SellNavDate ))
                {
                    var tmpRes = res.FirstOrDefault(n => n.Accoutnumber == acc.Accoutnumber && n.SubAccountNumber == acc.SubAccountNumber);
  
                    var tmpPrev = tmpbalance.First(n => n.Accoutnumber == acc.Accoutnumber && n.SubAccountNumber == acc.SubAccountNumber && n.NavDate == neadate.PrevSellNavDate);

                    tmpRes.Nea = (tmpPrev.Balance + tmpRes.NeaAcc) * pct; //prüfen
                    tmpRes.NeaAcc += tmpRes.Nea;

                    
                }


            }
        }

        private IEnumerable<Account> CreateNeaResultList(DateTime navdate)
        {
            var res = new List<Account>();

            foreach (var item in res.Select( n => new { n.Accoutnumber, n.SubAccountNumber }).Distinct())
            {
                res.Add(new Account() { 
                    Accoutnumber = item.Accoutnumber
                    ,SubAccountNumber = item.SubAccountNumber
                    ,Nea = 0
                    ,NavDate = navdate
                });
            }
            return res;

        }
    }
}
