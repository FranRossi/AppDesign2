using FileHandler;
using FileHandlerFactory;
using FileHandlerInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using Utilities.CustomExceptions;

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

            TestExceptionUtils.Throws<CompanyIsNotRegisteredException>(
                () => factory.GetStrategy(companyName), "The entered company is not registered on the API."
            );
        }
    }
}
