# Design Portfolio - Architecture

## Design Patterns

#### MVVM

We decided to use the Model-View-ViewModel or MVVM design pattern because it helps us to clearly separate the business logic from the user interface. This allows us to keep code testable, maintainable and reusable. MVVM works well with .NET MAUI as it breaks up what would be tight coupling with the XAML UI and the code behind that still operates within the UI. MVVM has the three core components, shown in the diagram below, but has limited access to each other, allowing the model to evolve independently of the view. This allows the app UI to be redesigned without touching the view model or model code, as our view has been implemented in XAML/C#. This also allowed us to work concurrently. 

This is an example of the MVVM file structuring we have used in our code to keep adhering to this design pattern.

![mvvm-file-structure](diagrams/mvvm-file-structure.png)

#### Service Container Pattern

Later on in the project, we realised we had created a lot of singleton patterns in our code, and so decided to turn them into services and combine MVVM with the Service Container pattern, using C#'s automatic dependency injection to make everything more sleek and adhere better to SOLID principles, as it directly aligns with the Dependency Inversion Principle or DIP, inverting control of created dependencies, allowing a class to access dependencies externally, rather than the classes instantiating directly.

## Tech Stack - .NET MAUI v. Blazor/Razor
Both .NET MAUI nd Blazor are technologies for application development, but cater to different platforms. We chose to work with .NET MAUI primarily as it focuses on cross-platform applications, being able to work from mobile to desktop apps. MAUI also pairs well with XAML and C#, utilising native platform control.

In terms of actually creating the application, although settling on .NET MAUI we considered blazor as well, but eventually settled on MAUI as it doesn't require JS interop unless you're embedding web content, and gives full native API access. .NET MAUI also works well with XAML and other .NET projects, and allows you to only need a single build in C#/XAML so made the most sense for us to keep our project as modernised as possible.

## ASP.NET Core - Web Development
We used ASP.NET Core alongside MAUI for our server because it provides a built-in database and comes with everything we would need to build a web-based application. On top of this, it also comes with Blazor support, which we eventually decided to ditch in favour of MAUI but it was a key component of our decision making process to use ASP.NET.
