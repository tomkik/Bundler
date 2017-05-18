using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bundler.BusinessLogic
{
    public class Rules
    {
        private IBundles Bundles;
        private IProducts Products;

        private const string NEW_LINE = "<br />";

        public Rules(IBundles bundles, IProducts products)
        {
            Bundles = bundles;
            Products = products;
        }

        public Bundle RecommendBundle(CustomerInfo info)
        { 
            List<Bundle> bundles = Bundles.GetBundlesHighesValueFirst();

            foreach (Bundle bundle in bundles)
            { 
                bool satisfiesRules = bundle.Rules.Where(r => r.SatisfiesCondition(info)).Count() > 0;

                if (satisfiesRules)
                {
                    return bundle;
                }
            }

            return null;
        }

        public string ValidateCustomBundle(CustomerInfo info, Bundle customBundle)
        {
            List<string> allErrors = new List<string>();

            AddMessageIfNotEmpty(ref allErrors, ValidateAccount(info, customBundle.Account));
            AddMessageIfNotEmpty(ref allErrors, ValidateCards(info, customBundle.Cards));
            AddMessageIfNotEmpty(ref allErrors, ValidateCardsAccountMatch(customBundle.Cards, customBundle.Account));

            return string.Join(NEW_LINE, allErrors.ToArray());
        }

        public string ValidateCardsAccountMatch(List<Products.Card> cards, Products.Account account)
        {
            BusinessLogic.Products.Account[] debitCardAccounts = new Products.Account[]
            {
                BusinessLogic.Products.Account.CURRENT_ACCOUNT,
                BusinessLogic.Products.Account.CURRENT_ACCOUNT_PLUS,
                BusinessLogic.Products.Account.STUDENT_ACCOUNT,
                BusinessLogic.Products.Account.PENSIONER_ACCOUNT
            };

            if (cards != null && cards.Contains(BusinessLogic.Products.Card.DEBIT_CARD) && !debitCardAccounts.Contains(account))
            {
                return BusinessLogic.Products.Card.DEBIT_CARD + " validation error:" + NEW_LINE + "-Bundle must include one of " +
                    string.Join(", ", debitCardAccounts.Select(a => a.ToString()).ToArray());
            }

            return null;
        }

        public string ValidateAccount(CustomerInfo info, Products.Account account)
        {
            if (account == BusinessLogic.Products.Account.NONE)
            {
                return null;
            }

            Products.AccountRule accountRule = Products.GetAccountRule(account);
            List<string>[] errors = accountRule.Rules.Select(rule => rule.GetValidationErrors(info)).ToArray();

            string concatenatedLeastErrors = GetLeastErrorsString(errors);

            return string.IsNullOrEmpty(concatenatedLeastErrors) ? null : account + " validation errors:" + concatenatedLeastErrors;
        }

        public string ValidateCards(CustomerInfo info, List<Products.Card> cards)
        {
            if (cards == null || cards.Count() == 0)
            {
                return null;
            }

            List<string> errorsPerCard = new List<string>();

            foreach(Products.Card card in cards)
            {
                Products.CardRule cardRule = Products.GetCardRule(card);
                List<string>[] errors = cardRule.Rules.Select(rule => rule.GetValidationErrors(info)).ToArray();

                string concatenatedLeastErrors = GetLeastErrorsString(errors);

                if(!string.IsNullOrEmpty(concatenatedLeastErrors))
                {
                    errorsPerCard.Add(card + " validation errors:" + concatenatedLeastErrors);
                }
            }

            return string.Join(NEW_LINE, errorsPerCard.ToArray());
        }

        public string GetLeastErrorsString(List<string>[] errors)
        {
            string separator = NEW_LINE + "-";

            List<string> LeastErrors = errors.OrderBy(e => e.Count()).First();

            return LeastErrors.Count() == 0 ? null : separator + string.Join(separator, LeastErrors.ToArray());
        }

        public void AddMessageIfNotEmpty(ref List<string> list, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                list.Add(message);
            }
        }
    }
}
