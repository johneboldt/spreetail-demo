# spreetail-demo

# Overview
This .Net solution comprises a demo of a console application fronting a memory backed multi-value dictionary. The solution's primary purpose is to demonstrate coding techniques I use when implementing an application.

# Approach
* I'm not treating this as a throw away code application. Even though the sole purpose is to demo a simple application, I'm still implementing it using best practice software design principles.

* Even though the specifications state that the application need only support string keys and values, I designed it with extensibility in mind so that other data types can be employed.

* I kept scalability in mind while designing the various repository methods. Even though this implementation renders a simple console application, I decided to make all operations on the backing in-memory dictionary thread safe.

* Throughout the code base, you will find SOLID principles implemented.
  * Single Responsibility - each project, class/interface, and method are cohesive and focused on a single responsibility.
  * Open Closed - Each project contains classes that are open for extension and closed for modifications. Examples include the Spreetail.Demo.ConsoleCommands.IConsoleCommand interface which allows adding additional commands to extend functionality.
  * Liskov Substitution - the Spreetail.Demo.ConsoleCommands.ConsoleCommand abstract class contains common logic that all commands use and delegates specific implementation to each command subclass through abstract methods.
  * Interface Segregation - each interface is focused on a particular intention and includes a set of cohesive operations
  * Dependency Inversion - all dependencies are represented through interfaces and concreate implementations are injected within the console project at startup. This provides a seam for extending the various components at runtime. 

# Structure
The solution is structured into 3 projects.

## Spreetail.Demo
This is a console project that handles user input. There are 10 commands available for the user to maintain and display the current state of the multi-value dictionary. The console accepts the various user commands and orchestrates the processing with the *Spreetail.Demo.Repository* assembly.

## Spreetail.Demo.Repository
This is a class libarary that provides repository services for the consuming console application. It is a stand alone library meaning it doesn't have any knowledge of the consuming application. It could conceivably be hosted behind a REST service using HTTP requests as commands. Although the specifications call for the multi-value repository to handle only string keys and values, this repository is set up to be extensible and is not tied to any particular data type.

## Spreetail.Demo.Test.Unit
This is a test harness utilizing the XUnit testing framework. Since a vast majority of the work is done in the *Spreetail.Demo.Repository*, I focused code coverage in that project. It employs the *FluentAssertions* unit testing assertion framework to provide a more readable testing suite.