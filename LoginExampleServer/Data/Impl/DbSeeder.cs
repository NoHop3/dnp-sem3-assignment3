using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using LoginExampleServer.DataAccess;
using LoginExampleServer.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginExampleServer.Data.Impl
{
    public class DbSeeder : IUserService, IAdultService, IJobsService
    {
        private AdultDBContext dbContext;
        public IList<Adult> AdultsList { get; private set; }
        public IList<User> UsersList { get; private set; }
        public IList<Job> JobsList { get; private set; }
        private readonly string adultsFile = "adults.json";
        private readonly string usersFile = "users.json";
        private readonly string jobsFile = "jobs.json";

        public DbSeeder()
        {
            dbContext = new AdultDBContext();
            Seed();
        }

        public void Seed()
        {
            AdultsList = File.Exists(adultsFile) ? ReadData<Adult>(adultsFile) : new List<Adult>();
            UsersList = File.Exists(usersFile) ? ReadData<User>(usersFile) : new List<User>();
            JobsList = File.Exists(jobsFile) ? ReadData<Job>(jobsFile) : new List<Job>();

            try
            {
                Console.WriteLine("Inserting adults...");
                AddAdults();
                Console.WriteLine("Done!");

                Console.WriteLine("Inserting users..");
                AddUsers();
                Console.WriteLine("Done!");

                Console.WriteLine("Inserting jobs...");
                AddJobs();
                Console.WriteLine("Done");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void AddAdults()
        {
            foreach (Adult adult in AdultsList)
            {
                try
                {
                    if (!dbContext.Adults.Contains(adult))
                    {
                        dbContext.Adults.Add(adult);
                        dbContext.Entry(adult).State = EntityState.Added;
                        dbContext.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.InnerException.Message);
                }
            }
        }

        private void AddUsers()
        {
            foreach (User user in UsersList)
            {
                try
                {
                    if (!dbContext.Users.Contains(user))
                    {
                        dbContext.Users.Add(user);
                        dbContext.Entry(user).State = EntityState.Added;
                        dbContext.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                }
            }
        }

        private void AddJobs()
        {
            foreach (Job job in JobsList)
            {
                if (!dbContext.Jobs.Contains(job))
                {
                    dbContext.Jobs.Add(job);
                    dbContext.Entry(job).State = EntityState.Added;
                    dbContext.SaveChanges();
                }
            }
        }

        private IList<T> ReadData<T>(string s)
        {
            using (var jsonReader = File.OpenText(s))
            {
                return JsonSerializer.Deserialize<List<T>>(jsonReader.ReadToEnd());
            }
        }

        public async Task<IList<Adult>> GetAdultsAsync()
        {
            List<Adult> temp = new List<Adult>(dbContext.Adults).OrderBy(adult => adult.Id).ToList();
            return temp;
        }

        public async Task<IList<User>> GetUsersAsync()
        {
            List<User> temp = new List<User>(dbContext.Users).OrderBy(user => user.Id).ToList();
            return temp;
        }

        public async Task<IList<Job>> GetJobsAsync()
        {
            List<Job> temp = new List<Job>(dbContext.Jobs).OrderBy(job => job.Id).ToList();
            return temp;
        }

        public async Task AddJobAsync(Job job)
        {
            int max;
            try
            {
                foreach (var job1 in dbContext.Jobs)
                {
                    if (job.Id == job1.Id)
                    {
                        max = dbContext.Jobs.Max(job => job.Id);
                        job.Id = (++max);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            dbContext.Jobs.Add(job);
            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveJobAsync(int jobId)
        {
            Job jobToRemove = dbContext.Jobs.Where(job1 => job1.Id == jobId).Include(j => j.Adults).First();
            dbContext.Jobs.Remove(jobToRemove);
            await dbContext.SaveChangesAsync();
        }

        public async Task<User> ValidateUserAsync(string userName, string password)
        {
            User first =
                dbContext.Users.FirstOrDefault(user => user.UserName.Equals(userName) && user.Password.Equals(password));
            if (first == null)
            {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }

            return first;
        }

        public async Task AddNewUserAsync(User user)
        {
            int max;
            try
            {
                foreach (var user1 in dbContext.Users)
                {
                    if (user.Id == user1.Id)
                    {
                        max = dbContext.Users.Max(adult => adult.Id);
                        user.Id = (++max);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddPersonAsync(Adult adult)
        {
            int max;
            try
            {
                foreach (var adult1 in dbContext.Adults)
                {
                    if (adult.Id == adult1.Id)
                    {
                        max = dbContext.Adults.Max(adultA => adultA.Id);
                        adult.Id = (++max);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            dbContext.Adults.Add(adult);
            await dbContext.SaveChangesAsync();
        }

        public async Task RemovePersonAsync(int adultId)
        {
            Adult adultToRemove = dbContext.Adults.First(adult1 => adult1.Id == adultId);
            dbContext.Adults.Remove(adultToRemove);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Adult> GetAdultAsync(int id)
        {
            return dbContext.Adults.FirstOrDefault(adult => adult.Id == id);
        }
    }
}