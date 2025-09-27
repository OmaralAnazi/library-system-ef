using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Migrations
{
    /// <inheritdoc />
    public partial class SeedingMembersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO members (name, email, created_at, updated_at, key)
                VALUES
                ('Alice Johnson', 'alice@example.com', NOW(), NOW(), gen_random_uuid()::text),
                ('Bob Smith',     'bob@example.com',   NOW(), NOW(), gen_random_uuid()::text),
                ('Charlie Lee',   'charlie@example.com', NOW(), NOW(), gen_random_uuid()::text);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM members
                WHERE email IN (
                    'alice@example.com',
                    'bob@example.com',
                    'charlie@example.com'
                );
            ");
        }
    }
}
