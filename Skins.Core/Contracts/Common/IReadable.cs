using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Skins.Core.Contracts.Common
{
    public interface IReadable<TReturnModel> where TReturnModel : class
    {
        TReturnModel GetById(string id);
    }
}