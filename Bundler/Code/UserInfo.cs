using System;
using System.Collections.Generic;
using System.Linq;
using Bundler.BusinessLogic;

namespace Bundler.Code
{
    public class UserInfo
    {
        public enum AgeRange
        {
            UNDEFINED = 0,
            ZERO_TO_SEVENTEEN,
            EIGHTEEN_TO_SIXTY_FOUR,
            SIXTY_FIVE_PLUS,
        }

        public enum IncomeRange
        {
            UNDEFINED = 0,
            ZERO,
            ONE_TO_TWELVE_THOUSAND,
            TWELVE_THOUSAND_AND_ONE_TO_FORTY_THOUSAND,
            FORTY_THOUSAND_AND_ONE_PLUS
        }

        public AgeRange Age { get; set; }

        public CustomerInfo.PersonStatus Status { get; set; }

        public IncomeRange Income { get; set; }

        public Products.Account Account { get; set; }

        public List<Products.Card> Cards { get; set; }

        public CustomerInfo ConvertToCustomerInfo()
        {
            return new CustomerInfo
            {
                Age = GetAge(this.Age),
                Status = this.Status,
                Income = GetIncome(this.Income),
            };
        }

        public Bundle ConvertToBundle()
        {
            return new Bundle
            {
                Account = this.Account,
                Cards = this.Cards,
            };
        }

        private Range GetIncome(IncomeRange range)
        {
            switch (range)
            {
                case IncomeRange.ZERO:
                    return new Range { From = 0, To = 0 };
                case IncomeRange.ONE_TO_TWELVE_THOUSAND:
                    return new Range { From = 1, To = 12000 };
                case IncomeRange.TWELVE_THOUSAND_AND_ONE_TO_FORTY_THOUSAND:
                    return new Range { From = 12001, To = 40000 };
                case IncomeRange.FORTY_THOUSAND_AND_ONE_PLUS:
                    return new Range { From = 40001, To = int.MaxValue };
                default:
                    return new Range { From = 0, To = 0 };
            }
        }

        private Range GetAge(AgeRange range)
        {
            switch (range)
            {
                case AgeRange.ZERO_TO_SEVENTEEN:
                    return new Range { From = 0, To = 17 };
                case AgeRange.EIGHTEEN_TO_SIXTY_FOUR:
                    return new Range { From = 18, To = 64 };
                case AgeRange.SIXTY_FIVE_PLUS:
                    return new Range { From = 65, To = int.MaxValue };
                default:
                    return new Range { From = 0, To = 0 };
            }
        }
    }
}