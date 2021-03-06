﻿using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripMaker.ExternalServices.Entities.GooglePlaceNearbySearch;
using TripMaker.ExternalServices.Interfaces.GooglePlace;
using TripMaker.Plan.Interfaces;
using TripMaker.Plan.Models;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using TripMaker.PlacePhotos;

namespace TripMaker.Plan
{
    public class PlanManager : IPlanManager
    {
        private readonly IRepository<Plan> _planRepository;
        private readonly IRepository<PlanForm> _planFormRepository;
        private readonly IPlacePhotoManager _placePhotoManager;
        //private readonly IRepository<PlanElement> _planElementRepository;
        //private readonly IRepository<PlanRoute> _planRouteRepository;
        //private readonly IRepository<PlanRouteStep> _planRouteStepRepository;
        private readonly IPlanProvider _planProvider;
        public IEventBus EventBus { get; set; }


        public PlanManager
            (
            IRepository<Plan> planRepository,
            IRepository<PlanForm> planFormRepository,
            IPlacePhotoManager placePhotoManager,
            //IRepository<PlanElement> planElementRepository,
            //IRepository<PlanRoute> planRouteRepository,
            //IRepository<PlanRouteStep> planRouteStepRepository,
            IPlanProvider planProvider
            )
        {
            _planRepository=planRepository;
            _planFormRepository = planFormRepository;
            //_planElementRepository = planElementRepository;
            //_planElementsProvider = planElementsProvider;
            //_planRouteRepository = planRouteRepository;
            //_planRouteStepRepository = planRouteStepRepository;
            //_planFormPolicy = planFormPolicy;
            EventBus = NullEventBus.Instance;
            _planProvider = planProvider;
            _placePhotoManager = placePhotoManager;
        }

        public async Task<Plan> CreateAsync(PlanForm planForm)
        {

            await EventBus.TriggerAsync(new EventSearchPlace(planForm)); //update SearchedPlaces DB

            await _planFormRepository.InsertAsync(planForm);

            var plan =await _planProvider.GenerateAsync(planForm); 

            await _planRepository.InsertAsync(plan);

            plan.Photo = await _placePhotoManager.GetPhotos(plan.PlanForm.PlaceId);

            //foreach(var element in plan.Elements)
            //{
            //    await _planElementRepository.InsertAsync(element);

            //    if(element.EndingRoute != null)
            //    {
            //        await _planRouteRepository.InsertAsync(element.EndingRoute);

            //        foreach(var step in element.EndingRoute.Steps)
            //        {
            //            await _planRouteStepRepository.InsertAsync(step);
            //        }
            //    }
            //}

            return plan;
        }

        public async Task<Plan> GetAsync(int planId)
        {
            var plan = await _planRepository
                                .GetAll()
                                .Include(e=>e.PlanFormWeightVector)
                                .Include(e => e.PlanAccomodation)
                                .Include(e => e.PlanForm)
                                .Include(e => e.Elements)
                                .ThenInclude(r => r.EndingRoute)
                                .ThenInclude(r => r.Steps)
                                .Where(e => e.Id == planId)
                                .FirstOrDefaultAsync();

            plan.Elements.OrderBy(e => e.OrderNo);

            plan.Photo = await _placePhotoManager.GetPhotos(plan.PlanForm.PlaceId);

            if (plan == null)
            {
                throw new UserFriendlyException($"Nie udało znaleźć się planu z id: {planId}");
            }

            return plan;
        }
    }
}
