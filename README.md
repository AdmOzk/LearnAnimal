# Use Claude Sonnet with .NET: Learn Animal

![image](https://github.com/user-attachments/assets/eb6a1ccf-0d2a-47e1-a82d-824ed8706c3d)


### About the Repository
Welcome to the repository for "Use Claude Sonnet with .NET: A Comprehensive Guide". This project demonstrates how to integrate the powerful AI model Claude Sonnet, 
available via Amazon Bedrock, into a .NET Core application. The repository contains the complete source code referenced in my Medium post and LinkedIn articles.
The goal of this project is to showcase how developers can leverage Claude Sonnet to build AI-driven applications 
that can handle various input types like text, images, and PDF documents. This guide and codebase provide a framework to adapt to future updates.

### Features
Text Input Handling: Implemented using GetAnimalInfoAsync, which sends text queries to Claude Sonnet and retrieves detailed responses.

Image Input Handling: Using the AnimalImage method, the application can process PNG images by converting them to Base64 format before sending them to the API.

PDF Input Handling: Although not supported by Claude Sonnet 3.0, the AnalyzePdfAsync method demonstrates how to structure PDF requests for future model versions.


## Setup Instructions 🛠️

### Clone the repository:
git clone https://github.com/AdmOzk/LearnAnimal.git

### Configure your appsettings.json file with your AWS credentials.

### Run the application in your preferred .NET Core environment.

## Project Structure

### Controllers: Contains the HomeController, which manages API calls.

### Services: Includes the ClaudeService class that handles interactions with Claude Sonnet.

### Views: Contains the front-end views to interact with the application.

# Medium Post
## Check out the detailed guide on Medium to follow a step-by-step walkthrough of the project setup, AWS configuration, and code implementation:
https://medium.com/@adem.ozkyt/use-claude-sonnet-with-net-a-comprehensive-guide-70e6f2b3fcc1

# Linkedin Post
## Check out the demo video from Linkedin post
https://www.linkedin.com/posts/adem-%C3%B6zkay%C4%B1t-945003238_ai-yapayzeka-claudesonnet-activity-7284970269968846849-6aSE?utm_source=share&utm_medium=member_desktop


# Project Screen Shuts (From Turkish Language Option, Program in Repository updated to English language.)

## Landing Page
![Ekran görüntüsü 2025-01-14 182224](https://github.com/user-attachments/assets/5852537f-3034-448c-beb0-616546d97ad7)

## Text Input & Result
![image](https://github.com/user-attachments/assets/e2f8a8e1-d7bc-47f6-8623-d23e6046be96)

![image](https://github.com/user-attachments/assets/21101229-dcc9-40ae-87ed-f727059dd4c9)

## Image Input & Result
![cuteLion](https://github.com/user-attachments/assets/66ece1de-9db9-4e95-a3be-9e4314179be3)

![analizSonucu](https://github.com/user-attachments/assets/d4cc521e-a088-4408-8810-10618fd6283b)




