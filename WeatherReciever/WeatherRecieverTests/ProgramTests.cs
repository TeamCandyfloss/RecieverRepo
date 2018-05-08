using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherReciever;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherReciever.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "Du kan ikke indsætte en Null værdi")]
        public void MainTest()
        {
            string s = null;
            //arrange
            Program p = new Program();
            //act
         int test = p.ToDataBase(s);
            //assert
            Assert.AreEqual(0, test);
        }

        [TestMethod()]
        public void ToDataBaseTest()
        {

            string s = "9000";
            //arrange
            Program p = new Program();
            //act
            int test = p.ToDataBase(s);
            //assert
            Assert.AreEqual(1, test);
        }
    }
}