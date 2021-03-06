﻿using Abp.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TripMaker.Plan.Interfaces;

namespace TripMaker.Plan
{
    public class PlanFormPolicy : IPlanFormPolicy
    {
       // private readonly IPlanDataProvider _planDataProvider;

        public PlanFormPolicy()
        {
         //   _planDataProvider = planDataProvider;
        }

        // Co sprawdzić
        // -> różnica dat -> 2 tygodnie?
        // -> Timespan must be?


        public async Task CheckFormValidAsync(PlanForm planForm)
        {
            if (!planForm.IsDatesCorrect())
            {
                throw new UserFriendlyException("Trip start or end data are incorrect!");
            }
            //if (!(await _planDataProvider.IsPlaceIdValid(planForm.PlaceId)))
            //{
            //    throw new UserFriendlyException("PlaceId is incorrect!");
            //}
        }
    }
}
