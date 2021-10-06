using FileHandler;
using FileHandlerFactory;
using FileHandlerInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            string companyName = "Company1";

            IFileReaderStrategy strategy = factory.GetStrategy(companyName);

            Assert.IsTrue(expectedStrategy.GetType() == strategy.GetType());
        }
    }
}
