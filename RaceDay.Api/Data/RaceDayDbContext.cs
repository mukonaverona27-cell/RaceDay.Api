using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Models;
using System.Collections.Generic;

namespace RaceDay.Api.Data
{
    public class RaceDayDbContext : IdentityDbContext<AppUser>
    {
        public RaceDayDbContext(DbContextOptions<RaceDayDbContext> options)
            : base(options) { }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Race> Races => Set<Race>();
        public DbSet<Participant> Participants => Set<Participant>();
        public DbSet<RaceEntry> RaceEntries => Set<RaceEntry>();
        public DbSet<Result> Results => Set<Result>();
    }
}