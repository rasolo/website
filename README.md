# rasolo.azurewebsites.net [![Build Status](https://dev.azure.com/rasmusolofssons/rasolo/_apis/build/status/rasolo%20-%201%20-%20CI?branchName=master)](https://dev.azure.com/rasmusolofssons/rasolo/_build/latest?definitionId=2&branchName=master)

## About ##
A website made in Umbraco
___

## Getting Started
How to setup project on your own machine.
### Prerequisites
```
NPM: https://www.npmjs.com/get-npm
Visual Studio 2017+: https://visualstudio.microsoft.com/
```

### Installing
#### Front end
* Cd to Project root/Frontend
* Run "npm install"
* Run "npm run scss-watch"
* Run (in a separate window) "npm run browserify-watch"
___

## Deployment
* Make sure code works locally, all tests builds and the site works.
* Tag develop with release/{versionnumber}
* Create a branch release/{versionnumber} from develop
  * Bump the version number in the following files depending if it is major/minor:
  * Frontend/package-lock.json
  * Frontend/package.json
  * Rasolo.Web/Properties/AssemblyInfo.cs
* Merge release branch into stage using a new commit
* Make sure everything works with the stage database
* Merge stage into master
* Release to production on azure pipelines
