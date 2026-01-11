using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skins.Core.Contracts.Common;
using Skins.Core.Models.User;
using Skins.Infrastructure.Data.Models;

namespace Skins.Core.Contracts.Services
{
    public interface IUserService: IAddable<UserRegisterViewModel>, 
    IReadable<User>,
    IRemovable
    {
        void Update(string id, UserUpdateViewModel model);
    }
}