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

            decimal amountWithCommission = amount + amount * 0.01m;
            MoneyCharged += amountWithCommission;
        }
    }
}
