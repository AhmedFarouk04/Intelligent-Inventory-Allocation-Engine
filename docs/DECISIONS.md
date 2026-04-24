# Decisions

1. Use SQL Server as primary provider.
2. Keep repository + unit of work for clear transaction boundaries.
3. Use CQRS via MediatR for API use-cases.
4. Emit domain events for stock movement and low-stock notifications.
