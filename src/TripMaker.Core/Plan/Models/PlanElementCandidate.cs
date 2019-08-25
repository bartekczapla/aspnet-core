﻿using System;
using System.Collections.Generic;
using System.Text;
using TripMaker.Enums;
using TripMaker.ExternalServices.Entities.Common;

namespace TripMaker.Plan.Models
{
    public class PlanElementCandidate
    {
        public string PlaceName { get; set; }

        public string PlaceId { get; set; }

        public Location Location { get; set; }

        public IList<PlanElementType> ElementTypes { get; set; }

        public decimal? Rating { get; set; }

        public decimal? Price { get; set; }

        public decimal? Popularity { get; set; }


        public PlanElementCandidate(string placeName, string placeId, Location location, IList<PlanElementType> elementTypes,  decimal? rating=null, decimal? price=null, decimal? popularity=null)
        {
            PlaceName = placeName;
            PlaceId = placeId;
            Location = location;
            ElementTypes = elementTypes;
            Rating = rating;
            Price = price;
            Popularity = popularity;

        }

    }
}