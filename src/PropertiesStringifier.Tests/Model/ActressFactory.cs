using System;
using System.Collections.Generic;

namespace PropertiesStringifier.Tests
{
    static class ActressFactory
    {
        /// <summary>
        /// Returns an instance of Actress with all properties filled.
        /// </summary>
        public static Actress GetActress()
        {
            return new Actress
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
    }
}
