using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Domain.Entities
{
    public class ServicePictures
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey(nameof(Service))]
        public Guid ServiceId { get; set; }

        public Service Service { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;

    }
}
