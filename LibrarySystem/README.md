# 📚 Library System

A simple **EF Core practice project** for learning repositories, unit of work, validation, and migrations — built around a basic library domain.

---

## ✅ Features & Progress

* Fresh project with local DB setup
* Global naming in `snake_case` via `OnModelCreating`
* Custom **Repository & Unit of Work** pattern
* Base CRUD controllers & services (for faster dev)
* **AutoMapper** integration for DTOs
* Entities: `Book`, `Member`, `Loan`
* Global exception handling (middleware)
* **FluentValidation** (e.g. ISBN unique check)
* Migrations with `migrationBuilder.Sql()` seeding
* Auto apply migrations on build *(not for prod)*
* Practice queries in LINQPad or Rider, inspect SQL

---

## 🧩 Entities

| Entity     | Fields                                 |
| ---------- | -------------------------------------- |
| **Book**   | Title, Author, ISBN, PublishedDate     |
| **Member** | Name, Email                            |
| **Loan**   | BookId, MemberId, LoanDate, ReturnDate |

---

## 🧠 What You’ll Practice

* **EF Core** basics & naming conventions
* **Repository + UoW** patterns
* **FluentValidation** (business rules like loan limits)
* **AutoMapper** for DTO conversions
* **Migrations & SQL() seeding**
* **Viewing generated SQLs** using `AsNoTracking`

---

## 💡 Lessons Learned

* Implementing repo/UoW patterns manually
* Using `CancellationToken` and `Dispose` correctly
* Creating base CRUD services (and why not to overuse)
* Using **AutoMapper** and **FluentValidation** together
* Writing raw SQL in migrations for data seeding
* Auto-migration on startup *(useful for dev only)*
* Viewing EF-generated SQLs for better understanding

---

## ⚙️ Run Locally

```bash
dotnet ef database update
dotnet run
```

---

## 📦 Notes

This is a **learning project** — not meant for production use.
Auto migrations, global exception handling, and direct seeding are all simplified for experimentation.
