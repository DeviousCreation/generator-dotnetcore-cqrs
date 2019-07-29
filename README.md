# generator-dotnetcore-cqrs
A Yeoman generator for .net core using CQRS, Mediatr, Entity Framework and Dapper

## Information
This generate is a multi project solution, geared at kick started a domain drive web project using the CRRS pattern.  It made up of the following parts:

### Domain
This is developed with the aggregate root pattern with state mutation performed with mediatr requests. It is database agnostic and so it is designed to user both the repository and unit of work patterns.

### Infrastucture
This component is where the implementation of the repositories sit, it is also the place where Entity Framework is wired up to provide the domain with data.

### Queries
This is where reading of immutable data is set up.  I currently runs via Dapper

### Web
This is a standard MVC project. It has been pre configured to use Feature Folders and Stackify's Prefix to aid with debugging.  For the client side Webpack is used to complie all resources. It is setup to use Sass and Typescript.

## Getting Started

- Dependencies:
    - Node.js
    - Yeoman: `npm install -g yo`
- Install: `npm install -g generator-dotnetcore-cqrs`
- Run: `yo sitecore`

> **NPM** https://www.npmjs.com/package/generator-sitecore