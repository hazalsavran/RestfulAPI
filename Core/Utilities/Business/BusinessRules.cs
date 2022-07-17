using Core.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics) //params ile Iresult tipinde istenilen kadar metotu dizi şeklinde gönderir, logics iş kurallarıdır
        {
            // iş kurallarını burada yapılandırmamızın sebebi iş katmanındaki managerlarda kod fazlalıklarından ve tekrarlarından kurtulmaktır.
            foreach (var logic in logics)
            {
                if (!logic.Succes)//!logic.Succes => yani iş kuralı başarısız ise 
                {
                    return logic; // bu logic kuralı hatalıdır deyip geri döndürür, örn CheckIfProductNameExists başarısız ise..
                }
                
            }

            return null;
        }

    }
}
