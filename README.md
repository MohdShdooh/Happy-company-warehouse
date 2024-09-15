## Installation
- you need to install the following packages for FrontEnd : 
  	- npm install 
	  - npm install bootstrap
- you need to run this commands For migration in BackEnd :
	- add-migration initialCreation -o Migrations
	- update-database

## Implementation Details

### Backend (ASP.NET Core 7)

- **Database**: Created using the Code-First approach with migrations.

- **Design Pattern**: Repository design pattern with a layered architecture.

- **API Layer**: Handles API requests and responses.

- **Business Logic Layer (BAL)**:
  - Contains two folders: `Interfaces` and `Services`.
  - Interfaces for `Users` , `WareHouses` and `Items` are implemented in the Services folder .

- **Data Access Layer (DAL)**:
  - **DBContext**: Contains `WareHouseContext` with model builders for the database.
  - **Migrations**: Folder containing database migration files.
  - **Entities**: Folder with `Users`, `WareHouse` and `Items` entities that are migrated to database tables.
  - **Repository**: Folder with `UsersRepository` , `WareHouseRepository` and `ItemsRepository`, each containing an interface and its implementation.

- **Entities Layer**:
  - Contains a `DTOs` folder for data models and a `Tools` folder for utility classes, like the `ErrorCodes` enum.

### Frontend (Angular 18.2.4)

- **Manage Dashboard Page**: Displays a statistices page that have a top 10 quentity items and less 10 for all warehouses for specifice user .

- **Components**:
  - **Users**: this component have :
	- **Add user component**: this will add new user .
	- **Edit user component**: this will Edit the user .
	- **Change password component**: this will update the password for specifice user .
	- **list users component**: this will list all users in the system .
	- we will delete the user by showing a popup dialog then the user will deleted without reload the page .

- **WareHouse**: this component have :
	- **Add warehouse component**: this will add new warehouse .
	- **list warehouses component**: this will list all warehouses for specifice user .
	- **view component**: this will open the listing Items component  .

 - **Items**: this component have :
	- **Add Item component**: this will add new Item .
	- **list Items component**: this will list all Items for specifice warehouse .

- **Login**: this component responsible for authenticate the user . 

- **sidebar**: this will appear on all dashboards children .

- **Models folder**: Contains objects for requests, responses, API endpoints, and Etities .
-Note :- when you want to change the apis end points Enter to the Apis class through this folder and change it from one place only and i handeld it to change the base url for all apis .

- **Service folder**: this folder have all services that will hit apis each service injectd in own component.


