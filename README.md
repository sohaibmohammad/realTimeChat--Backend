# 💬 Real-Time Chat Backend API

A scalable, production-ready real-time chat backend built with **ASP.NET Core 8**, **SignalR**, and **Clean Architecture**. The system supports instant messaging, secure authentication, online presence tracking, conversation management, and scalable real-time communication.

---

# ✨ Features

## 🔐 Authentication
- JWT Authentication
- Role-Based Authorization
- Secure Password Hashing
- Refresh Token Ready

## 💬 Messaging
- One-to-One Real-Time Messaging
- Conversation Management
- Message History
- Pagination
- Read Status
- Message Delivery Status
- Typing Indicator Ready

## 🟢 Presence
- Online Users
- Offline Detection
- Connection Tracking
- Multiple Device Support

## ⚡ Real-Time Communication
- SignalR Hub
- Instant Message Delivery
- Automatic Reconnection
- Connection Lifecycle Handling

## 🗄 Database
- Entity Framework Core
- SQL Server
- Code First
- Migrations

## 🏗 Architecture
- Clean Architecture
- Repository Pattern
- Unit of Work
- Dependency Injection
- CQRS Ready
- SOLID Principles

---

# 🛠 Tech Stack

### Backend

- ASP.NET Core 8
- SignalR
- Entity Framework Core
- SQL Server
- JWT Authentication
- AutoMapper
- FluentValidation
- MediatR
- LINQ

### Tools

- Visual Studio 2022
- Postman
- Git
- GitHub

---

# 📂 Project Structure

```
src
│
├── Chat.API
├── Chat.Application
├── Chat.Domain
└── Chat.Infrastructure
```

---

# 📌 System Architecture

```
                 Client A
                    │
                    │
            SignalR Connection
                    │
             ASP.NET Core API
                    │
     ┌──────────────┴──────────────┐
     │                             │
Business Logic               SignalR Hub
     │                             │
     └──────────────┬──────────────┘
                    │
             Entity Framework
                    │
              SQL Server
```

---

# 🚀 Main Functionalities

- User Authentication
- Real-Time Messaging
- Conversation Creation
- Message Persistence
- Online User Tracking
- Pagination
- Global Exception Handling
- Validation
- Dependency Injection

---

# 📦 Installation

Clone repository

```bash
git clone https://github.com/yourusername/realtime-chat-backend.git
```

Restore packages

```bash
dotnet restore
```

Apply migrations

```bash
dotnet ef database update
```

Run project

```bash
dotnet run
```

---

# 🔑 Authentication

The API uses JWT Bearer Authentication.

Example

```http
Authorization: Bearer YOUR_ACCESS_TOKEN
```

---

# 📡 SignalR Hub

```
/chatHub
```

Example Events

### Client → Server

- SendMessage
- JoinConversation
- LeaveConversation

### Server → Client

- ReceiveMessage
- UserConnected
- UserDisconnected
- MessageDelivered
- MessageRead

---

# 🗄 Database Entities

- Users
- Conversations
- Participants
- Messages
- Connections

---

# 🔄 Message Flow

```
User A
   │
   ▼
HTTP API
   │
   ▼
Business Layer
   │
   ▼
Save Message
   │
   ▼
Database
   │
   ▼
SignalR Hub
   │
   ▼
User B
```

---

# 📖 API Documentation

Swagger is enabled during development.

```
https://localhost:5001/swagger
```

---

# 🚧 Future Improvements

- Redis Cache
- Redis Backplane
- RabbitMQ
- Outbox Pattern
- Background Services
- Push Notifications
- Group Chat
- Docker
- Unit Testing
- Integration Testing
 
---

# 🎯 Design Principles

- SOLID
- Clean Architecture
- Separation of Concerns
- Dependency Injection
- Repository Pattern
- Unit of Work
- RESTful API Design

---

# 👨‍💻 Author

**Sohaib Mohammad**

📧 sohaib.swe@gmail.com

GitHub

https://github.com/sohaibmohammad

---

# ⭐ Support

If you found this project useful, consider giving it a ⭐ on GitHub.
