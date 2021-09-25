using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain;
using Utilities;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Comparers;

namespace DataAccessTest
{
    [TestClass]
    public class BugRepositoryTest
    {

        private readonly DbConnection _connection;
        private readonly BugRepository _bugRepository;
        private readonly BugSummaryContext _bugSummaryContext;
        private readonly DbContextOptions<BugSummaryContext> _contextOptions;

        public BugRepositoryTest()
        {
            this._connection = new SqliteConnection("Filename=:memory:");
            this._contextOptions = new DbContextOptionsBuilder<BugSummaryContext>().UseSqlite(this._connection).Options;
            this._bugSummaryContext = new BugSummaryContext(this._contextOptions);
            this._bugRepository = new UserRepository(this._bugSummaryContext);
        }


    }
}
