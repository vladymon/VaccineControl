using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VC.Common.Entities;
using VC.Web.Data.Entities;
using VC.Web.Models;

namespace VC.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<VaccineRequest> VaccineRequests { get; set; }
        public DbSet<VaccineRequestDetail> vaccineRequestDetails { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbQuery<AttentionPerDayCalendar> AttentionPerDayCalendars { get; set; }
        public DbQuery<FindUserDetailViewModel> FindUserDetailViewModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().HasIndex(t => t.Name).IsUnique();
            modelBuilder.Entity<Department>(dep => { 
                dep.HasIndex("Name", "CountryId").IsUnique(); 
            });
            modelBuilder.Entity<Province>(pro => {
                pro.HasIndex("Name", "DepartmentId").IsUnique();
            });
            modelBuilder.Entity<District>(dis => {
                dis.HasIndex("Name", "ProvinceId").IsUnique();
            });
            modelBuilder.Entity<User>().HasIndex(t => t.Document).IsUnique();
        }

        public DbSet<VC.Common.Entities.Clinic> Clinic { get; set; }

        
    }

}
