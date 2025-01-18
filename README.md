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
   dotnet user-secrets set "JyrosContext" "Data Source=<server_name>;Initial Catalog=Jyros;Integrated Security=True;TrustServerCertificate=True;"
   ```

   Replace the following placeholders:
   - `<server_name>`: The name of your server.
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

## Step 5: Add Mock Data to the Database

After setting up the database and applying migrations, you can insert mock data to test your application.

1. Open **SQL Server Management Studio (SSMS)** and connect to your SQL Server instance.

2. In the query editor, select the `Jyros` database, and run the following SQL commands:

```sql
-- Adding mock data for Users
INSERT INTO Users (username, Password) VALUES
('Alice', 'pass1'),
('Bob', 'pass2'),
('Charlie', 'pass3'),
('Diana', 'pass4');

-- Adding mock data for Teams
INSERT INTO Teams (team_name, team_description, team_lead_id) VALUES
('Team Alpha', 'Handles Alpha projects', 1),
('Team Beta', 'Focuses on Beta tasks', 2);

-- Adding mock data for UsersTeams
INSERT INTO UsersTeams (user_id, team_id) VALUES
(1, 1),
(2, 1),
(3, 2),
(4, 2);

-- Adding mock data for Sprints
INSERT INTO Sprints (name, goal, start_date, end_date, status, team_id) VALUES
('Sprint 1', 'Complete initial setup', '2023-01-01', '2023-01-15', 'Active', 1),
('Sprint 2', 'Develop core features', '2023-01-16', '2023-01-31', 'Active', 2);

-- Adding mock data for Stories
INSERT INTO Stories (title, description, status, parent_id, sprint_id, created_by, story_points) VALUES
('Setup database', 'Create and configure the database', 'To Do', NULL, 1, 1, 5),
('Build API', 'Develop the API for the app', 'To Do', NULL, 2, 2, 8);

-- Adding mock data for UsersStories
INSERT INTO UsersStories (story_id, user_id) VALUES
(1, 1),
(2, 2);

-- Adding mock data for TeamMemberAvailabilities
INSERT INTO TeamMemberAvailabilities (user_id, sprint_id, availability_points) VALUES
(1, 1, 20),
(2, 1, 15),
(3, 2, 18),
(4, 2, 22);

-- Adding mock data for Adjustments
INSERT INTO Adjustments (sprint_id, adjustment_points, reason) VALUES
( 1, -2, 'lmao'),
( 1, 3, 'UwU'),
( 2, 0, 'bruh'),
( 2, 1, 'bro dieded');
```

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
