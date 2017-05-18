using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bundler.BusinessLogic
{
    public class CustomerInfo : ICustomerInfo
    {
        public enum PersonStatus
        {
            UNDEFINED = 0,
            STUDENT,
            OTHER,
        }

        public IRange Age;
        public IRange Income;
        public PersonStatus Status;

        public const string AgeMustBeGreaterThan = "Must be older";
        public const string AgeMustBeLessThan = "Must be younger";
        public const string IncomeMustBeGreaterThan = "Income too low";
        public const string IncomeMustBeLessThan = "Income too high";
        public const string PersonMustBe = "Person must be ";

        public List<string> GetValidationErrors(CustomerInfo condition)
        {
            List<string> errors = new List<string>();

            if (!this.Age.WithinLowerBounds(condition.Age.From))
            {
                errors.Add(AgeMustBeGreaterThan);
            }
            if (!this.Age.WithinUpperBounds(condition.Age.To))
            {
                errors.Add(AgeMustBeLessThan);
            }
            if (!this.Income.WithinLowerBounds(condition.Income.From))
            {
                errors.Add(IncomeMustBeGreaterThan);
            }
            if (!this.Income.WithinUpperBounds(condition.Income.To))
            {
                errors.Add(IncomeMustBeLessThan);
            }
            if (this.Status != condition.Status)
            {
                errors.Add(PersonMustBe + this.Status);
            }

            return errors;
        }

        public bool SatisfiesCondition(CustomerInfo condition)
        {
            return GetValidationErrors(condition).Count() == 0;
        }
    }
}
