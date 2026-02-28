![Coffee_Shop_Chain_CD](https://github.com/user-attachments/assets/3bd30e6c-8224-44f6-a4d8-ebd8f0dbaa8a)


![Coffee_Shop_Chain_ERD](https://github.com/user-attachments/assets/f3ccf664-9498-4002-9670-d15a447fa6ab)


# Coffee Shop Chain – Design Documentation

## Domain Entities

| Entity | Purpose | Key Relationships |
|--------|---------|-------------------|
| **CoffeeShopChain** | The chain; manages multiple shops | Has many CoffeeShops |
| **CoffeeShop** | A single store with its menu, baristas, and orders | Belongs to a chain; has menu items, baristas, and orders |
| **Barista** | Employee who prepares orders | Works at a shop; prepares orders |
| **Customer** | Person who places orders; optionally has a loyalty account | Places orders; may have one LoyaltyAccount |
| **LoyaltyAccount** | Tracks membership tier and points (Regular = 1pt/€, Gold = 2pt/€) | Belongs to exactly one customer |
| **MenuItem** | A product on the menu (beverage type + size + price) | Belongs to a shop; referenced by order items |
| **Extra** | An add-on (extra shot, syrup, whipped cream) with additional cost | Referenced by order item extras |
| **Order** | A purchase with timestamp, total, and status | Placed by a customer, prepared by a barista, at a shop; contains order items |
| **OrderItem** | A line in an order (e.g., 2× Medium Latte) | References a menu item; may have extras |
| **OrderItemExtra** | A selected extra with count on a specific order line | References an extra definition |

## Enums / Constrained Values

| Name | Values | Stored as |
|------|--------|-----------|
| BeverageType | ESPRESSO, LATTE, CAPPUCCINO | Enum (class diagram), varchar (ERD) |
| Size | SMALL, MEDIUM, LARGE | Enum (class diagram), varchar (ERD) |
| MembershipTier | REGULAR, GOLD | Enum (class diagram), varchar (ERD) |

---

## Class Diagram – Specific Details

### Aggregation (independent lifecycle)

| Relationship | Multiplicity | Why aggregation |
|---|---|---|
| Chain → Shop | 1 to 1..* | A shop is a real-world location; it doesn't cease to exist conceptually if the chain object is removed |
| Shop → MenuItem | 1 to 1..* | Menu items define products that could be reused or shared; they aren't structurally part of one shop |
| Shop → Barista | 1 to 1..* | A barista is a person with independent identity; closing a shop doesn't destroy the employee |
| Shop → Order | 1 to 0..* | Orders are historical records; they should survive even if a shop is removed from the system |

### Composition (lifecycle-dependent)

| Relationship | Multiplicity | Why composition |
|---|---|---|
| Customer → LoyaltyAccount | 1 to 0..1 | A loyalty account only exists because a customer enrolled; delete the customer and the account has no meaning |
| Order → OrderItem | 1 to 1..* | A line item is structurally part of its order; it cannot exist on its own |
| OrderItem → OrderItemExtra | 1 to 0..* | An extra selection only makes sense within the context of a specific line item |

### Directed Associations (references)

| Relationship | Multiplicity | Why a reference |
|---|---|---|
| OrderItem → MenuItem | 0..* to 1 | The order item doesn't own or control the menu item; it just records which product was ordered |
| OrderItemExtra → Extra | 0..* to 1 | Same logic; the selection points to an extra definition without owning it |
| Order → Customer (placedBy) | 0..* to 1 | The order records who placed it, but doesn't own the customer |
| Order → Barista (preparedBy) | 0..* to 1 | The order records who prepared it, but doesn't own the barista |

---

## ERD – Specific Details

### Snapshot Columns

| Column | On Table | Why |
|--------|----------|-----|
| `unit_price` | OrderItem | Captures menu item price at time of purchase |
| `unit_cost` | OrderItemExtra | Captures extra cost at time of purchase |

Price changes after an order don't affect historical records.

### Other ERD Choices

- Enum values stored as `varchar` with CHECK constraints rather than separate lookup tables.
- Extra pricing is global (not per-shop). A `ShopExtra` bridge could be added if needed.
- Barista assignment is one shop per barista; no transfer history modeled.
- `LoyaltyAccount.customer_id` has a UNIQUE constraint to enforce max one account per customer.

---

## Design Notes

Methods are included only to represent core operations from the story; we avoided additional service classes to keep the model simple for the assignment.

In a production system, we might normalize menu offerings with a `ShopMenuItem` bridge table to support per-shop pricing/availability and avoid duplication. The assignment keeps a shop-owned menu for simplicity.

<br><br><br>


# SieMarket Electronics Store - Documentation

An online electronics store order management system built with C#.

## Structure

```
SieMarket/
├── model/
│   ├── Order.cs
│   └── OrderItem.cs
├── repository/
│   └── OrderRepository.cs
├── service/
│   └── OrderService.cs
└── Program.cs
```

## Features

- **CalculateFinalPrice** — calculates the total price of an order, applying a 10% discount if it exceeds 500€.
- **GetTopSpendingCustomer** — returns the name of the customer who spent the most across all orders.
- **GetPopularProducts(topN)** — returns the top N most sold products with their total quantity (defaults to 3).

## Run

```bash
dotnet run --project SieMarket
```

## Example Output

```
=== Final Prices ===
Order 1 (Alice): 855.89€
Order 2 (Bob): 254.97€
Order 3 (Alice): 674.97€

=== Top Spending Customer ===
Alice

=== Popular Products ===
USB Cable: 5 sold
Headset: 3 sold
Mouse: 2 sold
Monitor: 2 sold
Laptop: 1 sold
```

> Popular products example uses `topN=5`. Default is 3.
