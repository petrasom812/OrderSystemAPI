# Order System API

Backend API built with ASP.NET Core

## Features
- CRUD Orders
- DTO-based architecture
- Async EF Core
- Business rule enforcement
- OrderNumber generator

## Tech Stack
- ASP.NET Core
- EF Core + SQLite

Order
 └── OrderItem
       └── Product
             └── Inventory

Orders.Id
    ↓
OrderItems.OrderId

Products.Id
    ↓
OrderItems.ProductId

Products.Id
    ↓
Inventories.ProductId