using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bundler.BusinessLogic;

namespace Bundler.Repository
{
    public class Logger
    {
        public void LogInfo(CustomerInfo info)
        {
            using (BundlerEntities dataContext = new BundlerEntities())
            {
                var logs = dataContext.Set<BundlerLog>();

                logs.Add(new BundlerLog
                {
                     Timestamp = DateTime.Now,
                     AgeFrom = info.Age.From,
                     AgeTo = info.Age.To,
                     Satus = info.Status.ToString(),
                     IncomeFrom = info.Income.From,
                     IncomeTo = info.Income.To,
                });

                dataContext.SaveChanges();
            }
        }
    }
}
