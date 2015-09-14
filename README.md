# nintx-url-shortener
A simple URL Shortener.  Allows anonymous users to submit URLs to shorten as well as automatically redirects to original URL if short code is provided.

## Tech Stack
ASP.NET MVC 4 web application with Windows Azure web hosting and database backend.

## Windows Azure Setup
### Web App
* Create a new web application:
** Select a URL: http://nintxurlshortener.azurewebsites.net/ used in this repository.
** Select a Database: Microsoft SQL chosen in this repository.
** Publish from source control: GitHub chosen
### Database
* Create one table, "Urls":
** Column: "ID" - type 'int', Identity, Required, Primary Key
** Column: "EncodedUrl" - type 'nvarchar(max)'
** Column: "OrigUrl" - type - nvarchar(max)', Required
* Set a Windows Azure firewall rule for your personal computer IP address (in order to connect to the database while testing the app locally)
* Update web.config with the SQL database connection string provided by Azure


## Enhancement List
* Logging
* Dependency injection for URL backend (result: decoupled unit tests for MVC controllers)
* Create database scripts
* Add user accounts