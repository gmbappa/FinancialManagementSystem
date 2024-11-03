# Financial Management System

## Project Overview

The **Financial Management System** (FMS) is designed to provide users with comprehensive tools to manage financial records, including journal entries and chart of accounts. This web application allows users to create, view, and report on financial data efficiently.

### Key Features and Functionalities Implemented

- **User Authentication:** Secure login/logout functionality to manage user sessions.
- **Chart of Accounts:** Users can view, create, and manage accounts.
- **Journal Entries:** Users can create, view, and report on journal entries.
- **Responsive Design:** A user-friendly interface that works on various devices and screen sizes.
- **Data Validation:** Strong validation mechanisms for user inputs to ensure data integrity **FluentValidation**.

## Architecture Explanation

The Financial Management System employs **Onion Architecture** (or **Clean Architecture**) with **CQRS (Command Query Responsibility Segregation)** principles. This architectural pattern helps to separate the application's concerns, making it easier to maintain, test, and scale.

### Description of Architecture

- **Onion Architecture:** This architecture promotes a layered approach where the application core is at the center, surrounded by layers of dependencies. This separation allows for a clear distinction between the business logic and the infrastructure.

- **CQRS:** This pattern divides the application into two distinct parts: 
  - **Commands:** Handle all operations that modify data.
  - **Queries:** Handle all operations that retrieve data without changing it.

### Project Structure

- **Core Layer:** Contains business logic and domain entities. It is independent of other layers.
- **Application Layer:** Contains application services and the implementation of CQRS.
- **Infrastructure Layer:** Deals with data access, external services, and implementations of repositories.
- **Presentation Layer:** Contains the UI components/API, including controllers and views (Razor views).

```markdown
FinancialManagementSystem
│
├── Core
│   ├── Entities
│   ├── Interfaces
│
├── Application
│   ├── Commands
│   ├── Queries
│   ├── Handlers
│
├── Infrastructure
│   ├── Repositories
│   ├── Data
│
├── Presentation
    │── API
	│   ├── Controllers
	│   ├── Models
	│── UI
	│   ├── Controllers
	│   ├── Views
	    ├── Scripts
		├── Validator
		├── Mapper
```
### How Different Layers Interact

1. The **Presentation Layer** communicates with the **Application Layer** to send commands or queries.
2. The **Application Layer** processes requests and communicates with the **Core Layer** to perform business logic.
3. The **Core Layer** interacts with the **Infrastructure Layer** to retrieve or store data.

## Setup Instructions

### Prerequisites

Before you begin, ensure you have met the following requirements:

- .NET SDK (version 4.8 or higher)
- Visual Studio 2019 or newer (with ASP.NET workload)
- SQL Server 2019/2022 or SQL Server Express
- Any additional libraries or tools as required by the project

### Step-by-Step Guide on How to Set Up the Development Environment

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/yourusername/FinancialManagementSystem.git
   cd FinancialManagementSystem```
 
1. **Install dependencies:**

- Open the solution in Visual Studio and restore NuGet packages.   


**Configure Database:**

- Update the connection string in Web.config with your database credentials at API project.
- Create database and table from db.sql that present in 'DocsFolder' at 'DB Folder'.
- Running the Application Locally
- Launch the application:
- Use Visual Studio to run the application or run dotnet run in the terminal.
- API and UI Project Set As Start  from Properties/Multiple StartUp Project. 

**Access the app:**


The application should be accessible at http://localhost:60371/ by default for UI and http://localhost:55312/  for API.

**API Documentation**

List of API Endpoints

Base URL : http://localhost:55312/api/v1/

**Authentication**

- POST /Authentication/Login - Logs in a user, returning a JWT token.

**Chart of Accounts (COA)**

- GET /ChartOfAccounts/GetActiveAccounts - Retrieves a list of COA entries.
- POST /ChartOfAccounts/Add - Creates a new COA entry.
- GET /ChartOfAccounts/Details/{id} - Retrieves details of a specific COA entry.
- GET /ChartOfAccounts/GetReportData{queryString} - Retrieves details of report COA entry.

**Journal Entries**

- GET /JournalEntry/GetAllEntries - Retrieves journal entries with pagination.
- POST /JournalEntry/Add - Creates a new journal entry.
- GET /JournalEntry/Details/{id} - Retrieves details of a specific journal entry.
- GET /JournalEntry/GetReportData{queryString} - Retrieves details of report journal entry.


**Request and Response Formats**

Login Request

```json
Copy code
POST /Authentication/Login
{
  "username": "admin",
  "password": "123"
}
```
Login Response

```json
Copy code
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Authentication Details**

Explanation of JWT Token Implementation

JWT is used to secure API endpoints. After a user logs in, they receive a token that must be included in the Authorization header of requests.

How to Obtain and Use the Token

Example:

```http
Copy code
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```
**Pagination Implementation**

Details on Pagination Handling

- API Pagination: Use page and pageSize query parameters to fetch paged results.
- UI Pagination: Pagination controls allow users to navigate through pages seamlessly in the interface.

**Adjustable Parameters or Settings**

- Default Page Size: 10 items per page.
- Custom Page Size: Modify pageSize query parameter to retrieve different numbers of records.

**Reporting Features**

Instructions on Accessing and Using Reporting Functionalities
Navigate to the sidebar to access reporting options for both the Chart of Accounts and Journal modules. Upon accessing a report, you’ll see aggregated financial data, which can also be exported.

**Database Schema**

Overview of the Database Design

The system uses a normalized database schema to store financial data securely.

**Tables and Relationships**

- Users
   Stores user information and authentication details.
   
- ChartOfAccounts
  Stores account categories and types.
  
- JournalEntries
  Stores journal headers, including date, description, and reference number.
  JournalEntryLines

Stores individual line items in each journal entry, including account name, debit/credit amount, and relationship to a journal entry.

**Relationships Between Tables**

- Users to JournalEntries: One-to-Many
- JournalEntries to JournalEntryLines: One-to-Many
