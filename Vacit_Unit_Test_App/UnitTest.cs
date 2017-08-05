using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Vacit;
using Vacit.Converter;

namespace Vacit_Unit_Test_App
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AgeStringTest01()
        {
            DateTime testBirthDate = new DateTime(2005, 2, 1);
            Assert.AreEqual("12 år 6 måneder", DateConverter.AgeToStringDanish(testBirthDate));
        }

        [TestMethod]
        public void AgeStringTest02()
        {
            DateTime testBirthDate = new DateTime(2016, 6, 14);
            Assert.AreEqual("1 år 2 måneder", DateConverter.AgeToStringDanish(testBirthDate));
        }

        [TestMethod]
        public void AgeStringTest03()
        {
            DateTime testBirthDate = new DateTime(2016, 6, 15);
            Assert.AreEqual("1 år 1 måneder", DateConverter.AgeToStringDanish(testBirthDate));
        }

        [TestMethod]
        public void AgeStringTest04()
        {
            DateTime testBirthDate = new DateTime(2016, 7, 14);
            Assert.AreEqual("1 år 1 måneder", DateConverter.AgeToStringDanish(testBirthDate));
        }

        [TestMethod]
        public void AgeStringTest05()
        {
            DateTime testBirthDate = new DateTime(2016, 7, 15);
            Assert.AreEqual("1 år", DateConverter.AgeToStringDanish(testBirthDate));
        }

        [TestMethod]
        public void AgeStringTest06()
        {
            DateTime testBirthDate = new DateTime(2016, 8, 1);
            Assert.AreEqual("1 år", DateConverter.AgeToStringDanish(testBirthDate));
        }

        [TestMethod]
        public void AgeStringTest07()
        {
            DateTime testBirthDate = new DateTime(2016, 8, 14);
            Assert.AreEqual("1 år", DateConverter.AgeToStringDanish(testBirthDate));
        }

        [TestMethod]
        public void AgeStringTest08()
        {
            DateTime testBirthDate = new DateTime(2016, 8, 15);
            Assert.AreEqual("11 måneder", DateConverter.AgeToStringDanish(testBirthDate));
        }

        [TestMethod]
        public void AgeStringTest09()
        {
            DateTime testBirthDate = new DateTime(2017, 6, 1);
            Assert.AreEqual("2 måneder", DateConverter.AgeToStringDanish(testBirthDate));
        }

        [TestMethod]
        public void AgeStringTest10()
        {
            DateTime testBirthDate = new DateTime(2017, 6, 2);
            Assert.AreEqual("8 uger", DateConverter.AgeToStringDanish(testBirthDate));
        }

        [TestMethod]
        public void AgeStringTest11()
        {
            DateTime testBirthDate = new DateTime(2017, 7, 17);
            Assert.AreEqual("2 uger", DateConverter.AgeToStringDanish(testBirthDate));
        }

        [TestMethod]
        public void AgeStringTest12()
        {
            DateTime testBirthDate = new DateTime(2017, 7, 18);
            Assert.AreEqual("13 dage", DateConverter.AgeToStringDanish(testBirthDate));
        }

        [TestMethod]
        public void AgeStringTest13()
        {
            DateTime testBirthDate = new DateTime(2017, 7, 30);
            Assert.AreEqual("2 dage", DateConverter.AgeToStringDanish(testBirthDate));
        }

        [TestMethod]
        public void AgeStringTest14()
        {
            DateTime testBirthDate = new DateTime(2017, 7, 31);
            Assert.AreEqual("1 dag", DateConverter.AgeToStringDanish(testBirthDate));
        }

        [TestMethod]
        public void AgeStringTest15()
        {
            DateTime testBirthDate = new DateTime(2017, 8, 1);
            Assert.AreEqual("0 dage", DateConverter.AgeToStringDanish(testBirthDate));
        }

        [TestMethod]
        public void AgeStringTest16()
        {
            DateTime testBirthDate = new DateTime(2017, 6, -2);
            Assert.AreEqual("8 uger", DateConverter.AgeToStringDanish(testBirthDate));
        }




    }
}
