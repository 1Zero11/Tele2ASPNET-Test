using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;

namespace data
{
    /// <summary>
    /// Больше не используется
    /// </summary>
    public class DBManager
    {
        private List<Person> people = new List<Person>();

        public async void LoadData(){
            using (WebClient wc = new WebClient())
            {
                var json = await wc.DownloadStringTaskAsync("http://testlodtask20172.azurewebsites.net/task");
                people = new List<Person>(JsonSerializer.Deserialize<Person[]>(json));
            }

            foreach(Person p in people)
                p.age = await GetAgeById(p.id);
        }

        public IEnumerable<Person> GetAll(string sex = "all", int startage = 0, int endage = int.MaxValue){

            return from g in people
                    where sex=="all" ? true : g.sex == sex 
                    && g.age >= startage 
                    && g.age <= endage
                    select g;
        }

        public Person GetPersonById(string id){
            return (from g in people
                    where g.id == id
                    select g).First(); 
        }

        private async Task<int> GetAgeById(string id){
            using (WebClient wc = new WebClient())
            {
                var json = await wc.DownloadStringTaskAsync("http://testlodtask20172.azurewebsites.net/task/" + id);
                return JsonSerializer.Deserialize<Person>(json).age;
            }
        }
    }
}