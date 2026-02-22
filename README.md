# Classes Map Visualizer

<!-- [![Stars](https://img.shields.io/github/stars/Mo-Moallem/FileVisualizer?style=flat)](https://github.com/Mo-Moallem/FileVisualizer/stargazers) -->

<!--[![License](https://img.shields.io/github/license/Mo-Moallem/FileVisualizer)](LICENSE)-->

![Windows](https://img.shields.io/badge/Platform-Windows-0078D6?logo=windows\&logoColor=white)
![C#](https://img.shields.io/badge/-C%23-239120?logo=csharp\&logoColor=white)
![.NET](https://img.shields.io/badge/-.NET%209-512BD4?logo=dotnet\&logoColor=white)
![WinForms](https://img.shields.io/badge/-WinForms-5C2D91)

A small Windows Forms (.NET 9) application that parses an Excel file including data about different sections of different classes and, given their CRNs (ID for a section or different sections that are strictly enrolled with each other), visualizes an approximate map of the different buildings you need to go through on a specific day.

## Demo

![App demo](assets/demo.gif)

## Why this project is useful

* Helps choose classes and sections to enroll in by comparing different combinations and their paths at the university.

## Features

* Reads an Excel file of 3,717 rows and 13 columns and parses it in 1,333 ms.
* Instance visualization and data access using a hashtable.

## Quick start

1. Clone the repository

   ```
   git clone https://github.com/Mo-Moallem/CourseManager
   ```

2. Open in Visual Studio

3. Change the directory in the `DataReader.cs` class

   ```
   xWorkBook = xApp.Workbooks.Open(@"CHANGE THIS", ReadOnly: true);
   ```

4. Run
