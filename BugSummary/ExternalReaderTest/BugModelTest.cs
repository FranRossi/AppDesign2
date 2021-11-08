using ExternalReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalReaderTest
{
    [TestClass]
    public class BugModelTest
    {
        [TestMethod]
        public void CreateName()
        {
            BugModel newBug = new BugModel
            {
                Name = "Error en base de datos"
            };
            Assert.AreEqual("Error en base de datos", newBug.Name);
        }

        [TestMethod]
        public void CreateDescription()
        {
            BugModel newBug = new BugModel
            {
                Description = "Error en base de datos a la hora de cargar nuevos bugs"
            };
            Assert.AreEqual("Error en base de datos a la hora de cargar nuevos bugs", newBug.Description);
        }

        [TestMethod]
        public void CreateVersion()
        {
            BugModel newBug = new BugModel
            {
                Version = "1.2"
            };
            Assert.AreEqual("1.2", newBug.Version);
        }

        [TestMethod]
        public void CreateActive()
        {
            BugModel newBug = new BugModel
            {
                State = BugState.Active
            };
            Assert.AreEqual(BugState.Active, newBug.State);
        }

        [TestMethod]
        public void CreateDateTypeParameter()
        {
            BugModel newBug = new BugModel
            {
                State = BugState.Fixed
            };
            Assert.AreEqual(BugState.Fixed, newBug.State);
        }
    }
}
