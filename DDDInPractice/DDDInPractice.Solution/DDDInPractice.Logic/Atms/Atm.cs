using DDDInPractice.Logic.Common;
using DDDInPractice.Logic.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDInPractice.Logic.Atms
{
    public class Atm : AggregateRoot
    {
        private const decimal CommissionRate = 0.01m;

        public virtual Money MoneyInside { get; protected set; } = Money.None;
        public virtual decimal MoneyCharged { get; protected set; }

        public void LoadMoney(Money money)
        {
            MoneyInside += money;
        }

        public virtual void TakeMoney(decimal amount)
        {
            Money output = MoneyInside.Allocate(amount);
            MoneyInside -= output;

            decimal amountWithCommission = CalculateAmountWithCommission(amount);
            MoneyCharged += amountWithCommission;
        }

        private decimal CalculateAmountWithCommission(decimal amount)
        {
            decimal commission = amount * CommissionRate;
            decimal lessThanCent = commission % 0.01m;
            if (lessThanCent > 0)
            {
                commission = commission - lessThanCent + 0.01m;
            }

            return amount + commission;
        }
    }
}
