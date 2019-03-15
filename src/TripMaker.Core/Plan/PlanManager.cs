﻿using Abp.Domain.Repositories;
using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TripMaker.ExternalServices.Entities.GooglePlaceNearbySearch;
using TripMaker.ExternalServices.Interfaces.GooglePlace;
using TripMaker.Plan.Interfaces;

namespace TripMaker.Plan
{
    public class PlanManager : IPlanManager
    {
        private readonly IRepository<Plan> _planRepository;
        private readonly IRepository<PlanForm> _planFormRepository;
        private readonly IRepository<PlanElement> _planElementRepository;
        private readonly IPlanFormPolicy _planFormPolicy;
        private readonly IPlanElementsProvider _planElementsProvider;
        public IEventBus EventBus { get; set; }


        public PlanManager
            (
            IRepository<Plan> planRepository,
            IRepository<PlanForm> planFormRepository,
            IRepository<PlanElement> planElementRepository,
            IPlanElementsProvider planElementsProvider,
            IPlanFormPolicy planFormPolicy
            )
        {
            _planRepository=planRepository;
            _planFormRepository = planFormRepository;
            _planElementRepository = planElementRepository;
            _planElementsProvider = planElementsProvider;
            _planFormPolicy = planFormPolicy;
            EventBus = NullEventBus.Instance;
        }

        public async Task<Plan> CreateAsync(PlanForm planForm)
        {
            await _planFormPolicy.CheckFormValidAsync(planForm); //check if planForm object has valid data

            await EventBus.TriggerAsync(new EventSearchPlace(planForm)); //update SearchedPlaces DB

            await _planFormRepository.InsertAsync(planForm);
   

            var plan = await _planElementsProvider.GenerateAsync(planForm);


            //insert plan to DB
            await _planRepository.InsertAsync(plan);

            foreach(var element in plan.Elements)
            {
                await _planElementRepository.InsertAsync(element);
            }

            return plan;
        }
    }
}
