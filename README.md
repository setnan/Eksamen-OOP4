# 🧠 OOP Exam – ASCII Conversion & Job Matching

Welcome to my submission for the OOP4 exam. This solution includes two console applications focusing on algorithmic thinking, data handling, and API interaction.

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

Optional: Set environment variables if needed

```powershell
$env:EXAM_API_KEY="..."
$env:EXAM_API_URL="..."
```

---

## 🔢 Question 1 – ASCII Conversion & Luhn Algorithm

A console application that transforms a string (e.g. a name) into ASCII codes and applies the **Luhn algorithm** to calculate and append a checksum digit. The application provides interactive visual feedback using **Spectre.Console**.

### 🔧 Architecture

* `AsciiConverter.cs` – maps characters to ASCII values
* `LuhnCalculator.cs` – applies Luhn algorithm
* `UIHelper.cs` – handles console visuals

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

* `ExamTaskService.cs` – handles API retrieval
* `MatchService.cs` – logic for determining best matches
* `Menu.cs` – console menu UI

### 📋 Concepts Demonstrated

* API interaction using `HttpClient`
* JSON deserialization
* LINQ filtering and scoring
* Console-based user navigation with Spectre.Console

---

## 🧠 Reflections

### Simon Etnan – Reflection

> This has been a great project to work on. I enjoyed building both low-level logic and higher-level architectural flow. I think Spectre.Console was a great library to learn and helped me present data intuitively. If I had more time I would like to make this into an Avalonia application or Web application.

---

## 🖥️ Platform

Developed and tested on **Windows 11**. Please evaluate in a Windows environment to match console UI rendering.

---

## 🤖 Use of AI

* Conceptual help understanding the Luhn algorithm
* Guidance on project structure and file organization
* Inspiration and formatting tips for README presentation
* Conceptual sparring to explore unfamiliar features and techniques.
* AI-assisted reflections and conversations served as inspiration and guidance, with no direct code copied.

> All AI suggestions were manually reviewed and selectively applied.

---

## 🔗 API Endpoint

```
https://exam.05093218.nip.io/api/ExamTask
```

API key is included in the project and may also be provided via environment variables.

---
