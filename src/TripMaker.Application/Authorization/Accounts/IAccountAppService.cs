﻿using System.Threading.Tasks;
using Abp.Application.Services;
using TripMaker.Authorization.Accounts.Dto;

namespace TripMaker.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
