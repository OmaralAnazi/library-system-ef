using System.Linq.Expressions;
using System.Text;
using LibrarySystem.Data.Entities;
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

    public static ModelBuilder ApplyGlobalSoftDeleteFilter(this ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes()
            .Where(t => typeof(BaseEntity).IsAssignableFrom(t.ClrType));
        
        foreach (var et in entityTypes)
        {
            var p = Expression.Parameter(et.ClrType, "e");
            var body = Expression.Equal(
                Expression.Property(p, nameof(BaseEntity.DeletedAt)),
                Expression.Constant(null, typeof(DateTime?)));
            var lambda = Expression.Lambda(body, p);
            et.SetQueryFilter(lambda);
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