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
    internal class ImageRepository: Repository<Image>, IImageRepository
    {
        DatabaseContext _context;
        public ImageRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
