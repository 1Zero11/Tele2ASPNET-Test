
using System.Collections.Generic;
using data;

namespace TodoApi
{
    public interface IPersonService
    {

        IEnumerable<Person> GetPeople(int page, int perPage, string sex = "all", int startage = 0, int endage = int.MaxValue);

        Person GetPerson(string Id);
    }
}
