using DDDInPractice.Logic.Common;
using DDDInPractice.Logic.SharedKernel;

namespace DDDInPractice.Logic.Management
{
    public class HeadOffice : AggregateRoot
    {
        public virtual decimal Balance { get; protected set; }
        public virtual Money Cash { get; set; } = Money.None;

        public virtual void ChangeBalance(decimal delta)
        {
            Balance += delta;
        }
    }
}
