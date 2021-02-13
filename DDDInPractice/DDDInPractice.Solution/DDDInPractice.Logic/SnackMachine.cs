namespace DDDInPractice.Logic
{
    public sealed class SnackMachine
    {
        public Money MoneyInside { get; set; }

        public Money MoneyInTransaction { get; set; }

        public void InsertMoney(Money money)
        {
            MoneyInTransaction += money;
        }

        public void ReturnMoney()
        {
            // MoneyInTransaction = 0
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
