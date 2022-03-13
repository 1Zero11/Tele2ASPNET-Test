using System;
using Xunit;
using TodoApi;
using TodoApi.Controllers;
using System.Data;
using data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;

namespace tests
{
    public class UnitTest1
    {
        PeopleController controller;
        public UnitTest1(){
            DBManager dbmanager = new DBManager();

            string mySqlConnectionStr = "Server=localhost; Uid=people-admin; Pwd=password; Database=People";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            var options = optionsBuilder
                    .UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr))
                    .Options;
            ApplicationContext context = new ApplicationContext(options);

            IPersonService service = new PersonService(context);
            controller = new PeopleController(dbmanager, null, service);
        }

        [Fact]
        public async void DataTest()
        {

            using (WebClient wc = new WebClient())
            {
                var json = await wc.DownloadStringTaskAsync("http://testlodtask20172.azurewebsites.net/task");
                List<Person> expectedPeople = new List<Person>(JsonSerializer.Deserialize<Person[]>(json));

                foreach(Person p in expectedPeople){
                    Person dbPerson = (from dbp in controller.GetAll()
                                      where dbp.id == p.id
                                      select dbp)
                                      .First();
                    Assert.Equal(p.name, dbPerson.name);
                    Assert.Equal(p.sex, dbPerson.sex);
                }
            }

        }

        [Theory]
        [InlineData("male")]
        [InlineData("female")]
        [InlineData("all")]
        [InlineData("srgerhrtjxbsrg")]
        [InlineData("")]
        public void SexFilterTest(string sex)
        {
            List<Person> people = new List<Person>(controller.GetAll(sex: sex));

            bool rightSex = true;

            foreach(Person p in people)
                if(p.sex!=sex&& sex!="all")
                    rightSex = false;

            Assert.True(rightSex);

        }

        [Theory]
        [InlineData(0, 4, 4)]
        [InlineData(3, 4, 1)]
        [InlineData(12, 4, 0)]
        [InlineData(0, int.MaxValue, 13)]
        [InlineData(-10, int.MaxValue, 0)]
        [InlineData(1, -2, 0)]
        public void PaginationTest(int page, int perPage, int itemsExpected)
        {
            List<Person> people = new List<Person>(controller.GetAll(page: page, perPage: perPage));

            Assert.Equal(itemsExpected, people.Count);

        }

        [Theory]
        [InlineData(30, 40)]
        [InlineData(15, 15)]
        [InlineData(-2, 2)]
        [InlineData(40, 20)]
        [InlineData(int.MinValue, int.MaxValue)]
        public void AgeTest(int sage, int eage)
        {

            List<Person> people = new List<Person>(controller.GetAll(startage: sage, endage: eage));
            bool test = true;

            foreach (Person p in people)
                if (p.age < sage || p.age > eage)
                    test = false;

            Assert.True(test);

        }
    }
}
