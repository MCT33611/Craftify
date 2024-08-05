using Craftify.Application.Reviews.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace Craftify.Application.Reviews.Queries.GetReviewsByCustomerId
{
    public class GetReviewsByCustomerIdQuery : IRequest<List<CustomerReviewDto>>
    {
        public Guid CustomerId { get; set; }
    }
}