using NUnit.Framework;
using System;
using System.Diagnostics;

namespace PropertiesStringifier.Tests
{
    public class PerformanceTests
    {
        /// <summary>
        /// 1 thousand iterations must be executed in less than 100 milliseconds.
        /// </summary>
        [Test]
        public void PerformanceTest()
        {
            const int ITERATIONS = 1000;
            const double MAX_ALLOWED_TIME_IN_MILLISECONDS = 100;

            var actress = new Actress
            {
                Name = "Fernanda Montenegro",
                Age = 90,
                BirthDate = new DateTime(1929, 10, 16)
            };

            var stw = Stopwatch.StartNew();
            for (int i = 0; i < ITERATIONS; i++)
            {
                actress.StringifyProperties();
            }
            stw.Stop();
            Assert.That(stw.ElapsedMilliseconds, Is.LessThan(MAX_ALLOWED_TIME_IN_MILLISECONDS));
        }
    }
}