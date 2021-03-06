﻿using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TripMaker.Authorization.Users;

namespace TripMaker.UserPlans.Interfaces
{
    public interface IUserPlansManager : IDomainService
    {
        Task<List<Plan.Plan>> GetAllUserPlansAsync(long userId);

        Task<Plan.Plan> GetDetailsAsync(int planId);

        Task<bool> DeleteAsync(int planId);
    }
}
