﻿using Abp.Domain.Services;
using TripMaker.ExternalServices.Entities.GoogleDirections;
using TripMaker.Plan;

namespace TripMaker.ExternalServices.Interfaces
{
    public interface IGoogleDirectionsInputFactory : IDomainService
    {
        GoogleDirectionsInput Create(PlanForm planForm);
    }
}
