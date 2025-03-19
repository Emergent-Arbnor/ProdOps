# <span style="color:#5F99AE">ProdOps (A Production Operations Hub)</span>

*Work in progress. Constantly updating and refining.*

This project is a hobby project to create software that mimicks a production environment. In the website, you are able to:

- Create new accounts
- Remove account
- Administrate or watch the production in real time

## Frontend using Vite / Typescript + React

An authenatication context is used to keep track of a user being logged in. The user is sent to the dashboard page whenever logging in.

The frontend calls the backend through an API to get infromation from the backend.

## Backend using ASP.NET

The backend is developed using ASP.NET Core 8.0 with C#.

By default, if there are duplicate keys in a JSON requestm ASP.NET picks the last key value pair. Created a custom CustomJsonStream.cs class to intercept the JSON data stream from the HTTP request to check whether there are any duplicates in the JSON request. If there is a duplicate, an error is thrown.

### Postgres database
The database currently has 2 tables with the following structure:

- **Users** - contains the users of the application
    - ID - primary key - serial
    - Username - Varchar(20)
    - Password - TEXT

The password is encrypted using BCrypt.

- **Articles** - contains the articles and its count in the production facility
    - ID - primary key - serial
    - article_name - Varchar(20)
    - article_count - TEXT
