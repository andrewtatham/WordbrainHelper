using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;
using NUnit.Framework;

namespace WordbrainHelper.Tests
{


    [SetUpFixture]
    public class AllTests
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            BasicConfigurator.Configure();
        }

    }
}
