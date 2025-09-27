using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Migrations
{
    /// <inheritdoc />
    public partial class SeedingBooksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO books (isbn, title, author, published_at, created_at, updated_at, key)
                VALUES 
                ('978-0-123456-47-2', 'Clean Code', 'Robert C. Martin', '2008-08-11', NOW(), NOW(), gen_random_uuid()::text),
                ('978-1-491947-65-2', 'Domain-Driven Design', 'Eric Evans', '2003-08-30', NOW(), NOW(), gen_random_uuid()::text),
                ('978-0-596520-68-7', 'Refactoring', 'Martin Fowler', '1999-07-08', NOW(), NOW(), gen_random_uuid()::text);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM books
                WHERE isbn IN (
                    '978-0-123456-47-2',
                    '978-1-491947-65-2',
                    '978-0-596520-68-7'
                );
            ");
        }
    }
}
