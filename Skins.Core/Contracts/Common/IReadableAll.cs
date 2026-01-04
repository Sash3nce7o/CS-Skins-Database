using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skins.Core.Contracts.Common
{
    public interface IReadableAll<TReturnModel> where TReturnModel : class
    {
        IEnumerable<TReturnModel> All();
        IEnumerable<TReturnModel> AllAsNoTracking();
    }
}