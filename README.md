🐇 RabbitMQ Demo with .NET
This repository demonstrates how to integrate RabbitMQ with a .NET application using the .NET 6+ framework. It includes a simple producer-consumer setup showcasing message publishing, queuing, and processing in a decoupled architecture.

🔧 Features
.NET Worker Service for background message processing

RabbitMQ message queue integration using RabbitMQ.Client

JSON-based message serialization

Docker support for running RabbitMQ with the Management UI

Clean and minimal architecture for easy understanding

📦 Components
Publisher: Sends messages to RabbitMQ queue

Consumer: Listens to the queue and processes messages

RabbitMQ Setup: Uses the rabbitmq:3-management Docker image for easy local testing

🚀 Getting Started
Run RabbitMQ using Docker:

bash
Copy
Edit
docker run --rm -it -p 15672:15672 -p 5672:5672 rabbitmq:3-management
Start the .NET producer and consumer applications.

Access the RabbitMQ UI at http://localhost:15672 (default login: guest / guest).

📁 Structure
bash
Copy
Edit
RabbitMQDemo/
├── Publisher/         # Sends messages to queue
├── Consumer/          # Receives and processes messages
├── Shared/            # Shared message contracts and models
└── README.md
📚 Requirements
.NET 6 or later

Docker (for RabbitMQ container)
