﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.UI;
using TripMaker.Plan;
using TripMaker.UserPlans.Dto;
using TripMaker.UserPlans.Interfaces;

namespace TripMaker.UserPlans
{
    public class UserPlansAppService : TripMakerAppServiceBase, IUserPlansAppService
    {
        private readonly IUserPlansManager _userPlansManager;

        public UserPlansAppService(IUserPlansManager userPlansManager)
        {
            _userPlansManager = userPlansManager;
        }

        public async Task<bool> DeleteAsync(EntityDto<int> input)
        {
            return await _userPlansManager.DeleteAsync(input.Id);
        }

        public async Task<ListResultDto<UserPlansListDto>> GetAllUserPlansAsync(GetAllUserPlansInput input)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                throw new UserFriendlyException("Could not found logged user.");
            }

            user.Id = 3;
            var plans = await _userPlansManager.GetAllUserPlansAsync(user);

            return new ListResultDto<UserPlansListDto>(UserPlansCommon.MapResult(plans));
        }

        public async Task<PlanDto> GetDetailAsync(EntityDto<int> input)
        {
            var result = await _userPlansManager.GetDetailsAsync(input.Id);

            return result.MapTo<PlanDto>();
        }
    }
}
