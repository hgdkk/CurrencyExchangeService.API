# Currency Exchange API
Simple .NET Core Web API to get latest currency rates and convert currencies

# Used 
• C# 

• .NET Core 3.1

• Entity Framework Core

• Online Azure SQL Server Database

• Caching

• RESTful APIs

• Unit Tests

• Logging

• Fixer API to get currency rates

• Swagger

# Introduction

• In project we used .NET Core 3.1 the create web apis and online azure SQL server database to store data.

• There are two tables in Azure SQL Server. Transactions table to store converted transactions for accounts and Logs table to log process and errors. As you can see pictures below.

![image](https://user-images.githubusercontent.com/13198189/190142053-43b6786d-f5d7-48fb-95a9-8abf1019bba4.png)

![image](https://user-images.githubusercontent.com/13198189/190142298-f4949073-9dc5-495c-b353-8c15f921da5a.png)

• Used Entity Framework Core to access data.

• Used simple distributed cache to limit convert request for every account.

• Added unit tests to check success of the methods.

• Added log table to sql server to store process and errors.

• Used Fixer API to get currency rates.

• Used Swagger UI to visualize and interact with the API's resources.


## Endpoints

• Latest Currency Rates

• Convert Currency

• Get Transaction List

• Get Log List


### Latest Currency Rates 

Returns real-time exchange rate data

#### Parameters

##### Base (optional)

Enter the three-letter currency code of your preferred base currency.

Data Type: string

##### Symbols (optional)
Enter a list of comma-separated currency codes to limit output currencies.

Data Type: string

### Convert Currency 

Currency conversion endpoint, which can be used to convert any amount from one currency to another. In order to convert currencies, please use the API's convert endpoint, append the from and to parameters and set them to your preferred base and target currency codes.

There is insertion when convert the currencies according to account number. For every convert currency request inserts data to Transaction table.

#### Parameters

##### AccountNumber (required)

The number to keep which account converted currency

Data Type: string

##### Amount (required)

The amount to be converted.

Data Type: string

##### From (required)

The three-letter currency code of the currency you would like to convert from.

Data Type: string

##### To (required)

The three-letter currency code of the currency you would like to convert to.

Data Type: string

### Get Transaction List 

Returns convertion request and result list that inserted to Transaction table.

#### Parameters

##### TransactionAccount (optional)

Account number to filter list.

Data Type: string

##### TransactionStartDate (optional)

Start date to filter list from TransactionDate column.

Data Type: DateTime

##### TransactionEndDate (optional)

End date to filter list from TransactionDate column.

Data Type: DateTime

### Get Log List

Returns log list from Log table that logged for every process and errors.

#### Parameters

##### IsSuccess (optional)

Parameter to check the success status

Data Type: bool

##### LogStartDate (optional)

Start date to filter list from LogDate column.

Data Type: DateTime

##### LogEndDate (optional)

End date to filter list from LogDate column.

Data Type: DateTime
