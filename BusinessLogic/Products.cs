using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bundler.BusinessLogic
{
    public class Products : IProducts
    {
        public enum Account
        {
            NONE = 0,
            CURRENT_ACCOUNT,
            CURRENT_ACCOUNT_PLUS,
            JUNIOR_SAVER_ACCOUNT,
            STUDENT_ACCOUNT,
            PENSIONER_ACCOUNT,
        }

        public enum Card
        {
            DEBIT_CARD = 1,
            CREDIT_CARD,
            GOLD_CREDIT_CARD,
        }

        public class AccountRule
        {
            public Account Account;
            public List<ICustomerInfo> Rules;
        }

        public class CardRule
        {
            public Card Card;
            public List<ICustomerInfo> Rules;
        }

        private List<AccountRule> Accounts = new List<AccountRule>
        {
            new AccountRule
            {
                Account = Account.CURRENT_ACCOUNT,
                Rules = new List<ICustomerInfo>
                {
                    new CustomerInfo
                    {
                         Age = new Range
                         {
                              From = 18,
                              To = int.MaxValue,
                         },
                         Income = new Range
                         {
                              From = 0,
                              To = int.MaxValue,
                         },
                         Status = CustomerInfo.PersonStatus.OTHER,
                    },
                    new CustomerInfo
                    {
                         Age = new Range
                         {
                              From = 18,
                              To = int.MaxValue,
                         },
                         Income = new Range
                         {
                              From = 0,
                              To = int.MaxValue,
                         },
                         Status = CustomerInfo.PersonStatus.STUDENT,
                    },
                },
            },
            new AccountRule
            {
                Account = Account.CURRENT_ACCOUNT_PLUS,
                Rules = new List<ICustomerInfo>
                {
                    new CustomerInfo
                    {
                         Age = new Range
                         {
                              From = 18,
                              To = int.MaxValue,
                         },
                         Income = new Range
                         {
                              From = 40001,
                              To = int.MaxValue,
                         },
                         Status = CustomerInfo.PersonStatus.OTHER,
                    },
                    new CustomerInfo
                    {
                         Age = new Range
                         {
                              From = 18,
                              To = int.MaxValue,
                         },
                         Income = new Range
                         {
                              From = 40001,
                              To = int.MaxValue,
                         },
                         Status = CustomerInfo.PersonStatus.STUDENT,
                    },
                },
            },
            new AccountRule
            {
                Account = Account.JUNIOR_SAVER_ACCOUNT,
                Rules = new List<ICustomerInfo>
                {
                    new CustomerInfo
                    {
                         Age = new Range
                         {
                              From = 0,
                              To = 18,
                         },
                         Income = new Range
                         {
                              From = 0,
                              To = int.MaxValue,
                         },
                         Status = CustomerInfo.PersonStatus.OTHER,
                    },
                    new CustomerInfo
                    {
                         Age = new Range
                         {
                              From = 0,
                              To = 18,
                         },
                         Income = new Range
                         {
                              From = 0,
                              To = int.MaxValue,
                         },
                         Status = CustomerInfo.PersonStatus.STUDENT,
                    },
                },
            },
            new AccountRule
            {
                Account = Account.STUDENT_ACCOUNT,
                Rules = new List<ICustomerInfo>
                {
                    new CustomerInfo
                    {
                         Age = new Range
                         {
                              From = 18,
                              To = int.MaxValue,
                         },
                         Income = new Range
                         {
                              From = 0,
                              To = int.MaxValue,
                         },
                         Status = CustomerInfo.PersonStatus.STUDENT,
                    },
                },
            },
            // improvised as there was no product rule for pensioner account
            new AccountRule
            {
                Account = Account.PENSIONER_ACCOUNT,
                Rules = new List<ICustomerInfo>
                {
                    new CustomerInfo
                    {
                         Age = new Range
                         {
                              From = 65,
                              To = int.MaxValue,
                         },
                         Income = new Range
                         {
                              From = 0,
                              To = int.MaxValue,
                         },
                         Status = CustomerInfo.PersonStatus.OTHER,
                    },
                    new CustomerInfo
                    {
                         Age = new Range
                         {
                              From = 65,
                              To = int.MaxValue,
                         },
                         Income = new Range
                         {
                              From = 0,
                              To = int.MaxValue,
                         },
                         Status = CustomerInfo.PersonStatus.STUDENT,
                    },
                },
            },
        };

        private List<CardRule> Cards = new List<CardRule>
        {
            new CardRule
            {
                Card = Card.DEBIT_CARD,
                Rules = new List<ICustomerInfo>
                {
                    new CustomerInfo
                    {
                         Age = new Range
                         {
                              From = 0,
                              To = int.MaxValue,
                         },
                         Income = new Range
                         {
                              From = 0,
                              To = int.MaxValue,
                         },
                         Status = CustomerInfo.PersonStatus.OTHER,
                    },
                    new CustomerInfo
                    {
                         Age = new Range
                         {
                              From = 0,
                              To = int.MaxValue,
                         },
                         Income = new Range
                         {
                              From = 0,
                              To = int.MaxValue,
                         },
                         Status = CustomerInfo.PersonStatus.STUDENT,
                    },
                },
            },
            new CardRule
            {
                Card = Card.CREDIT_CARD,
                Rules = new List<ICustomerInfo>
                {
                    new CustomerInfo
                    {
                         Age = new Range
                         {
                              From = 18,
                              To = int.MaxValue,
                         },
                         Income = new Range
                         {
                              From = 12001,
                              To = int.MaxValue,
                         },
                         Status = CustomerInfo.PersonStatus.OTHER,
                    },
                    new CustomerInfo
                    {
                         Age = new Range
                         {
                              From = 18,
                              To = int.MaxValue,
                         },
                         Income = new Range
                         {
                              From = 12001,
                              To = int.MaxValue,
                         },
                         Status = CustomerInfo.PersonStatus.STUDENT,
                    },
                },
            },
            new CardRule
            {
                Card = Card.GOLD_CREDIT_CARD,
                Rules = new List<ICustomerInfo>
                {
                    new CustomerInfo
                    {
                         Age = new Range
                         {
                              From = 18,
                              To = int.MaxValue,
                         },
                         Income = new Range
                         {
                              From = 40001,
                              To = int.MaxValue,
                         },
                         Status = CustomerInfo.PersonStatus.OTHER,
                    },
                    new CustomerInfo
                    {
                         Age = new Range
                         {
                              From = 18,
                              To = int.MaxValue,
                         },
                         Income = new Range
                         {
                              From = 40001,
                              To = int.MaxValue,
                         },
                         Status = CustomerInfo.PersonStatus.STUDENT,
                    },
                },
            },
        };

        public AccountRule GetAccountRule(Account account)
        {
            return Accounts.Where(a => a.Account == account).First();
        }

        public CardRule GetCardRule(Card card)
        {
            return Cards.Where(a => a.Card == card).First();
        }
    }
}
