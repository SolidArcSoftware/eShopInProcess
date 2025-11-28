# eShop In-Process (Modular Monolith Structure)

This repository is an architectural fork of **eShopOnContainers**.  
The goal is to redesign the solution from a microservices architecture into a **modular monolith** that follows:

- Full Clean Architecture
- Domain-Driven Design (DDD)
- Modular Monolith principles
- Multi-database / single database (5th normal form) persistence options

The solution is composed using the **DataArc Orchestration Framework**, which replaces the need for:

- Event Busses
- Domain Services as orchestration coordinators
- Handler chains (Mediator pipelines, CQRS handler request / response sprawl)
- Repository-driven transaction boundaries
- Outbox pattern implementations for consistency
- Cross-service messaging to synchronize bounded contexts
- Boilerplate infrastructure plumbing scattered across the application layer

DataArc provides a single, explicit orchestration layer that coordinates multi-context operations with immediate consistency, deterministic inputs and outputs, and full Clean Architecture separation.

---

## 1. The Database Definition Builder

End-to-end database definition and initialization using orchestrators:

- Create and seed multi-context persistence layers as a normalized single database
- Create and seed multi-context persistence layers as separate databases
- Integrate database definition with ASP.NET Identity
- Orchestrate database definition building as a contained integration unit
- Plug database definition building into integration tests
- Achieve all of the above **without** migration files or boilerplate migration code

## Disclaimer

This repository demonstrates architectural structure only.
The orchestration runtime is required to run this solution.
Integration tests will not run without the runtime packages.
