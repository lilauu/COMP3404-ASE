# Design Portfolio - Technical Design

## Design Considerations
When we started the technical design, we wanted to bear a few considerations in mind to keep our code adequately compartmentalised, and adhering to design strategies such as loose coupling, high cohesion, encapsulation and separation of concerns.\
Some key considerations we came up with were:
- All API and authentication related process should be handled server-side to keep them separate from the user interface and obscure more technical processes from the user.
- We should create reusable components for the UI to avoid duplication and support cohesive, clean and testable code.
- We should pass data explicitly instead of using global variables to avoid unnecessary coupling between unrelated parts of the system.
- We need to keep the AI integration loosely coupled so that we can easily substitute the stub for the real API later down the line.

## Use Case Descriptors
Given these considerations, we started to create our use case descriptors in order to start mapping out how components would connect and things we needed for our application and it's infrastructure. This table helped us visualise what we were aiming to achieve much easier with a focus on the processes the system would need to work through as well as being able to identify actors and edge cases if something was to go wrong.

![use-case-descriptor-table](diagrams/use-case-descriptors.png)

With the biggest process being identified as the authentification process for logging in, we decided to create a use case diagram to visualise it a little better.

![use-case-auth](diagrams/use-case.drawio.png)

## Sequence Diagrams
Given the use-case descriptors we came up with, we decided to create sequence diagrams of the authentication/login process, sending prompts to the AI API and getting responses as well as the process for deleting chats, 3 key processes we needed to get right in order to successfully create our MVP.

This first one is the auth process we created a use-case diagram for as shown above. The difference between these diagrams being that the use-case shows what the system does functionally, and this sequence diagram showing the behaviour over time and how data is passed around.

![login-sequence-diagram](diagrams/login-sequence.png)

These two sequence diagrams show different ways the user can interact with chats, with the top one being the user sending a prompt into the chatbot, along with how the API is called and returns a response. The second diagram shows the process that the user does not directly see when they delete an online saved chat, looking at the connection with the database and verifying that the right thing has been deleted before returning a success message to the user.

![user-chat-interaction-sequence-diagrams](diagrams/user-chat-interaction-sequence.png)

## UML Diagram

The UML diagram we've produced shows the hierarchy of the services we've created. 

![uml-diagram](diagrams/UML-diagram.png) 

## SOLID Principles
So before we get into SOLID principles, there may be a question of 'where are the CRC cards in the previous section?' however, we decided as a group not to produce any CRC cards because they assume multiple responsibilities and tasks for every class, and this goes directly against the Single Responsibility Principle. The SRP states that each module should only have one reason to change, or be responsible for one thing only, to promote modularity, making code more maintable and testable, as well as much easier to read. Instead of this, for class discovery to help us map responsibilities to classes/components we took an approach of going through the brief and picking out key nouns, such as Chat, Account and AI and created classes such as ChatController, AccountController, which are used in the API and an interface named IAIModelService with two classes named StubAIModel and GeminiAIModel. We also defined classes and view models for each page and component to keep everything separated, such as SettingsPage, SettingsPageViewModel and MessageView and MessageViewModel.

In addition to SRP, we've also adhered to the SOLID principle of Interface Segregation by keeping our interfaces small and only implementing classes to be dependent on the interfaces they need, with most of our interfaces being API related and very specific in their uses., such as the IAIModelService only being used by StubAI model until it's replaced by GeminiAIModel. When we decided to implement Service Containers as a design pattern we also bumped up our adherance to SOLID principles as service containers promote dependency injection, which directly correlates with Dependency Injection Principle, separating classes from instantiating dependencies and instead letting them get the dependencies from an external source.

## How our design met the client brief (Reflection)

During this project, our primary focus was to deliver a minimum viable product (MVP) that fulfilled the requirements outlined in the NovaChat brief. I'm proud to say that we successfully met all of our functional requirements and even exceeded our initial expectations by implementing additional non-functional features beyond what was originally planned in our MVP specifications.

For example, we personalised the chatbot experience by using the user's name based on their login credentials, and introduced features such as text-to-speech functionality and a stylised dark/light mode with appropriate contrast to enhance usability. We also prioritised accessibility by ensuring features like translation support and proper tab indexing, which helped make our product more inclusive and user-friendly.

Beyond the core requirements, we added valuable functionality including user login integration via GitHub OAuth, and the full integration of the AI model — replacing the stub model we initially used to keep components loosely coupled during development. We also provided users with flexibility in how they manage their data by allowing them to store chat histories both locally and online through a dedicated chat history tab.

Our integration of the Gemini API enabled features like effective text summarisation and continuous conversational capabilities across multiple simultaneous chat windows, also meeting key requirements for our MVP. Finally, while cross-platform compatibility was not initially in scope, we chose to develop a web-based UI using MAUI to future-proof the application and offer a more seamless and user-friendly experience compared to a forms-based UI that was in our basic functional requirements.

Overall, the effort of the team not only met the brief but delivered a product that is robust, accessible, and ready for future growth.
