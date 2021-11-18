using FileHandler;
using FileHandlerFactory;
using FileHandlerInterface;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TestUtilities;
using Utilities.CustomExceptions.FileHandler;

namespace FileHandlerFactoryTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
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

        [TestMethod]
        public void GetCompany2ReaderStrategy()
        {
            ReaderFactory factory = new ReaderFactory();
            IFileReaderStrategy expectedStrategy = new Company2Reader();
            string companyName = "Company2";

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

        [TestMethod]
        public void GetCompanyReaderNames()
        {
            ReaderFactory factory = new ReaderFactory();
            IEnumerable<string> expectedNames = new List<string> { "Company1", "Company2" };

            IEnumerable<string> result = factory.GetCompanyReaderNames();

            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedNames, result);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
    }
}
