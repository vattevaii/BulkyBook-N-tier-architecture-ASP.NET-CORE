using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Infrastructure.IRepository;
using BulkyBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext _context;

        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }

        public IImageRepository Images { get; private set; }

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            Product = new ProductRepository(_context);
            Images = new ImageRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
