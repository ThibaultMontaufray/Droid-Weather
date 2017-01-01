
using System;
using NUnit.Framework;
using Droid_weather;
using System.Windows.Forms;

namespace UnitTestProject
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void TestUTRuns()
        {
            Assert.IsTrue(true);
        }
        [Test]
        public void Test_meteo_create()
        {
            try
            {
                Meteo meteo = new Droid_weather.Meteo();
                Assert.IsTrue(true);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_demo()
        {
            try
            {
                FormWeather form = new FormWeather();
                Assert.IsTrue(true);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_parser_web()
        {
            try
            {
                ParserMeteo pm = new ParserMeteo();
                Assert.IsTrue(true);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
    }
}
