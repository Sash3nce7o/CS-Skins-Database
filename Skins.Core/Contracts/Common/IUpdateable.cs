using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skins.Core.Contracts.Common
{
    public interface IUpdateable<TUpdateModel> where TUpdateModel : class
    {
        void Update(TUpdateModel model);
    }
}
