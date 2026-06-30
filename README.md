# Order Management System API

A backend Order Management System built with ASP.NET Core, Entity Framework Core, and SQLite.

## Features

### Product Management
- Create Product
- Get All Products
- Get Product By Id
- Update Product
- Soft Delete Product
- SKU uniqueness validation

### Inventory Management
- Create Inventory
- Get All Inventory
- Get Inventory By Id
- Update Inventory
- Delete Inventory
- Prevent duplicate inventory per product
- Validate Product existence

### Order Management
- Create Order
- Get Orders
- Get Order By Id
- Update Order Status
- Delete Order

### Order Items
- Multiple products per order
- Product validation before order creation
- SKU automatically sourced from Product table

---

## Architecture

```text
Order
 └── OrderItem
       └── Product
             └── Inventory
```

### Relationships

```text
Orders.Id
    ↓
OrderItems.OrderId

Products.Id
    ↓
OrderItems.ProductId

Products.Id
    ↓
Inventories.ProductId
```

---

## Database Schema

### Products

```text
Id
Sku
Name
IsActive
CreatedAt
EditedAt
```

### Inventories

```text
Id
ProductId
QuantityOnHand
ReorderLevel
CreatedAt
EditedAt
```

### Orders

```text
Id
OrderNumber
Status
TotalAmount
CreatedAt
EditedAt
```

### OrderItems

```text
Id
OrderId
ProductId
Sku
Quantity
UnitPrice
```

---

## Business Rules

### Products

- SKU must be unique
- Product names can be duplicated
- Products cannot be created with empty values

### Inventory

- Inventory requires a valid Product
- One inventory record per Product
- Quantity cannot be negative

### Orders

- Product must exist before creating an OrderItem
- SKU is automatically retrieved from Product
- TotalAmount is automatically calculated

---

## Example Order Request

```json
{
  "status": "Pending",
  "items": [
    {
      "productId": 1,
      "quantity": 2,
      "unitPrice": 300
    },
    {
      "productId": 6,
      "quantity": 3,
      "unitPrice": 50
    }
  ]
}
```

---

## Technologies

- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger/OpenAPI
- C#

---

## Future Enhancements

- Inventory-aware order creation
- Automatic inventory deduction
- Global error handling middleware
- AutoMapper
- JWT Authentication
- Order History
- Inventory History
- Background archival jobs
- Pagination and filtering

---

## Learning Objectives

This project was built to practice:

- REST API design
- Service layer architecture
- DTO patterns
- Entity Framework Core
- Database relationships
- Backend business rules
- Validation strategies
- Inventory management concepts