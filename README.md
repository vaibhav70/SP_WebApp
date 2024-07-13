# Spaulding Ridge - Sales Forecast Tool

## Overview
This application provides a simple sales forecasting tool using ASP.NET Web Forms and SQL Server.

## Technologies Used
- ASP.NET Web Forms (.NET Framework)
- C#
- MS SQL Server

## How to Run
1. Clone the repository:
   ```bash
   git clone https://github.com/vaibhav70/SP_WebApp.git
   ```
2. Open the solution file `SP_WebApp.sln` in Visual Studio.
3. Ensure the database connection string in `Web.config` is correct.
4. Execute the SQL script `Db Scripts` to create the database.
5. Run the application from Visual Studio.

## Database Setup
Run the following scripts in order:
Create table T_Orders, T_OrderReturns and then T_Products

## Features
- Query sales data by year.
- Apply a percentage increase to the sales data.
- Export forecasted sales data to CSV.

## Caveats
- Ensure the SQL Server instance is running and accessible.
- Modify the database connection string in `Web.config` if necessary.
