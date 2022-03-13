# Tele2ASPNET-Test

# Описание

.Net Core 3.1, vs code, ubuntu, mysql, xunit

tests - юнит-тесты.

TodoApi - сам проект ASP.NET.

# Использование

https://localhost:5001/people/id - для вывода жителя по id

https://localhost:5001/people?page=0&perPage=1000&sex=all&startage=0&endage=1000 - для вывода всех жителей с разными фильтрами. Все они опциональны.

page - номер страницы.

perPage - количество жителей на одну страницу.

sex - пол, может быть all, male или female.

startage и endage - пределы возраста.


