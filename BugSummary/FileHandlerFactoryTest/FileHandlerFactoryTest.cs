using FileHandler;
using FileHandlerFactory;
using FileHandlerInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;

namespace FileHandlerFactoryTest
{
    [TestClass]
    public class FileHandlerFactoryTest
    {
        [TestMethod]
        public void GetCompany1ReaderStrategy()
        {
            ReaderFactory factory = new ReaderFactory();
            IFileReaderStrategy expectedStrategy = new Company1Reader();
            string companyName = "Empresa1";

            IFileReaderStrategy strategy = factory.GetStrategy(companyName);

            Assert.IsTrue(expectedStrategy.GetType() == strategy.GetType());
        }

        [TestMethod]
        public void GetCompany2ReaderStrategy()
        {
            ReaderFactory factory = new ReaderFactory();
            IFileReaderStrategy expectedStrategy = new Company2Reader();
            string companyName = "Empresa2";

            IFileReaderStrategy strategy = factory.GetStrategy(companyName);

            Assert.IsTrue(expectedStrategy.GetType() == strategy.GetType());
        }

        [TestMethod]
        public void GetInexistentCompanyStrategy()
        {
            ReaderFactory factory = new ReaderFactory();
            IFileReaderStrategy expectedStrategy = new Company2Reader();
            string companyName = "non existent company";

            TestExceptionUtils.Throws<ProjectNameIsNotUniqueException>(
                () => projectLogic.Add(projectToAdd), "The project name chosen was already taken, please enter a different name"
            );
            IFileReaderStrategy strategy = factory.GetStrategy(companyName);
        }
    }
}
