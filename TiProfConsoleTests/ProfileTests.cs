using Microsoft.VisualStudio.TestTools.UnitTesting;
using TiProfConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace TiProfConsole.Tests
{
    [TestClass()]
    public class ProfileTests
    {

        [TestMethod()]
        public void LoadTest()
        {
            List<PostBox> list = Profile.Load();

            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(3, list.Select(n => n.Mails).Count());
        }
    }
}