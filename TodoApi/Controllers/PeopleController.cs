using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using data;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace TodoApi.Controllers
{
    [Route("people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly DBManager dBManager;
        private readonly ILogger<PeopleController> _logger;
        private readonly IPersonService _personService;
        public PeopleController(DBManager dbm, ILogger<PeopleController> logger, IPersonService personService){
            dBManager = dbm;
            _logger = logger;
            _personService= personService;
        }

        
        [HttpGet()]
        public IEnumerable<Person> GetAll(
            int page = 0, int perPage = int.MaxValue,
            string sex = "all",
            int startage = 0, int endage = int.MaxValue)
        {
            // Возраст выводить не нужно, но можно?)
            
            //return dBManager.GetAll(sex: sex, startage: startage, endage:endage );
            return _personService.GetPeople(page: page, perPage: perPage, sex: sex, startage: startage, endage:endage);
        }

        [HttpGet("{id}")]
        public Person GetPerson(string id)
        {   
            //Person person = dBManager.GetPersonById(id);
            //_personService.AddPerson(person);
            return _personService.GetPerson(id);
        }

    }

}