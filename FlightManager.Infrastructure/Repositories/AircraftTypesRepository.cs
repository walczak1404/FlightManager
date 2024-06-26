﻿using FlightManager.Core.Domain.Entities;
using FlightManager.Core.Domain.RepositoryInterfaces;
using FlightManager.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Infrastructure.Repositories
{
    public class AircraftTypesRepository : IAircraftTypesRepository
    {
        private readonly AppDbContext _db;

        public AircraftTypesRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<AircraftType>> GetAllAircraftTypesAsync()
        {
            List<AircraftType> aircraftTypes = await _db.AircraftTypes.ToListAsync();
            return aircraftTypes;
        }

        public async Task<AircraftType?> GetAircraftTypeByIDAsync(Guid aircraftTypeID)
        {
            AircraftType? aircraftType = await _db.AircraftTypes.FindAsync(aircraftTypeID);

            return aircraftType;
        }
    }
}
