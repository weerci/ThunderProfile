﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using TiProfConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiProfConsole.Tests
{
    [TestClass()]
    public class IniFileTests
    {
        [TestMethod()]
        public void LoadTest()
        {
            IniFile iniFile = IniFile.Load();

            Assert.AreEqual(IniFile.PATH_TO_INI, iniFile.PathToIni);
            Assert.AreEqual(IniFile.PATH_TO_SAVE, iniFile.PathToSave);
        }

        [TestMethod()]
        public void LoadProfileTest()
        {
            IniFile iniFile = IniFile.Load();

            Profile profile = Profile.Load(iniFile);

            Assert.AreEqual(3, profile.ListIniStruct.Count);
        }
    }
}