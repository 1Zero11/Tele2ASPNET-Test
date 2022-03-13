using System;
using System.Net;
using data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;

namespace TodoApi
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://testlodtask20172.azurewebsites.net/task");
                People.AddRange(JsonSerializer.Deserialize<Person[]>(json).Select(
                    c => {c.age = JsonSerializer.Deserialize<Person>
                    (wc.DownloadString("http://testlodtask20172.azurewebsites.net/task/" + c.id)).age; 
                    return c;}));
            }

            SaveChanges();


        }
    }
}