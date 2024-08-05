using Craftify.Application.Reviews.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Reviews.Queries.GetReviewById
{
    public class GetReviewByIdQuery : IRequest<ReviewDetailVm>
    {
        public Guid Id { get; set; }
    }}
