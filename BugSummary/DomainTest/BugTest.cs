using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class BugTest
    {
        [TestMethod]
        public void CreateId()
        {
            Bug newBug = new Bug
            {
                id = 154
            };
            Assert.AreEqual(154, newBug.id);
        }

        [TestMethod]
        public void CreateName()
        {
            Bug newBug = new Bug
            {
                name = "Missing parenthesis"
            };
            Assert.AreEqual("Missing parenthesis", newBug.name);
        }
    }
}
