# Setting Up SQL Server and Configuring Local Database

This guide will help you set up SQL Server, add a connection string to your project using user secrets, and update the local database using the `dotnet-ef` tool.

---

## Prerequisites

Before proceeding, ensure you have the following installed:

- **SQL Server**: Download and install SQL Server [here](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
- **SQL Server Management Studio (SSMS)**: Download and install SSMS [here](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms).
- **.NET SDK**: Install the latest .NET SDK [here](https://dotnet.microsoft.com/en-us/download).

---

## Step 1: Download and Set Up SQL Server

1. **Download SQL Server**:
   - Visit the [SQL Server download page](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
   - Choose the edition (Developer or Express is free).

2. **Install SQL Server**:
   - Follow the installation wizard.
   - During setup, choose the "Mixed Mode" authentication option to enable both SQL Server Authentication and Windows Authentication.
   - Note the username (e.g., `sa`) and password you set during installation.

3. **Verify Installation**:
   - Open SQL Server Management Studio (SSMS).
   - Connect to the server using:
     - **Server name**: `localhost` or `localhost\SQLEXPRESS` (depending on your configuration).
     - **Authentication**: SQL Server Authentication or Windows Authentication.

---
## Step 2: Create a Database Using SQL Queries

1. Open **SQL Server Management Studio (SSMS)** and connect to your SQL Server instance.

2. In the query editor, run the following SQL command to create a database named `Jyros`:
   ```sql
   CREATE DATABASE Jyros;
 ---
## Step 3: Add Connection String in User Secrets

### Enable User Secrets for Your Project

1. In the project directory, run:
   ```bash
   dotnet user-secrets init
   ```

### Add the Connection String

2. Run the following command to set the connection string:
   ```bash
   dotnet user-secrets set "JyrosContext" "Data Source=<database_name>;Initial Catalog=Jyros;Integrated Security=True;TrustServerCertificate=True;"
   ```

   Replace the following placeholders:
   - `<database_name>`: The name of your database.
---

## Step 4: Update Local Database Using `dotnet-ef`

### Install `dotnet-ef` Tool

1. If not already installed, install the `dotnet-ef` CLI tool:
   ```bash
   dotnet tool install --global dotnet-ef
   ```

### Apply Migrations

2. Run the following command to update the local database with the latest migration:
   ```bash
   dotnet ef database update
   ```

### Verify Database Changes

3. Open SSMS and connect to your SQL Server instance.
4. Verify that the database has been created and updated with the specified tables and schema.

---

## Troubleshooting

- **Connection Issues**:
  - Double-check your connection string in user secrets.
  - Ensure SQL Server is running (`SQL Server Configuration Manager` > SQL Server Services).
  - If using `localhost`, try using `127.0.0.1` instead.

- **`dotnet-ef` Errors**:
  - Ensure migrations have been created:
    ```bash
    dotnet ef migrations add InitialCreate
    ```
  - Ensure the `dotnet-ef` tool is installed.

---

You're all set! 🎉 Your local database is now configured and ready for use with your .NET application.
