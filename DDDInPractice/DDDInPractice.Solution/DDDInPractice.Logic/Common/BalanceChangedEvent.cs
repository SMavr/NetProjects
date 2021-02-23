namespace DDDInPractice.Logic.Common
{
    public class BalanceChangedEvent : IDomainEvent
    {
        public decimal Delta { get; set; }

        public BalanceChangedEvent(decimal delta)
        {
            Delta = delta;
        }
    }
}
