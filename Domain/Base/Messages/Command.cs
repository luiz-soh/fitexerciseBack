﻿using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Base.Messages
{
    public abstract class Command<TResponse> : Message, IRequest<TResponse>
    {
        public DateTime TimeStamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            TimeStamp = DateTime.UtcNow;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
