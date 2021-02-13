namespace DDDInPractice.Logic
{
    public sealed class SnackMachine : Entity
    {
        public Money MoneyInside { get; set; }

        public Money MoneyInTransaction { get; set; }


        public SnackMachine()
        {
            MoneyInside = Money.None;
            MoneyInTransaction = Money.None;
        }

        public void InsertMoney(Money money)
        {
            MoneyInTransaction += money;
        }

        public void ReturnMoney()
        {
            MoneyInTransaction = Money.None;
        }

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;

            // MoneyInTransaction = 0;
            //OneCentCountInTransaction = 0;
            //TenCentCountInTransaction = 0;
            //QuarterCountInTransaction = 0;
            //OneDollarCountInTransaction = 0;
            //FiveDollarCountInTransaction = 0;
            //TwentyDollarCountInTransaction = 0;
        }
    }
}
