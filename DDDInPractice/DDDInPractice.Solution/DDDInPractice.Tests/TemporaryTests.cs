using DDDInPractice.Logic.Management;
using DDDInPractice.Logic.SnackMachines;
using DDDInPractice.Logic.Utils;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Xunit;

using static DDDInPractice.Logic.SharedKernel.Money;

namespace DDDInPractice.Tests
{
    public class TemporaryTests
    {
        [Fact]
        public void Test()
        {
            
            SessionFactory.Init("Server=(localdb)\\mssqllocaldb;Database=DddInPractice;Trusted_Connection=True;");

            using (ISession session = SessionFactory.OpenSession())
            {
                long id = 1;
                var snackMachine = session.Get<SnackMachine>(id);
            }
        }

        [Fact]
        public void TestRepository()
        {

            SessionFactory.Init("Server=(localdb)\\mssqllocaldb;Database=DddInPractice;Trusted_Connection=True;");

            SnackMachineRepository repository = new SnackMachineRepository();
            SnackMachine snackMachine = repository.GetById(1);

            HeadOfficeInstance.Init();
            HeadOffice headOffice = HeadOfficeInstance.Instance;
        }
    }
}
