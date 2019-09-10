# rasolo.azurewebsites.net [![Build Status](https://dev.azure.com/rasmusolofssons/rasolo/_apis/build/status/rasolo%20-%201%20-%20CI?branchName=master)](https://dev.azure.com/rasmusolofssons/rasolo/_build/latest?definitionId=2&branchName=master)

## About ##
My personal website made in Umbraco 8.1+ hosted on Azure.

#### Some dependencies

The project uses **Browserify** for the front-end in order to make the code more modularized: http://browserify.org/.

**Umbraco Mapper** is used combined with route hijacking in order to get a more pure MVC approach and to be more similar to Episerver. https://github.com/AndyButland/UmbracoMapper.

**uSync** is used in order to make working with different environments and deployment easier. uSync stores database settings on disk which are tracked in git and can be imported in other environments. The settings that are stored are things such as document types, properties and tree structure. https://our.umbraco.com/packages/developer-tools/usync/.

**Moq** is used to make mocking and unit testing easier: https://github.com/Moq/moq4/wiki/Quickstart.

___

The front-end is located in /Frontend and the back-end is located in /src.
This is the first project where I am trying to do a TDD approach.

## Getting Started
How to setup project on your own machine.
### Prerequisites
```
NPM: https://www.npmjs.com/get-npm
Visual Studio 2017+: https://visualstudio.microsoft.com/
Browserify (If you want to build front-end): http://browserify.org/
```

### Installing

#### Back-end
* Clone this project.
* Build the project.
* Create a database:
___
Open the file: src/Rasolo.Web/web.config.
Find and replace this line:
**From:** "connectionString="server=.;database=rasmuso.azurewebsites.net;user id=rasmuso.azurewebsites.net;password=5A2068b67C" providerName="System.Data.SqlClient" />"
**To:** connectionString=""
Find and replace this line:
**From:** <add key="Umbraco.Core.ConfigurationStatus" value="8.1.0" />
**To:** <add key="Umbraco.Core.ConfigurationStatus" value="" />
(This will force Umbraco to create a new database for you when you reload the page for the site)

Open the website either by CTRL + F5 in Visual Studio or by having set it up in IIS. Umbraco will now ask you to install a database. You can choose custom install and choose a database of your choice such as SQL server, or you can let Umbraco create an .sdf database for you that is stored on disk. The credentials you enter will be automatically added to the connection string so you do not have to worry about that.

___
#### Front-end
* Cd to Project root/Frontend
* Run "npm install"
* Run "npm run scss-watch"
* Run (in a separate window) "npm run browserify-watch"



## Development
#### Naming conventions:
Private fields are named as: _camelCase.



## Deployment

* Make sure code works locally, all tests builds and the site works.
* Bump the version number in the following files depending if it is major/minor/build:

  * Frontend/package-lock.json

  * Frontend/package.json

  * Rasolo.Web/Properties/AssemblyInfo.cs
* Tag develop with release number.
* Create a branch release/{versionnumber} from develop
* Merge release branch into stage using a new commit
* Make sure everything works with the stage database
* Merge stage into master
* Release to production on azure pipelines