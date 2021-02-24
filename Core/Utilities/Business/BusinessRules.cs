using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    // tamam çıplak class kalmasın dedik ama bu kadarı da over-design
    public class BusinessRules // metod static olduğundan class olmasa da olur
    {
        public static IResult Run(params IResult[] logics) // params ---> istediğimiz kadar parametre, logics --> iş kuralı
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                }
            }

            return null;
        }
    }
}
