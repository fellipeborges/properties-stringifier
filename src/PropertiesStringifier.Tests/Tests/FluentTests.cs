using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace PropertiesStringifier.Tests
{
    public class FluentTests
    {
        private Actress _actress;

        [SetUp]
        public void Setup()
        {
            _actress = ActressFactory.GetActress();
        }

        /// <summary>
        /// Assures that the fluent methods "StringifyProperty()" and "AndThisProperty()" returns a string that covers only the selected properties.
        /// </summary>
        [Test]
        public void SpecifyPropertiesToConsiderTest()
        {
            const string EXCEPTED_STRINGIFIED_TEXT = "Name: Natalie Portman Age: 38";
            string stringifiedProperties = _actress
                .StringifyThisProperty(x => x.Name)
                .AndThisProperty(x => x.Age)
                .ToString();
            Assert.AreEqual(EXCEPTED_STRINGIFIED_TEXT, stringifiedProperties);
        }

        /// <summary>
        /// Assures that the fluent methods "StringifyPropertiesExcept()" and "AndExceptThisProperty()" returns a string that does not contain the informed properties.
        /// </summary>
        [Test]
        public void SpecifyPropertiesToIgnoreTest()
        {
            const string EXCEPTED_STRINGIFIED_TEXT = "DeathDate: null LatestIntervewDatetime: 2019-05-18 12:15:16 MoviesCount: 5 NominationsCount: 3";
            string stringifiedProperties = _actress
                .StringifyPropertiesExcept(x => x.Name)
                .AndExceptThisProperty(x => x.Age)
                .AndExceptThisProperty(x => x.IsDead)
                .AndExceptThisProperty(x => x.BirthDate)
                .AndExceptThisProperty(x => x.MainMediaType)
                .ToString();
            Assert.AreEqual(EXCEPTED_STRINGIFIED_TEXT, stringifiedProperties);
        }
    }
}