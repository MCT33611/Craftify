﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Domain.Entities
{
    public class Worker
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [AllowNull]
        public User User { get; set; }

        public string ServiceTitle { get; set; } = null!;


        public string? Description { get; set; }   

        public string? CertificationUrl {  get; set; }

        public string? Skills { get; set; }

        public DateTime HireDate { get; set; } = DateTime.UtcNow;

        public decimal PerHourPrice { get; set; }

        public bool Approved { get; set; } = false;


        public string? LogoUrl { get; set; }
        public string? SmallPreviewImageUrl { get; set; }
        public string? MediumPreviewImageUrl { get; set; }
        public string? LargePreviewImageUrl { get; set; }
    }
}
