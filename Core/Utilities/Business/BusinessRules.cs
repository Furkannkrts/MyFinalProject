using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)//1parametre ile gönderdiğimiz iş kurallarından 
        {
            foreach (var logic in logics) 
            {
                if (!logic.Success)//2başarısız olanı
                {
                    return logic;//3business'a söylüyoruz
                }
            }
            return null;

        }
    }
}
