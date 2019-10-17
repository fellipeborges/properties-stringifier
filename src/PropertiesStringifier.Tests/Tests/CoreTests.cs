using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace PropertiesStringifier.Tests
{
    public class CoreTests
    {
        private Actress _actress;
        private const string EXCEPTED_STRINGIFIED_TEXT = "Name: Natalie Portman Age: 38 IsDead: False BirthDate: 1981-06-09 DeathDate: null LatestIntervewDatetime: 2019-05-18 12:15:16 MoviesCount: 5 NominationsCount: 3 MainMediaType: Movies";

        [SetUp]
        public void Setup()
        {
            _actress = new Actress
            {
                Name = "Natalie Portman",
                Age = 38,
                BirthDate = new DateTime(1981, 06, 09),
                DeathDate = null,
                IsDead = false,
                LatestIntervewDatetime = new DateTime(2019, 05, 18, 12, 15, 16),
                Movies = new string[]
                {
                    "Black Swan",
                    "V for Vendetta",
                    "Thor",
                    "Closer",
                    "Jackie"
                },
                Nominations = new List<string>
                {
                    "Best Supporting Actress for Closer",
                    "Best actress for Black Swan",
                    "Best actress for Jackie"
                },
                MainMediaType = MainMediaType.Movies,
                PersonalInfo = new PersonalInfo // Second level class must be ignored by the stringifier
                {
                    Residence = "Los Angeles, California, U.S.",
                    Citizenship = "Israel / United States",
                    Children = 2,
                    Spouse = "Benjamin Millepied"
                }
            };
        }

        /// <summary>
        /// Assures that the method "StringifyProperties()" returns a string that covers all properties as expected.
        /// </summary>
        [Test]
        public void StringifyMethodTest()
        {
            string stringifiedProperties = _actress.StringifyProperties();
            Assert.AreEqual(EXCEPTED_STRINGIFIED_TEXT, stringifiedProperties);
        }

        /// <summary>
        /// Assures that the Attribute is overriding the ToString() method and is covering all properties as expected.
        /// </summary>
        [Test]
        public void BaseClassTest()
        {
            string stringifiedProperties = _actress.ToString();
            Assert.AreEqual(EXCEPTED_STRINGIFIED_TEXT, stringifiedProperties);
        }

        /// <summary>
        /// Assures that a exception will be handled and not throwed.
        /// </summary>
        [Test]
        public void ExceptionTest()
        {
            Actress obj = null;
            string stringifiedProperties = obj.StringifyProperties();
            Assert.IsTrue(stringifiedProperties.Contains("Failed to stringify properties"));
        }
    }
}