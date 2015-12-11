using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using log4net.Core;
using NUnit.Framework;

namespace WordbrainHelper.Tests
{


    [SetUpFixture]
    public class AllTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            BasicConfigurator.Configure();
            
            


            //((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = Level.Info;
            //((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
     

            LogManager.Shutdown();
        }


    }
}
