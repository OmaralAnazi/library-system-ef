using System.Text;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ApplySnakeCaseConfiguration(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Convert table name
            entity.SetTableName(ToSnakeCase(entity.GetTableName()));

            // Optional: convert column names
            foreach (var property in entity.GetProperties())
                property.SetColumnName(ToSnakeCase(property.GetColumnName()));

            // Optional: convert key names
            foreach (var key in entity.GetKeys())
                key.SetName(ToSnakeCase(key.GetName()));

            foreach (var fk in entity.GetForeignKeys())
                fk.SetConstraintName(ToSnakeCase(fk.GetConstraintName()));

            foreach (var index in entity.GetIndexes())
                index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()));
        }

        return modelBuilder;
    }
    
    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        var sb = new StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            var c = input[i];
            if (char.IsUpper(c))
            {
                if (i > 0) sb.Append('_');
                sb.Append(char.ToLower(c));
            }
            else
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
}