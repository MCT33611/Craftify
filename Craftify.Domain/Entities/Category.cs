using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? Picture {  get; set; }


        public Decimal? MinmumPrice { get; set; }

        public Decimal? MaximumPrice { get; set; }


    }
}
