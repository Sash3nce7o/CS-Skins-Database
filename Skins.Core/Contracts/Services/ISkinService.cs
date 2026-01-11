using Skins.Core.Contracts.Common;
using Skins.Core.Models.Skin;
using Skins.Infrastructure.Data.Models;

namespace Skins.Core.Contracts.Services;

public interface ISkinService : 
    IAddable<SkinCreateViewModel>, 
    IReadable<Skin>,
    IRemovable
{
    void Update(string id, SkinUpdateViewModel model);
}