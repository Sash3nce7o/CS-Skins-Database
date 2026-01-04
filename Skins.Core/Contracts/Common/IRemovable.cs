using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skins.Core.Contracts.Common
{
    public interface IRemovable
    {
        bool Remove(string id);        
    }
}