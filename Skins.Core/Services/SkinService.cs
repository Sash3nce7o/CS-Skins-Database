using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Skins.Core.Contracts.Services;
using Skins.Core.Exceptions;
using Skins.Core.Models.Skin;
using Skins.Infrastructure.Common;
using Skins.Infrastructure.Data.Models;

namespace Skins.Core.Services
{
    public class SkinService : ISkinService
    {
        private readonly IRepository _repo;
        public SkinService(IRepository repo)
        {
            _repo = repo;
        }
        public void Add(SkinCreateViewModel entity)
        {
            var users = _repo.AllAsNoTracking<User>()
                .Where(u => u.Id == entity.OwnerId)
                .ToList();

            if (users.Count == 0)
            {
                throw new InvalidOperationException("Owner not found.");
            }

            var skin = new Skin()
            {
                Name = entity.Name,
                Float = entity.Float,
                Pattern = entity.Pattern,
                MaxFloat = entity.MaxFloat,
                OwnerId = entity.OwnerId,
                Quality = CalculateQuality(entity.Float)
            };

            _repo.Add(skin);
            _repo.SaveChanges();
        }

        
        public Skin GetById(string id)
        {
            if(string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id)){
                throw new NullReferenceException("Id cannot be null or whitespace.");
            }
            Skin skin = _repo.GetById<Skin>(id);
            if (skin == null){
                throw new NullReferenceException("Skin not found.");
            }
            return skin;
        }

        public bool Remove(string id)
        {
            try
            {
                _repo.Delete<Skin>(id);
                _repo.SaveChanges();
                return true;
            }
            catch (NullReferenceException)
            {
                
                return false;
            }
            

        }

    public void Update(string id, SkinUpdateViewModel model)
    {
        if(string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            throw new NotFoundException("ID cannot be null or empty");
        }
    
        Skin skin = _repo.GetById<Skin>(id);
        if (skin == null)
        {
            throw new NotFoundException($"Skin with ID '{id}' not found");
        }
    
        if (model.Float.HasValue)
            skin.Float = model.Float.Value;
    
        if (!string.IsNullOrEmpty(model.Pattern))
            skin.Pattern = model.Pattern;

        if (model.MaxFloat.HasValue)
            skin.MaxFloat = model.MaxFloat;
    
        _repo.Update(skin);
        _repo.SaveChanges();
    }

        

        private SkinQuality CalculateQuality(float @float)
        {
            if (@float <= 0.07f)
            {
                return SkinQuality.FactoryNew;
            }
            else if (@float <= 0.15f)
            {
                return SkinQuality.MinimalWear;                
            }
            else if (@float <= 0.37f)
            {
                return SkinQuality.FieldTested;
            }
            else if (@float <= 0.45f)
            {
                return SkinQuality.WellWorn;
            }
            else
            {
                return SkinQuality.BattleScarred;
            }
        }

        
    }
}