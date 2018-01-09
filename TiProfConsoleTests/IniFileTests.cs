using Microsoft.VisualStudio.TestTools.UnitTesting;
using TiProfConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TiProfConsole.Tests
{
    [TestClass()]
    public class IniFileTests
    {
        [TestMethod()]
        public void LoadTest()
        {
            IniFile iniFile = IniFile.Load();

            Assert.AreEqual(IniFile.PATH_TO_PROFILE, iniFile.PathToProfile);
            Assert.AreEqual(IniFile.PATH_TO_SAVE, iniFile.PathToSave);
        }

        [TestMethod()]
        public void LoadProfileTest()
        {
            IniFile iniFile = IniFile.Load();

            Profile profile = Profile.Load(iniFile);
            profile.Save();
            
            // 5 - количество профилей на компьютере
            Assert.AreEqual(5, profile.ListIniStruct.Count);
            //Assert.IsTrue(File.Exists(iniFile.PathToSave + "\\dima.csv"));
        }
    }
}