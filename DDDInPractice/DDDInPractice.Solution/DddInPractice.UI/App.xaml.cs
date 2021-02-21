using DDDInPractice.Logic.Utils;

namespace DddInPractice.Logic.UI
{
    public partial class App
    {
        public App()
        {
            Initer.Init("Server=(localdb)\\mssqllocaldb;Database=DddInPractice;Trusted_Connection=True;");
        }
    }
}
