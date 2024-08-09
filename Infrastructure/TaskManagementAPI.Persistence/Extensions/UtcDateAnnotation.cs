using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace TaskManagementAPI.Persistence.Extensions;
public static class UtcDateAnnotation
{
    private const string _isUtcAnnotation = "IsUtc";
    private static readonly ValueConverter<DateTime, DateTime> _utcConverter = new ValueConverter<DateTime, DateTime>(convertTo => DateTime.SpecifyKind(convertTo, DateTimeKind.Utc), convertFrom => convertFrom);

    public static PropertyBuilder<TProperty> IsUtc<TProperty>(this PropertyBuilder<TProperty> builder, bool isUtc = true) => builder.HasAnnotation(_isUtcAnnotation, isUtc);

    public static bool IsUtc(this IMutableProperty property)
    {
        if (property != null && property.PropertyInfo != null)
        {
            var attribute = property.PropertyInfo.GetCustomAttribute<IsUtcAttribute>();
            if (attribute is not null && attribute.IsUtc)
            {
                return true;
            }

            return ((bool?)property.FindAnnotation(_isUtcAnnotation)?.Value) ?? true;
        }
        return true;
    }

    /// <summary>
    /// Make sure this is called after configuring all your entities.
    /// </summary>
    public static void ApplyUtcDateTimeConverter(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (!property.IsUtc())
                {
                    continue;
                }

                if (property.ClrType == typeof(DateTime) ||
                    property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(_utcConverter);
                }
            }
        }
    }
}
public class IsUtcAttribute : Attribute
{
    public IsUtcAttribute(bool isUtc = true) => this.IsUtc = isUtc;
    public bool IsUtc { get; }
}
