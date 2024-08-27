using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Model;

namespace webapi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}