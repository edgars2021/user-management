Database already created !!!

How run App?
1. cd UserManagementApp.Host
2. dotnet run

If database does not exist then depend migration existence you should run just step 3.
1. go to folder UserManagementApp.Core
2. dotnet ef migrations add InitialCreate
3. dotnet ef database update
