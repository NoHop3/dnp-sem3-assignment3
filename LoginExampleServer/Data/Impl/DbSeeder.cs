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
    public class DbSeeder : IUserService, IAdultService
    {
        private AdultDBContext dbContext;
        public IList<Adult> AdultsList { get; private set; }
        public IList<User> UsersList { get; private set; }
        private readonly string adultsFile = "adults.json";
        private readonly string usersFile = "users.json";

        public DbSeeder()
        {
            dbContext = new AdultDBContext();
            Seed();
        }
        
        public void Seed()
        {
            
            Console.WriteLine("Inserting adults..");
            AdultsList = File.Exists(adultsFile) ? ReadData<Adult>(adultsFile) : new List<Adult>();
            AddAdults();
            Console.WriteLine("Done!");
            Console.WriteLine("Inserting users..");
            UsersList = File.Exists(usersFile) ? ReadData<User>(usersFile) : new List<User>();
            AddUsers();
            Console.WriteLine("Done!");
            Console.WriteLine("Inserting jobs...");
            //AddJobs(Adults);
            Console.WriteLine("Done");
        }

        private void AddAdults()
        {
            foreach (Adult adult in AdultsList)
            {

                    dbContext.Adults.Add(adult);
                    dbContext.Entry(adult).State = EntityState.Added;
                    dbContext.SaveChanges();
            }
        }

        private void AddUsers()
        {
            foreach (User user in UsersList)
            {
                dbContext.Users.Add(user);

                dbContext.Entry(user).State = EntityState.Added;
                dbContext.SaveChanges();
            }
        }

        private void AddJobs(IList<Adult> adults)
        {
            foreach (Adult adult in adults)
            {

               // dbContext.Jobs.Add(adult.JobTitle);
                //dbContext.SaveChanges();
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
            List<Adult> temp = new List<Adult>(AdultsList).OrderBy(adult => adult.Id).ToList();
            return temp;
        }

        public async Task<IList<User>> GetUsersAsync()
        {
            List<User> temp = new List<User>(UsersList);
            return temp;
        }
        public async Task<User> ValidateUserAsync(string userName, string password)
        {
            User first = UsersList.FirstOrDefault(user => user.UserName.Equals(userName) && user.Password.Equals(password));
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
                foreach (var user1 in UsersList)
                {
                    if (user.Id == user1.Id)
                    {
                        max = UsersList.Max(adult => adult.Id);
                        user.Id = (++max);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            UsersList.Add(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddPersonAsync(Adult adult)
        {
            int max;
            try
            {
                foreach (var adult1 in AdultsList)
                {
                    if (adult.Id == adult1.Id)
                    {
                        max = AdultsList.Max(adult => adult.Id);
                        adult.Id = (++max);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            AdultsList.Add(adult);
             await dbContext.SaveChangesAsync();
        }

        public async Task RemovePersonAsync(int adultId)
        {
            Adult adultToRemove = AdultsList.First(adult1 => adult1.Id == adultId);
            AdultsList.Remove(adultToRemove);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Adult> GetAdultAsync(int id)
        {
            return AdultsList.FirstOrDefault(adult => adult.Id == id);
        }
    }
}