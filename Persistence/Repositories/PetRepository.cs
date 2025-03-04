﻿using WebApi.Entities;
using WebApi.Helpers;
using WebApi.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Services;
using WebApi.Enums;

namespace WebApi.Persistence.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly DataContext _context;
        private readonly IPropertyMappingService _propertyMappingService;

        public PetRepository(DataContext context,
            IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _propertyMappingService = propertyMappingService ??
                throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public PagedList<Pet> Get(PetsResourceParameters resourceParameters, Account account)
        {
            if (resourceParameters == null)
            {
                throw new ArgumentNullException(nameof(resourceParameters));
            }

            var collection = _context.Pets
                                .Include(c => c.Race)
                                .Include(p => p.Images)
                                .Include(c => c.CreatedBy) as IQueryable<Pet>;

            var isOnlyPublished = true;
            if (account != null && (account.Role == Role.Admin || account.Id == resourceParameters.CreatedById))
            {
                collection = collection.Where(a => a.PetStatus != PetStatus.Deleted);
                isOnlyPublished = false;
            }
            
            if (isOnlyPublished)
            {
                collection = collection.Where(a => a.PetStatus == PetStatus.Published);
            }

            if (resourceParameters.PetType != PetType.All)
            {
                collection = collection.Where(a => a.PetType == resourceParameters.PetType);
            }

            if (resourceParameters.CreatedById > 0)
            {
                collection = collection.Where(a => a.CreatedById == resourceParameters.CreatedById);
            }

            if (resourceParameters.CityId > 0)
            {
                collection = collection.Where(a => a.CityId == resourceParameters.CityId);
            }

            if (resourceParameters.RaceId > 0)
            {
                collection = collection.Where(a => a.RaceId == resourceParameters.RaceId);
            }

            if (resourceParameters.Age != PetAge.None)
            {
                collection = collection.Where(a => a.Age == resourceParameters.Age);
            }

            if (resourceParameters.Gender != Gender.None)
            {
                collection = collection.Where(a => a.Gender == resourceParameters.Gender);
            }

            if (resourceParameters.Size != Size.None)
            {
                collection = collection.Where(a => a.Size == resourceParameters.Size);
            }

            if (resourceParameters.FromWhere != FromWhere.None)
            {
                collection = collection.Where(a => a.FromWhere == resourceParameters.FromWhere);
            }

            if (!string.IsNullOrWhiteSpace(resourceParameters.SearchQuery))
            {
                var searchQuery = resourceParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.Title.Contains(searchQuery));
            }

            if (!string.IsNullOrWhiteSpace(resourceParameters.OrderBy))
            {
                // get property mapping dictionary
                var propertyMappingDictionary =
                    _propertyMappingService.GetPropertyMapping<Models.PetDto, Pet>();

                collection = collection.ApplySort(resourceParameters.OrderBy,
                    propertyMappingDictionary);
            }

            return PagedList<Pet>.Create(collection,
                resourceParameters.PageNumber,
                resourceParameters.PageSize);
        }

        public Pet Get(Guid petId)
        {
            if (petId == Guid.Empty)
            {
                throw new AppException(nameof(petId));
            }

            var pet = _context.Pets
                            .Include(p => p.Race)
                            .Include(p => p.CreatedBy)
                            .Include(p => p.Images)
                            .SingleOrDefault(a => a.Id == petId);

            if (pet == null)
            {
                throw new KeyNotFoundException("Pet not found");
            }

            return pet;
        }

        public void Add(Pet pet)
        {
            if (pet == null)
            {
                throw new AppException(nameof(pet));
            }

            // the repository fills the id (instead of using identity columns)
            pet.Id = Guid.NewGuid();

            _context.Pets.Add(pet);
        }

        public void Update(Pet pet)
        {
            if (pet == null)
            {
                throw new AppException(nameof(pet));
            }
            
            _context.Pets.Update(pet);
        }
    }
}
