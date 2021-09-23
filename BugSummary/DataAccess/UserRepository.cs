using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class UserRepository
    {
        private readonly DbContext _context;

        public UserRepository(DbContext context)
        {
            this._context = context;

        }
    }
}