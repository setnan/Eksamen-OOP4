# 🧠 OOP Exam – ASCII Conversion & Job Matching

Welcome to my submission for the OOP 1st year backend exam. 
This project includes two console applications written in C#(.NET), focused on algorithmic logic, data handling, and API integration.

**Created by znan**

---

## 🗂️ Project Structure

* **Question-1/** – ASCII to number conversion and Luhn checksum calculator
* **Question-2/** – API integration and job-matching logic

---

## ⚙️ Project Requirements

* .NET 9.0 SDK
* Internet access for API retrieval

---

## 🚀 Getting Started

### 🧪 Setup Steps

1. **Clone the repository** *(or extract the .zip if provided)*

```bash
git clone git@github.com:setnan/Eksamen-OOP4.git
cd Eksamen-OOP4
```

2. **Install required NuGet packages**

Spectre.Console is a powerful library used to create rich, interactive, and visually appealing terminal applications.

```bash
dotnet add package Spectre.Console
```

3. **Build the solutions**

```bash
dotnet build
```

4. **Run Question 1** – ASCII to Luhn

```bash
cd Question-1
dotnet run
```

5. **Run Question 2** – API Matching System

```bash
cd Question-2
dotnet run
```

---

## 🔢 Question 1 – ASCII Conversion & Luhn Algorithm

A console application that transforms a string (e.g. a name) into ASCII codes and applies the **Luhn algorithm** to calculate and append a checksum digit. The application provides interactive visual feedback using **Spectre.Console**.

### 🔧 Architecture

- `AsciiConverter.cs` – converts input to combined ASCII digit string
- `LuhnCalculator.cs` – calculates Luhn check digit from ASCII string
- `Program.cs` – controls program flow and connects components
- `AppConfig.cs` – loads config and UI labels from appsettings.json
- `appsettings.json` – defines UI prompts, labels, and colors


### 📋 Concepts Demonstrated

* Encoding (ASCII)
* Flow control (loops, conditionals)
* Algorithm implementation (Luhn)
* Separation of concerns with a modular structure

---

## 🌐 Question 2 – API Interaction & Job Matching

This application retrieves randomized data from an external API, deserializes JSON into C# objects, and matches applicants to positions based on:

* Title
* Seniority
* Skills and Specialization

### 🔧 Architecture

- `ExamTaskService.cs` – fetches and deserializes API data
- `MatchService.cs` – matches applicants to positions
- `Program.cs` – controls flow and user interaction
- `AppConfig.cs` – loads API settings from config or environment
- `appsettings.json` – contains API key and base URL


- `Models/Applicant.cs` – represents job applicant
- `Models/Position.cs` – represents a job position
- `Models/ExamData.cs` – container for API data (applicants and positions)


### 📋 Concepts Demonstrated

* API interaction using `HttpClient`
* JSON deserialization
* LINQ filtering and scoring
* Console-based user navigation with Spectre.Console

---

## 🧠 Reflections

### Simon – Reflection

> This project has been really fun to work on. I liked the mix of building logic from scratch and organizing everything into a clean structure. Learning Spectre.Console was a cool bonus – it made the terminal experience way more dynamic. If I had more time, I’d make this into a full Avalonia or web app, just to see how far it could go.

---

## 🖥️ Platform

Developed and tested on **Windows 11**. Please evaluate in a Windows environment to match console UI rendering.

---

## 🤖 Use of AI

* Conceptual help understanding the Luhn algorithm
* Guidance on project structure and file organization
* Inspiration and formatting tips for README presentation
* Conceptual sparring to explore unfamiliar features and techniques.

> AI-assisted reflections and conversations served as inspiration and guidance, with no direct code copied.

---

## 🔗 API Endpoint

```
https://exam.05093218.nip.io/api/ExamTask
```

API key is included in the project and may also be provided via environment variables.

---
