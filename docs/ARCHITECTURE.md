# Architecture

The solution follows Clean Architecture:
- `SharedKernel`: base abstractions and cross-cutting primitives.
- `Domain`: business entities, value objects, events, and repository contracts.
- `Application`: CQRS commands/queries, handlers, DTOs, mappings, validators.
- `Infrastructure`: EF Core persistence, repositories, unit of work, services, seeding.
- `Api`: HTTP layer, middleware, DI composition, Swagger.
