using ExternalReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalReaderTest
{
    [TestClass]
    public class ParameterTest
    {
        [TestMethod]
        public void CreateParameterName()
        {
            Parameter newParameter = new Parameter
            {
                Name = "Ruta al servidor"
            };
            Assert.AreEqual("Ruta al servidor", newParameter.Name);
        }

        [TestMethod]
        public void CreateParameterValue()
        {
            Parameter newParameter = new Parameter
            {
                Value = "192.165.2.32"
            };
            Assert.AreEqual("192.165.2.32", newParameter.Value);
        }

        [TestMethod]
        public void CreateStringTypeParameter()
        {
            Parameter newParameter = new Parameter
            {
                Type = ParameterType.String
            };
            Assert.AreEqual(ParameterType.String, newParameter.Type);
        }

        [TestMethod]
        public void CreateIntTypeParameter()
        {
            Parameter newParameter = new Parameter
            {
                Type = ParameterType.Int
            };
            Assert.AreEqual(ParameterType.Int, newParameter.Type);
        }

        [TestMethod]
        public void CreateDateTypeParameter()
        {
            Parameter newParameter = new Parameter
            {
                Type = ParameterType.Date
            };
            Assert.AreEqual(ParameterType.Date, newParameter.Type);
        }
    }
}
