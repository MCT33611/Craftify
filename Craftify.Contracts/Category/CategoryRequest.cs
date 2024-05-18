﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Contracts.Category
{
    public record CategoryRequest(
        Guid Id,

        string CategoryName,

        Decimal? MinmumPrice,

        Decimal? MaximumPrice);
}