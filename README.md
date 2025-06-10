# todo-microservices
# 🧩 Todo Microservices (Mini Project)

This is a **very small microservices-based Todo application** built with .NET.  
The goal of the project is to demonstrate how to structure microservices with **Clean Architecture**, **Event-Driven Communication**, and **modern tooling** such as Docker and RabbitMQ.

> 🔹 This project is not production-level, but is focused on architectural clarity and learning purposes.

---

## 📦 Tech Stack

### General
- 🐳 Docker (multi-container with `docker-compose`)
- 📬 RabbitMQ (for event-driven messaging between services)
- 📘 Swagger (for API documentation)

### UserService
- 🧼 Clean Architecture + DDD Principles
- 🗄️ SQL Server + Entity Framework Core

### TaskService
- 🧼 Clean Architecture + DDD Principles
- 🍃 MongoDB

---

## 📡 Communication

- The services communicate **asynchronously** using **RabbitMQ**.
- When a new task is created in `TaskService`, an event (`TaskCreated`) is published.
- `UserService` listens for this event and processes it via a background consumer.

---

## 🧪 Running the Project

```bash
docker-compose up --build
