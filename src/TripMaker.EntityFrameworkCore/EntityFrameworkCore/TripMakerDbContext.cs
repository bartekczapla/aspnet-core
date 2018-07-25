﻿using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using TripMaker.Authorization.Roles;
using TripMaker.Authorization.Users;
using TripMaker.MultiTenancy;
using TripMaker.Tutorial;

namespace TripMaker.EntityFrameworkCore
{
    public class TripMakerDbContext : AbpZeroDbContext<Tenant, Role, User, TripMakerDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<SimpleTask> Tasks { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventRegistration> EventRegistrations { get; set; }

        public TripMakerDbContext(DbContextOptions<TripMakerDbContext> options)
            : base(options)
        {
        }
    }
}
