using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace PropertiesStringifier.Tests
{
    public class CoreTests
    {
        
        /// <summary>
        /// Assures that the stringified value is covering all properties as expected.
        /// </summary>
        [Test]
        public void StringifyTest()
        {
            var actress = new Actress
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

            string stringifiedProperties = actress.StringifyProperties();
            Assert.AreEqual("Name: Natalie Portman Age: 38 IsDead: False BirthDate: 1981-06-09 DeathDate: null LatestIntervewDatetime: 2019-05-18 12:15:16 MoviesCount: 5 NominationsCount: 3 MainMediaType: Movies", stringifiedProperties);
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