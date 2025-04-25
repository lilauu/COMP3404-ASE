# Design Portfolio - Architecture

## MVVM
We decided to use the Model-View-ViewModel or MVVM design pattern because it helps us to clearly separate the business logic from the user interface. This allows us to keep code testable, maintainable and reusable. MVVM works well with .NET MAUI as it breaks up what would be tight coupling with the XAML UI and the code behind that still operates within the UI. MVVM has the three core components, shown in the diagram below, but has limited access to each other, allowing the model to evolve independently of the view. This allows the app UI to be redesigned without touching the view model or model code, as our view has been implemented in XAML/C#. This also allowed us to work concurrently.

## Tech Stack - .NET MAUI v. Blazor/Razor
