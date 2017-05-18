using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bundler.BusinessLogic
{
    public interface IProducts
    {
        Products.AccountRule GetAccountRule(Products.Account account);
        Products.CardRule GetCardRule(Products.Card card);
    }
}
