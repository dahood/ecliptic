# Ecliptic - **E**x**C**e**L** to gherk**I**n **P**rogramma**TIC** Utility

Everyone loves Gherkin for a specification language, but it's much easier to use Excel for tabular data when writing complex specs.

Large scale project teams (chiefly the business analysts, quality analysts, and developers) benefit from having better tooling around building and maintaining Gherkin specifications and Ecliptic is such a tool.

Ecliptic takes you from Excel files (MyStory.feature.xlsx) into Gherkin (MyStory.feature). From there it is easier for developers to maintain and instrument into the application or system.


## Usage

Generate and Run Test(s)

```
#!bash

ecliptic MyStory.feature.xlsx
```

Generate Only

```
#!bash

ecliptic generate MyStory.feature.xlsx
```

Test Run Only

```
#!bash

ecliptic run MyStory.feature.xlsx
```


Ecliptic currently has PowerShell command line utilities to generate Gherkin and modify your project files. Bash versions of these will be available soon (please contribute if you have time).

## Ecliptic.properties File

This file allows you to define conventions for your project structure.

```
#!bash

SpreadSheetFolder = ./Stories
GherkinFolder = ./Features
ProjectFile = ./CSharpProject.csproj #Visual Studio
#Eclipse ./ClojureProject.project 

```

### What are the next planned features - and how can I contribute?

Here is our [todo list](todo.md) - this link is broken - fix when moving to GitHub pages

## What does Ecliptic mean?

http://en.wikipedia.org/wiki/Ecliptic


### License

Ecliptic is licensed uner Apache 2.0 (see LICENSE.txt). It includes a distribution of Specflow components which are licensed under BSD.