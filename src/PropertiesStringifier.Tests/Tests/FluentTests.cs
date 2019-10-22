using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace PropertiesStringifier.Tests
{
    public class FluentTests
    {
        private Actress _actress;
        private const string EXCEPTED_STRINGIFIED_TEXT = "Name: Natalie Portman Age: 38";

        [SetUp]
        public void Setup()
        {
            _actress = ActressFactory.GetActress();
        }

        /// <summary>
        /// Assures that the fluent methods "StringifyProperty()" and "AndProperty()" returns a string that covers only the selected properties.
        /// </summary>
        [Test]
        public void SpecifyPropertiesToConsiderTest()
        {
            string stringifiedProperties = _actress
                .StringifyThisProperty(x => x.Name)
                .AndThisProperty(x => x.Age)
                .ToString();
            Assert.AreEqual(EXCEPTED_STRINGIFIED_TEXT, stringifiedProperties);
        }
    }
}