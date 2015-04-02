using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LahoreGarrisonUniversity.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        #region New Table

        public virtual DbSet<Basic> Basic { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Testimonial> Testimonial { get; set; }

        //New Database

        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<AssignCourse> AssignCourses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomAllocation> RoomAllocations { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<EnrollCourse> EnrollCourses { get; set; }
        public DbSet<ResultEntry> ResultEntries { get; set; }
        public DbSet<Grade> Grades { set; get; }
        public DbSet<FrontEndCourse> FrontEndCourses { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            #region Rename identity tables

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("User");

            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("UserRole");

            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("UserLogin");

            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("UserClaim");

            modelBuilder.Entity<IdentityRole>()
                .ToTable("Role");

            #endregion
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}