using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightNote.Application.Contracts
{
    public interface ITokenValidator
    {
        bool Validate(string token);
    }
}