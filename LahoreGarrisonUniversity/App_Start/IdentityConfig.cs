using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;

namespace LahoreGarrisonUniversity.Models
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
        }
    }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            SeedData(context);
            base.Seed(context);
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string name = "admin@example.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }

        public static void SeedData(ApplicationDbContext context)
        {
            //Semester
            context.Semesters.Add(new Semester() { Semestername = "Fall 2013" });
            context.Semesters.Add(new Semester() { Semestername = "summer 2013" });
            context.Semesters.Add(new Semester() { Semestername = "Spring 2013" });
            context.Semesters.Add(new Semester() { Semestername = "Fall 2014" });
            context.Semesters.Add(new Semester() { Semestername = "Summer 2014" });
            context.Semesters.Add(new Semester() { Semestername = "spring 2014" });
            context.Semesters.Add(new Semester() { Semestername = "Fall 2015" });
            context.Semesters.Add(new Semester() { Semestername = "Summer 2015" });



            //Designation
            context.Designations.Add(new Designation() { DesignationName = "Professor" });
            context.Designations.Add(new Designation() { DesignationName = "Associate Professor" });
            context.Designations.Add(new Designation() { DesignationName = "Assistant Professor" });
            context.Designations.Add(new Designation() { DesignationName = "Lecturer" });
            context.Designations.Add(new Designation() { DesignationName = "Teacher Assistant" });

            //Department
            context.Departments.Add(new Department() { Code = "CSE", Name = "Computer Science" });

            //room
            context.Rooms.Add(new Room() { Name = "A 501" });
            context.Rooms.Add(new Room() { Name = "A 502" });
            context.Rooms.Add(new Room() { Name = "A 505" });
            context.Rooms.Add(new Room() { Name = "B 601" });
            context.Rooms.Add(new Room() { Name = "B 605" });
            context.Rooms.Add(new Room() { Name = "B 609" });


            //Day

            context.Days.Add(new Day() { Name = "SaturDay" });
            context.Days.Add(new Day() { Name = "SunDay" });
            context.Days.Add(new Day() { Name = "MonDay" });
            context.Days.Add(new Day() { Name = "TuesDay" });
            context.Days.Add(new Day() { Name = "WednesDay" });
            context.Days.Add(new Day() { Name = "ThursDay" });
            context.Days.Add(new Day() { Name = "Friday" });

            //grade
            context.Grades.Add(new Grade() { Name = "A+" });
            context.Grades.Add(new Grade() { Name = "A" });
            context.Grades.Add(new Grade() { Name = "B+" });
            context.Grades.Add(new Grade() { Name = "B" });
            context.Grades.Add(new Grade() { Name = "C+" });
            context.Grades.Add(new Grade() { Name = "C" });
            context.Grades.Add(new Grade() { Name = "D" });
            context.Grades.Add(new Grade() { Name = "F" });

            initializeFrontEndCourse(context);
            initializeTestimonial(context);
            initializeEvents(context);
            initializeNews(context);

            context.SaveChanges();
        }

        private static void initializeFrontEndCourse(ApplicationDbContext context)
        {
            context.FrontEndCourses.Add(
                new FrontEndCourse
                {
                    Title = "Discrete Structures",
                    Duration = "6 Months",
                    Flag = "New",
                    Level = "Undergraduate",
                    PrerequsiteCourse = "None",
                    StudentLevel = "Beginner",
                    Semester = "Fall",
                    CreditHourTheory = 3,
                    CreditHourPractical = 0,
                    CourseStructure =
                        "Mathematical reasoning:  introduction to logic, propositional and predicate calculus; negation disjunction and conjunction; implication and equivalence; truth tables; predicates; quantifiers; natural deduction; rules of Inference; methods of proofs; use in program proving; resolution principle!" +
                        "Set theory: Paradoxes in set theory; inductive definition of sets and proof by induction; Relations, representation of relations by graphs; properties of relations, equivalence relations and partitions; Partial orderings; Linear and wellordered sets!" +
                        "Functions: mappings, injection and surjection, composition of functions; inverse functions; special functions; Peano postulates; Recursive function theory; Elementary combinatorics; counting techniques; recurrence relation; generating functions!" +
                        "Graph Theory: elements of graph theory, Planar Graphs, Graph Colouring, Euler graph, Hamiltonian path, trees and their applications!",
                    Description =
                        @"Discrete mathematics is the study of mathematical structures that are fundamentally discrete rather than continuous. In contrast to real numbers that have the property of varying ""smoothly"", the objects studied in discrete mathematics – such as integers, graphs, and statements in logic – do not vary smoothly in this way, but have distinct, separated values. Discrete mathematics therefore excludes topics in ""continuous mathematics"" such as calculus and analysis. Discrete objects can often be enumerated by integers. More formally, discrete mathematics has been characterized as the branch of mathematics dealing with countable sets[3] (sets that have the same cardinality as subsets of the natural numbers, including rational numbers but not real numbers). However, there is no exact definition of the term ""discrete mathematics."" Indeed, discrete mathematics is described less by what is included than by what is excluded: continuously varying quantities and related notions!" +
                        "The set of objects studied in discrete mathematics can be finite or infinite. The term finite mathematics is sometimes applied to parts of the field of discrete mathematics that deals with finite sets, particularly those areas relevant to business!",
                    RecommendedBooks =
                        "Discrete Mathematical Structure with Application to Computer Science: J. P. Temblay and B Manohar, McGraw-Hill, 2nd Edition!" +
                        "Discrete Mathematics: 7th edition, Richard Johnson Baugh, 2008, Prentice Hall Publishers!" +
                        "Discrete Mathematics and Its Applications, 6th edition, Kenneth H. Rosen, 2006, McGraw-Hill Book Co!" +
                        "Discrete Mathematical Structures, 4th edition, Kolman, Busby & Ross, 2000, Prentice-Hall Publishers!" +
                        "Discrete and Combinatorial Mathematics: An Applied Introduction, Ralph P. Grimaldi,  Addison-Wesley Pub. Co., 1985!" +
                        "Logic and Discrete Mathematics: A Computer Science Perspective by Winifred Grassman, Jean-Paul Tremblay, Winifred Grassman, Prentice Hall, 1995!",
                    ImageUrl = "DiscreteStructures.png"
                });
            context.FrontEndCourses.Add(
                new FrontEndCourse
                {
                    Title = "Programming Fundamentals",
                    Duration = "6 Months",
                    Flag = "",
                    Level = "Undergraduate",
                    PrerequsiteCourse = "None",
                    StudentLevel = "Beginner",
                    Semester = "Fall",
                    CreditHourTheory = 3,
                    CreditHourPractical = 1,
                    CourseStructure =
                        "This course covers overview of Computer Programming, Principles of Structured and Modular Programming, Overview of Structured Programming Languages, Algorithms and Problem Solving!" +
                        "Program Development: Analyzing Problem, Designing Algorithm/Solution, Testing Designed Solution, Translating Algorithms into Programs, Fundamental Programming Constructs!" +
                        "Data Types; Basics of Input and Output, Selection and Decision (If, If-Else, Nested If-Else, Switch Statement and Condition Operator), Repetition (While and For Loop, Do-While Loops), Break Statement, Continue Statement, Control Structures, Functions, Arrays, Pointers, Records, Files (Input-Output), Testing & Debugging.",
                    Description =
                        @"Fluency in a programming language is prerequisite to the study of most of computer science. Undergraduate computer science programs must teach students how to use at least one programming language well; furthermore, computer science programs should teach students to become competent in languages that make use of the object-oriented and event-driven programming paradigms.
                                    This knowledge area includes those skills and concepts that are essential to programming practice independent of the underlying paradigm. As a result, this area includes units on fundamental programming concepts, basic data structures, algorithmic processes, and basic security. These units, however, by no means cover the full range of programming knowledge that a computer science undergraduate must know. Many of the other areas—most notably Programming Languages (PL) and Software Engineering (SE)—also contain programming-related units that are part of the undergraduate core. In most cases, these units could equally well have been assigned to either Programming Fundamentals or the more advanced area.",
                    RecommendedBooks =
                        "C How to Program, Paul Deitel and Harvey Deitel, Prentice Hall; 7th edition (March 4, 2012)!" +
                        "Programming in C, Stephen G. Kochan, Addison-Wesley Professional; 4th edition (September 25, 2013). ISBN-10: 0321776410!" +
                        "Java How to Program, Paul Deitel and Harvey Deitel, Prentice Hall; 9th edition (March, 2011)!" +
                        "C++ How to Programme, Paul Deitel and Harvey Deitel, Prentice Hall; 9th edition (February, 2013)!",
                    ImageUrl = "ProgrammingFundamentals.jpg"
                });
            context.FrontEndCourses.Add(
            new FrontEndCourse
            {
                Title = "Object Oriented Programming",
                Duration = "6 Months",
                Flag = "",
                Level = "Undergraduate",
                PrerequsiteCourse = "Programming Fundamentals",
                StudentLevel = "Beginner",
                Semester = "Spring",
                CreditHourTheory = 3,
                CreditHourPractical = 1,
                CourseStructure = "Evolution of Object Oriented Programming (OOP)!" +
                                  "Object Oriented concepts and principles!" +
                                  "problem solving in Object Oriented paradigm!" +
                                  "OOP design process, classes, functions/methods!" +
                                  "objects and encapsulation; constructors and destructors!" +
                                  "operator and function/method overloading, association!" +
                                  "aggregation, composition, generalization, inheritance and its types!" +
                                  "derived classes, function/method overriding, abstract and concrete classes!" +
                                  "virtual functions, polymorphism, exception handling!",
                Description = @"Object-oriented programming (OOP) is a programming paradigm based on the concept of ""objects"", which are data structures that contain data, in the form of fields, often known as attributes; and code, in the form of procedures, often known as methods. A distinguishing feature of objects is that an object's procedures can access and often modify the data fields of the object with which they are associated (objects have a notion of ""this""). In object-oriented programming, computer programs are designed by making them out of objects that interact with one another.[1][2] There is significant diversity in object-oriented programming, but most popular languages are class-based, meaning that objects are instances of classes, which typically also determines their type.",
                RecommendedBooks = "An Introduction to Object-Oriented Programming with Java, C. Thomas Wu (2010). 5th Edition. McGraw-Hill. ISBN: 9780073523309!" +
                                   "Java: How to Programme, 5/e, Deitel and Deitel, Prentice Hall, 0131016210/ 0131202367 International Edition!" +
                                   "Ivor Horton’s Beginning Java, 7/e, Ivor Horton!" +
                                   "C++: How to Programme, Deitel and Deitel, 5/e, Pearson!" +
                                   "Object Oriented Programming in C++, 3rdEdition, Robert Lafore!",
                ImageUrl = "ObjectOrientedProgramming",
            });


        }
        private static void initializeTestimonial(ApplicationDbContext context)
        {
            context.Testimonial.Add(
                new Testimonial
                {
                    Name = "Awais",
                    Review = "Good University",
                    MediaUrl = "/Content/FrontEnd/images/testimonials/profile-3.png",
                    CreatedDate = DateTime.Now,
                    IsApproved = 1
                });

            context.Testimonial.Add(
                new Testimonial
                {
                    Name = "Sidra",
                    Review = "Always highlight the strengths or the good experience you had using a service or product.",
                    MediaUrl = "/Content/FrontEnd/images/testimonials/profile-2.png",
                    CreatedDate = DateTime.Now,
                    IsApproved = 1
                });

            context.Testimonial.Add(
                new Testimonial
                {
                    Name = "Azeem",
                    Review = "While giving a testimonial make sure you give many indications that you are authentic. This can be done by providing some way for people to contact you to verify if you are really the one who provided the testimonial.",
                    MediaUrl = "/Content/FrontEnd/images/testimonials/profile-1.png",
                    CreatedDate = DateTime.Now,
                    IsApproved = 1
                });

            context.Testimonial.Add(
                new Testimonial
                {
                    Name = "Azeem",
                    Review = "It should be short and sweet. Even if you like everything about a service or a product, just write about the most important aspects of the service or a product. Come straight to the point when you are writing testimonials.",
                    MediaUrl = "/Content/FrontEnd/images/testimonials/profile-3.png",
                    CreatedDate = DateTime.Now,
                    IsApproved = 1
                });
        }
        private static void initializeEvents(ApplicationDbContext context)
        {
            context.Event.Add(
                new Event
                {
                    Title = "Open Day",
                    Location = "Lahore",
                    StartDate = DateTime.Now,
                    MediaUrl = "",
                    CreatedAt = DateTime.Now,
                    Description = "Orange Fire Production Is Back Again with Festival of colors Paint War 2 Be ready to go Crazy this Summer with the Colors Beat the Heat with Colorful Water Splashes Cover each other with the Colors of the Season Its Color Colors Colors Everywhere! We are Offering you Great entertainment & much more again & again!",
                    Duration = 2,
                    ForWhome = "students",
                    UserName = "Admin"
                }
                );

            context.Event.Add(
                new Event
                {
                    Title = "Open Day",
                    Location = "Lahore",
                    StartDate = DateTime.Now,
                    MediaUrl = "",
                    CreatedAt = DateTime.Now,
                    Description = "Orange Fire Production Is Back Again with Festival of colors Paint War 2 Be ready to go Crazy this Summer with the Colors Beat the Heat with Colorful Water Splashes Cover each other with the Colors of the Season Its Color Colors Colors Everywhere! We are Offering you Great entertainment & much more again & again!",
                    Duration = 2,
                    ForWhome = "students",
                    UserName = "Admin"
                }
                );

            context.Event.Add(
                new Event
                {
                    Title = "Open Day",
                    Location = "Lahore",
                    StartDate = DateTime.Now,
                    MediaUrl = "",
                    CreatedAt = DateTime.Now,
                    Description = "Orange Fire Production Is Back Again with Festival of colors Paint War 2 Be ready to go Crazy this Summer with the Colors Beat the Heat with Colorful Water Splashes Cover each other with the Colors of the Season Its Color Colors Colors Everywhere! We are Offering you Great entertainment & much more again & again!",
                    Duration = 2,
                    ForWhome = "students",
                    UserName = "Admin"
                }
                );

            context.Event.Add(
                new Event
                {
                    Title = "Open Day",
                    Location = "Lahore",
                    StartDate = DateTime.Now,
                    MediaUrl = "",
                    CreatedAt = DateTime.Now,
                    Description = "Orange Fire Production Is Back Again with Festival of colors Paint War 2 Be ready to go Crazy this Summer with the Colors Beat the Heat with Colorful Water Splashes Cover each other with the Colors of the Season Its Color Colors Colors Everywhere! We are Offering you Great entertainment & much more again & again!",
                    Duration = 2,
                    ForWhome = "students",
                    UserName = "Admin"
                }
                );

            context.Event.Add(
                new Event
                {
                    Title = "Open Day",
                    Location = "Lahore",
                    StartDate = DateTime.Now,
                    MediaUrl = "",
                    CreatedAt = DateTime.Now,
                    Description = "Orange Fire Production Is Back Again with Festival of colors Paint War 2 Be ready to go Crazy this Summer with the Colors Beat the Heat with Colorful Water Splashes Cover each other with the Colors of the Season Its Color Colors Colors Everywhere! We are Offering you Great entertainment & much more again & again!",
                    Duration = 2,
                    ForWhome = "students",
                    UserName = "Admin"
                }
                );

            context.Event.Add(
                new Event
                {
                    Title = "Open Day",
                    Location = "Lahore",
                    StartDate = DateTime.Now,
                    MediaUrl = "",
                    CreatedAt = DateTime.Now,
                    Description = "Orange Fire Production Is Back Again with Festival of colors Paint War 2 Be ready to go Crazy this Summer with the Colors Beat the Heat with Colorful Water Splashes Cover each other with the Colors of the Season Its Color Colors Colors Everywhere! We are Offering you Great entertainment & much more again & again!",
                    Duration = 2,
                    ForWhome = "students",
                    UserName = "Admin"
                }
                );

            context.Event.Add(
                new Event
                {
                    Title = "Open Day",
                    Location = "Lahore",
                    StartDate = DateTime.Now,
                    MediaUrl = "",
                    CreatedAt = DateTime.Now,
                    Description = "Orange Fire Production Is Back Again with Festival of colors Paint War 2 Be ready to go Crazy this Summer with the Colors Beat the Heat with Colorful Water Splashes Cover each other with the Colors of the Season Its Color Colors Colors Everywhere! We are Offering you Great entertainment & much more again & again!",
                    Duration = 2,
                    ForWhome = "students",
                    UserName = "Admin"
                }
                );
        }

        private static void initializeNews(ApplicationDbContext context)
        {
            context.News.Add(
                new News
                {
                    Title = "Addmissions",
                    Description = "Addmissions Open now",
                    CreatedAt = DateTime.Now,
                    MediaUrl = "/Content/FrontEnd/images/news/news-thumb-1.jpg",
                    UserName = "admin"
                }
                );
            context.News.Add(
                new News
                {
                    Title = "Addmissions",
                    Description = "Addmissions Open now",
                    CreatedAt = DateTime.Now,
                    MediaUrl = "/Content/FrontEnd/images/news/news-thumb-2.jpg",
                    UserName = "admin"
                }
                );
            context.News.Add(
                new News
                {
                    Title = "Addmissions",
                    Description = "Addmissions Open now",
                    CreatedAt = DateTime.Now,
                    MediaUrl = "/Content/FrontEnd/images/news/news-thumb-3.jpg",
                    UserName = "admin"
                }
                            );
            context.News.Add(
                            new News
                            {
                                Title = "Addmissions",
                                Description = "Addmissions Open now",
                                CreatedAt = DateTime.Now,
                                MediaUrl = "/Content/FrontEnd/images/news/news-thumb-4.jpg",
                                UserName = "admin"
                            }
                            );
            context.News.Add(
                            new News
                            {
                                Title = "Addmissions",
                                Description = "Addmissions Open now",
                                CreatedAt = DateTime.Now,
                                MediaUrl = "/Content/FrontEnd/images/news/news-thumb-5.jpg",
                                UserName = "admin"
                            }
                            );
            context.News.Add(
                            new News
                            {
                                Title = "Addmissions",
                                Description = "Addmissions Open now",
                                CreatedAt = DateTime.Now,
                                MediaUrl = "/Content/FrontEnd/images/news/news-thumb-6.jpg",
                                UserName = "admin"
                            }
                            );
            context.News.Add(
                            new News
                            {
                                Title = "Addmissions",
                                Description = "Addmissions Open now",
                                CreatedAt = DateTime.Now,
                                MediaUrl = "/Content/FrontEnd/images/news/news-thumb-4.jpg",
                                UserName = "admin"
                            }
                            );

        }

    }

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}