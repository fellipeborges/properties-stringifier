using NUnit.Framework;

namespace PropertiesStringifier.Tests
{
    public class CoreTests
    {
        private Actress _actress;
        private const string EXCEPTED_STRINGIFIED_TEXT = "Name: Natalie Portman Age: 38 IsDead: False BirthDate: 1981-06-09 DeathDate: null LatestIntervewDatetime: 2019-05-18 12:15:16 MoviesCount: 5 NominationsCount: 3 MainMediaType: Movies";

        [SetUp]
        public void Setup()
        {
            _actress = ActressFactory.GetActress();
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