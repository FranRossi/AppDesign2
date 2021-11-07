using ExternalReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities.CustomExceptions;

namespace ExternalReaderTest
{
    [TestClass]
    public class ExceptionTest
    {
        [TestMethod]
        public void CreateWrongFormatException()
        {
            ExternalReaderException newException = new WrongFormatException();
            string expectedMessage = "Error trying to import bugs, check properties restrictions and file formatting.";

            Assert.AreEqual(expectedMessage, newException.Message);
        }

        [TestMethod]
        public void CreateUnableToReadException()
        {
            ExternalReaderException newException = new UnableToReadException();
            string expectedMessage = "Unable to read file, check path or read permissions.";

            Assert.AreEqual(expectedMessage, newException.Message);
        }

        [TestMethod]
        public void CreateParameterException()
        {
            ExternalReaderException newException = new ParameterException();
            string expectedMessage = "Invalid parameters, please check types and make sure not to leave any empty fields.";

            Assert.AreEqual(expectedMessage, newException.Message);
        }

        [TestMethod]
        public void InexistentExternalReaderException()
        {
            InexistentExternalReaderException newException = new InexistentExternalReaderException();
            string expectedMessage = "The entered external reader file name does not exist, contact the admin to add it.";

            Assert.AreEqual(expectedMessage, newException.Message);
        }
    }
}
