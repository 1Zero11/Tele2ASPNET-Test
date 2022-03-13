
    using System.Collections.Generic;  
    using System.Linq;  
    using data;

namespace TodoApi 
    {  
        public class PersonService : IPersonService  
        {  
            public ApplicationContext _applicationContext;  
            public PersonService(ApplicationContext PersonDbContext)  
            {  
                _applicationContext = PersonDbContext;  
            }  

            public IEnumerable<Person> GetPeople(int page, int perPage, string sex = "all", int startage = 0, int endage = int.MaxValue)  
            {  
                var query = from g in _applicationContext.People.ToList()
                    where (sex=="all" ? true : g.sex == sex) 
                    && g.age >= startage 
                    && g.age <= endage
                    select g;

            return Page(query, page: page, perPage: perPage);

        } 

            private IEnumerable<Person> Page(IEnumerable<Person> arr, int page, int perPage){
                if(page<0 || perPage<=0)
                    return new Person[0];
                    
                return arr.Skip(page * perPage).Take(perPage);
            }
      
            public Person GetPerson(string Id)  
            {  
                return _applicationContext.People.FirstOrDefault(x => x.id == Id);  
            }  
      
        }  
    }  
