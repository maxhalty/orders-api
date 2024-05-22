<div align='center'>
<img src="./images/dotnet-on-aws.png" alt=".NET on AWS" title=".NET on AWS" width="150" height="100" style="display: block;margin-left: auto;margin-right: auto;" />
</div>

# Orders API

This repo has the Orders API, a Serverless Minimal API in .NET 8 with AWS Lambda that has the main purpose of creating orders and allowing to retrieve them by id.

## Overview

The solution has the following structure:

```
|-- OrdersAPI
    |-- cdk
        |-- infraestructure
        |-- lambdas
    |-- src
        |-- Apis
            |-- OrderAPI.Common
            |-- OrderAPI.GetByOrderId
            |-- OrderAPI.Post
        |-- OrderAPI.Application
        |-- OrderAPI.Domain
        |-- OrderAPI.Infraestrcture
    |-- tests
        |-- OrderAPI.Application.Tests
        |-- OrderAPI.Domain.Tests
        |-- OrderAPI.Infraestrcture.Tests
```

In the `cdk` folder you could find all the IaC associated with this repository, including a json with the associated Tags that will be added to the resources. Is divided in two sections, `infraestructure` with the IaC of api gateways and dynamodb, and `lambdas` where the IaC of the lambdas is located. 

The `tests` folder contains all the unit tests of the .net code located in the `src` folder. For this project I used xunit and the coverage is sent to sonarqube in order to be analyzed for the quarlity gate.

The `src` folder is divided in several projects. For this minimal api I used CQRS pattern, so you will find that the Commands and the Queries are independent from each other, and also the MediatR library is used to implement the Mediator pattern in order to simplify the implementation of CQRS. 