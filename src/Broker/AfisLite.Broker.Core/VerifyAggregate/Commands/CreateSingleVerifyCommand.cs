﻿using AfisLite.Broker.Core.VerifyAggregate.Models;
using MediatR;

namespace AfisLite.Broker.Core.VerifyAggregate.Commands
{
    public class CreateSingleVerifyCommand : IRequest<VerifyResponse>
    {
        public int CandidatePersonId { get; set; }
        public string ProbeBase64 { get; set; }
    }
}
