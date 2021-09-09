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

        [TestMethod]
        public void CreateDescription()
        {
            Bug newBug = new Bug
            {
                description = "On line 67, code won't compile because a parenthesis is missing"
            };
            Assert.AreEqual("On line 67, code won't compile because a parenthesis is missing", newBug.description);
        }

        [TestMethod]
        public void CreateVersion()
        {
            Bug newBug = new Bug
            {
                version = "1.0"
            };
            Assert.AreEqual("1.0", newBug.version);
        }

        [TestMethod]
        public void CreateState()
        {
            Bug newBug = new Bug
            {
                state = BugState.Active
            };
            Assert.AreEqual(BugState.Active, newBug.version);
        }
    }
}
