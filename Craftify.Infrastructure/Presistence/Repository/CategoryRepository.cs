using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using Craftify.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Infrastructure.Presistence.Repository
{
    public class CategoryRepository(
        CraftifyDbContext db
        ) : Repository<Category>(db), ICategoryRepository
    {
        public void Update(Category category)
        {
           db.Categories.Update(category);
        }
    }
}
