using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Infrastructure.IRepository;
using BulkyBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Infrastructure.Repository
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        DatabaseContext _context;

        public ProductRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public void Add(ProductAddModel productAddModel)
        {
            throw new NotImplementedException();
        }
    }
}
