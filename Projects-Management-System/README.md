# ProjectsPortal

This repository describes ProjectsPortal Api

## User stories

1.I want to be able to register with my username and password and enter the portal using them;
 
2.As a user i want to create projects with some parametrs such as budget, budget source, project manager identification and information about where the project will be implemented, etc;

3.As a user I want to be able to see the entire list of projects and search for projects by the given parameters;

4.As a user I want to make changes into my created projects;

5.As admin I want to administrate all users list which have been registered on the Projects Portal;

6.As admin I want to have access to Project portal logs

##Term

-BudgetSourse - source of funding for project implementation;
-BusinessUnit - identifier of the business implementing the project;
-User - project manager or Projects Portal administrator

##Models

###BudgetSource

-BudgetSourceID 
-BudgetSourceName 

###BusinessUnit

-BusinessUnitID 
-BusinessUnitName 

###District

-DistrictID 
-DistrictName 

###LogOperations

-LogID 
-LogSituation 
-UserID 
-Process 
-DateTime 
-UserIp 
-Statement 

###Project

-ProjectId 
-UserID
-BusinessUnitID 
-Budget 
-Title 
-Description 
-IsActive 
-BusinessUnit 
-User 

###User

-UserID 
-UserFirstname 
-UserSecondname 
-UserEmail 
-UserRole
-IsActive 
-UserDepartment 
-UserPassword 
