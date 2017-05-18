using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bundler.BusinessLogic
{
    public class Bundle
    {
        public enum Name
        {
            JUNIOR_SAVER,
            STUDENT,
            CLASSIC,
            CLASSIC_PLUS,
            GOLD,
        }

        public Name BundleName { get; set; }
        public Products.Account Account { get; set; }
        public List<Products.Card> Cards { get; set; }
        public int Value { get; set; }

        public List<ICustomerInfo> Rules { get; set; }
    }

    public class Bundles : IBundles
    {
        private List<Bundle> Offers = new List<Bundle>()
        {
            new Bundle()
            {
                BundleName = Bundle.Name.JUNIOR_SAVER,
                Account = Products.Account.JUNIOR_SAVER_ACCOUNT,
                Cards = new List<Products.Card>(){},
                Value = 0,

                Rules = new List<ICustomerInfo>()
                {
                    new CustomerInfo
                    {
                        Age = new Range
                        {
                             From = 0,
                             To = 17,
                        },
                        Income = new Range
                        {
                            From = 0,
                            To = int.MaxValue,
                        },
                        Status = CustomerInfo.PersonStatus.STUDENT,
                    },
                    new CustomerInfo
                    {
                        Age = new Range
                        {
                             From = 0,
                             To = 17,
                        },
                        Income = new Range
                        {
                            From = 0,
                            To = int.MaxValue,
                        },
                        Status = CustomerInfo.PersonStatus.OTHER,
                    },
                },
            },
            new Bundle()
            {
                BundleName = Bundle.Name.STUDENT,
                Account = Products.Account.STUDENT_ACCOUNT,
                Cards = new List<Products.Card>(){Products.Card.DEBIT_CARD, Products.Card.CREDIT_CARD},
                Value = 0,

                Rules = new List<ICustomerInfo>()
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
            new Bundle()
            {
                BundleName = Bundle.Name.CLASSIC,
                Account = Products.Account.CURRENT_ACCOUNT,
                Cards = new List<Products.Card>(){Products.Card.DEBIT_CARD},
                Value = 1,

                Rules = new List<ICustomerInfo>()
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
                            From = 1,
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
                            From = 1,
                            To = int.MaxValue,
                        },
                        Status = CustomerInfo.PersonStatus.STUDENT,
                    },
                },
            },
            new Bundle()
            {
                BundleName = Bundle.Name.CLASSIC_PLUS,
                Account = Products.Account.CURRENT_ACCOUNT,
                Cards = new List<Products.Card>(){Products.Card.DEBIT_CARD, Products.Card.CREDIT_CARD},
                Value = 2,

                Rules = new List<ICustomerInfo>()
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
            new Bundle()
            {
                BundleName = Bundle.Name.GOLD,
                Account = Products.Account.CURRENT_ACCOUNT_PLUS,
                Cards = new List<Products.Card>(){Products.Card.DEBIT_CARD, Products.Card.GOLD_CREDIT_CARD},
                Value = 3,

                Rules = new List<ICustomerInfo>()
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

        public List<Bundle> GetBundlesHighesValueFirst()
        {
            return Offers.OrderByDescending(b => b.Value).ToList();
        }
    }
}
