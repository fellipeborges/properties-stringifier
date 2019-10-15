using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace PropertiesStringifier.Tests
{
    public class StringifyTests
    {
        [Test]
        public void StringifyTest()
        {
            var obj = new Actress
            {
                Name = "Natalie Portman",
                Age = 38,
                BirthDate = new DateTime(1981, 06, 09),
                DeathDate = null,
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
                MainMediaType = MainMediaTypeEnum.Movies,
                PersonalInfo = new PersonalInfo // Second level class must be ignored by the stringifier
                {
                    Residence = "Los Angeles, California, U.S.",
                    Citizenship = "Israel / United States",
                    Children = 2,
                    Spouse = "Benjamin Millepied"
                }
            };

            string stringifiedProperties = obj.StringifyProperties();
            Assert.AreEqual("Name: Natalie Portman Age: 38 IsDead: False BirthDate: 1981-06-09 DeathDate: null MoviesCount: 5 NominationsCount: 3 MainMediaType: Movies", stringifiedProperties);
        }
    }

    class Actress
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsDead => DeathDate != null;
        public DateTime BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public string[] Movies { get; set; }
        public List<string> Nominations { get; set; }
        public MainMediaTypeEnum MainMediaType { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
    }

    class PersonalInfo
    {
        public string Residence { get; set; }
        public int Children { get; internal set; }
        public string Spouse { get; internal set; }
        public string Citizenship { get; internal set; }
    }

    enum MainMediaTypeEnum
    {
        Movies = 1,
        TvSeries = 2
    }
}