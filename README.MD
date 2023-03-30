
# Empleyees system

Exercise – Web Api project using .Net 7 and SQL Server

## Configuration 

### Database connection - Web Api

Add the corresponding connection string for the database server in the file
[appsettings.json](https:///appsettings.json) in the project EmployeeSystem.Api.

Change the value of the key `DBConnection`, there is a default connection that should work as shown below:

```JSON
"ConnectionStrings": {
    "DBConnection": "Server=(localdb)\\mssqllocaldb;Database=EmployeeDB Trusted_Connection=True;MultipleActiveResultSets=true"
},
```
Here is another example of a connection string with a user login with a password:

```JSON
"ConnectionStrings": {
    "DBConnection": "Data Source=MIKE-PC;Initial Catalog=EmployeeDB;User Id=pruebas;Password=1234; TrustServerCertificate=True; Trusted_Connection=True;"
},
```
> Note: Passwords shouldn't be laying around like this ⚠️

## Databse Creation
You can create the database using migrations int the Package manager console in visual studio, using the following command
```bash
  Update-Database
```
> Note: Before run the command set the api project as startup project, in the package manager console select the EmployeePersistence project.

### Database script
Optionally you can create the database just executing the script [EmployeeDB.sql](https://.sql) which is a backup of the database. This way you don't have to run the EF Core migrations.

