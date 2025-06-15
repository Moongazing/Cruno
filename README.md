Cruno

Cruno is a modular monolith task scheduling and job orchestration system built with .NET. It is designed to distribute, execute, retry, and monitor background jobs across distributed workers. Cruno is ideal for complex systems requiring reliable and traceable background task execution in a distributed environment.

📦 Project Structure

/src
├── Cruno.API                 → Exposes all modules as HTTP APIs
├── Cruno.WorkerService       → Background job processor (RabbitMQ consumer)
├── Cruno.Infrastructure      → Shared infrastructure (RabbitMQ, Redis, EF Core, Logging)
├── Cruno.BuildingBlocks      → Shared kernel (Entity, ValueObjects, Events)
├── Cruno.Application.Shared  → Shared abstractions (MediatR, Result<T>, Interfaces)
└── Modules/
     ├── Cruno.Modules.Jobs         → Job definitions, logs, creation APIs
     ├── Cruno.Modules.Scheduler    → Cron parsing, trigger evaluation
     ├── Cruno.Modules.Worker       → Worker registration and monitoring


✨ Features

Define and manage background jobs with retry strategies

Distribute jobs to multiple workers via RabbitMQ

Centralized execution logs and status tracking

Extensible retry policies (None, Linear, Exponential)

Redis-based caching for health checks and deduplication

Serilog-based structured logging

Modular DDD architecture

📡 Technologies

.NET 8

RabbitMQ (Event Bus)

Redis (Cache / Distributed coordination)

Entity Framework Core (Persistence)

Serilog (Logging)

MediatR (CQRS pattern)

Modular Monolith + DDD

🚀 Getting Started

Prerequisites:

.NET 8 SDK

RabbitMQ

Redis

Running the System:

# Restore and build
> dotnet restore
> dotnet build

# Run API (Exposes endpoints)
> dotnet run --project src/Cruno.API

# Run Worker (Consumes and executes jobs)
> dotnet run --project src/Cruno.WorkerService

📜 Message Contract: JobExecutionMessage

{
  "jobId": "00000000-0000-0000-0000-000000000000",
  "payload": "{...}",
  "retryCount": 0,
  "retryStrategy": "None"
}

🧱 Modules Overview

🔹 Jobs Module

JobDefinition entity (Id, Name, Cron, Payload, RetryStrategy)

Create/Update/Delete job commands

JobExecutionLog entity

API endpoints for CRUD

🔹 Scheduler Module

Cron parsing

Next execution time calculation

Job scheduling loop

🔹 Worker Module

Registers worker nodes

Tracks heartbeat

Assigns jobs based on load

🧪 Testing / Extending

Add unit/integration tests per module

Plug in external execution strategies

Build Blazor dashboard (Planned)
