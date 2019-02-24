﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TripMaker.Plan
{
    public static class PlanCommon
    {
        public static GetPlanInput CreateTestInput()
        {
            return new GetPlanInput
            {
                PlaceName= "Madrid",
                PlaceId= "ChIJgTwKgJcpQg0RaSKMYcHeNsQ",
                StartDate=new DateTime(2018, 8, 1),
                StartTime=new TimeSpan(8, 0, 0),
                EndDate=new DateTime(2018, 8, 8),
                EndTime=new TimeSpan(18, 0, 0),
                HasJourneyBooked=true,
                HasAccomodationBooked=false
            };
        }
    }
}