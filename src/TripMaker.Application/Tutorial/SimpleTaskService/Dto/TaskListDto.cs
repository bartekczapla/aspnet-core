﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripMaker.Tutorial.Dto
{
    [AutoMapFrom(typeof(SimpleTask))]
    public class TaskListDto : EntityDto, IHasCreationTime
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public TaskState State { get; set; }

        public int? AssignedPersonId { get; set; }
        public string AssignedPersonName { get; set; }

    }
}
