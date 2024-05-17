﻿using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Service.Commands.DeleteService
{
    public record DeleteServiceCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
