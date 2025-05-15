# OOP Exam - ASCII - Luhn Algorithm & Job-Matching

This project consists of two console applications developed for the OOP4 exam. Below is a description of the solutions.

## Question 1 - ASCII Conversion & Luhn Algorithm

A clean console application that converts a text string (e.g., a name) into ASCII values and then calculates a check digit using the Luhn algorithm. The program presents the conversion in a clear, colorful table and gives the user immediate visual feedback at each step. The user can press `Q` to exit the application.

### Techniques and Skills Demonstrated

In Question 1, I demonstrate both fundamental C# programming and more advanced application architecture and user experience concepts. The code is structured in a modular architecture with clear separation of concerns:

#### Architecture and Modules
* **Core Logic**
  * `AsciiConverter`: Handles conversion from text to ASCII values
  * `LuhnCalculator`: Implements the Luhn algorithm for check digit calculation
* **Configuration System**
  * `appsettings.json` with strongly typed configuration classes
  * Automatic loading and fallback if configuration is missing
* **UI Abstraction**
  * `UIHelper` class for all UI-related operations
  * Spectre.Console is used for structured and colored terminal output

### Prerequisites

#### Required Software
- **.NET 9.0 SDK** or later
  - This project targets .NET 9.0, which is a newer version. Make sure you have the appropriate SDK installed.
  - You can download it from: https://dotnet.microsoft.com/download
  - Check your installed version with: `dotnet --version`

#### Required Packages
- **Spectre.Console** - Used for enhanced terminal UI
  ```bash
  dotnet add package Spectre.Console
  ```
  Or install using the NuGet Package Manager in Visual Studio.

### Running Question-1

```bash
cd Question-1
dotnet run
```

1. Enter a word or name
2. View ASCII values and Luhn check digit
3. Press `Q` to quit

## Question 2 - API Interaction & Job Matching

An application that retrieves applicants and job positions from an external API and matches them based on title, seniority, specialization, and skills.

### Techniques and Skills Demonstrated

- Asynchronous API calls using `HttpClient`
- Deserialization with `System.Text.Json`
- Matching logic using LINQ and scoring
- Spectre.Console for menus, tables, and styling
- Scalable architecture using `Models`, `Services`, `UI`, and `Configuration`

### Running Question-2

```bash
cd Question-2
dotnet run
```

1. Data is automatically retrieved from the API
2. Navigate the menu using arrow keys
3. Interactively explore applicants and matches

### Using Environment Variables (Optional)

```powershell
$env:EXAM_API_KEY="..."
$env:EXAM_API_URL="..."
dotnet run
```

If environment variables are not set, values from `appsettings.json` will be used.

## Platform
Developed and tested on Windows. Recommended to be evaluated on the same platform.

## Use of AI
- Refactored method and class names
- Project and README structure guidance
- UI improvements with Spectre.Console

All AI suggestions have been manually reviewed and selectively implemented.

## Project Structure

```
Eksamen-OOP4/
├── Question-1/
│   ├── AsciiConverter.cs
│   ├── LuhnCalculator.cs
│   ├── UI/UIHelper.cs
│   ├── Configuration/AppConfig.cs
│   └── Program.cs
│
├── Question-2/
│   ├── Models/Applicant.cs, Position.cs, ExamData.cs
│   ├── Services/ExamTaskService.cs, MatchService.cs
│   ├── UI/Menu.cs
│   ├── Configuration/AppConfig.cs
│   └── Program.cs
└── README.md
```

## API Endpoint
```
https://exam.05093218.nip.io/api/ExamTask
```
The API key is included in the project and can also be set via environment variables.
