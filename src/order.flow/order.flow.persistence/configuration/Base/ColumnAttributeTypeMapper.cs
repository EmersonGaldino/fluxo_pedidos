using System.Reflection;
using Dapper;

namespace order.flow.persistence.configuration.Base;

public class ColumnAttributeTypeMapper<T> : FallbackTypeMapper
{
    #region Constructor

    public static readonly string ColumnAttributeName = "ColumnAttribute";

    public ColumnAttributeTypeMapper()
        : base(new SqlMapper.ITypeMap[]
        {
            new CustomPropertyTypeMap(typeof(T), SelectProperty),
            new DefaultTypeMap(typeof(T))
        })
    {
    }

    #endregion

    #region Methods

    private static PropertyInfo SelectProperty(Type type, string columnName)
    {
        return
            type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(
                prop =>
                    prop.GetCustomAttributes(false)
                        // Search properties to find the one ColumnAttribute applied with Name property set as columnName to be Mapped 
                        .Any(attr => attr.GetType().Name == ColumnAttributeName
                                     &&
                                     attr.GetType().GetProperties(BindingFlags.Public |
                                                                  BindingFlags.NonPublic |
                                                                  BindingFlags.Instance)
                                         .Any(
                                             f =>
                                                 f.Name == "Name" &&
                                                 f.GetValue(attr).ToString().ToLower() == columnName.ToLower()))
                    && // Also ensure the property is not read-only
                    (prop.DeclaringType == type
                        ? prop.GetSetMethod(true)
                        : prop.DeclaringType.GetProperty(prop.Name,
                            BindingFlags.Public | BindingFlags.NonPublic |
                            BindingFlags.Instance).GetSetMethod(true)) != null
            );
    }

    #endregion
}

public class FallbackTypeMapper : SqlMapper.ITypeMap
{
    private readonly IEnumerable<SqlMapper.ITypeMap> _mappers;

    public FallbackTypeMapper(IEnumerable<SqlMapper.ITypeMap> mappers)
    {
        _mappers = mappers;
    }

    public ConstructorInfo FindConstructor(string[] names, Type[] types)
    {
        foreach (var mapper in _mappers)
        {
            try
            {
                var result = mapper.FindConstructor(names, types);
                if (result != null)
                {
                    return result;
                }
            }
            catch (NotImplementedException)
            {
            }
        }

        return null;
    }

    public ConstructorInfo FindExplicitConstructor()
    {
        foreach (var mapper in _mappers)
        {
            try
            {
                var result = mapper.FindExplicitConstructor();
                if (result != null)
                {
                    return result;
                }
            }
            catch (NotImplementedException)
            {
            }
        }

        return null;
    }

    public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
    {
        foreach (var mapper in _mappers)
        {
            try
            {
                var result = mapper.GetConstructorParameter(constructor, columnName);
                if (result != null)
                {
                    return result;
                }
            }
            catch (NotImplementedException)
            {
            }
        }

        return null;
    }

    public SqlMapper.IMemberMap GetMember(string columnName)
    {
        foreach (var mapper in _mappers)
        {
            try
            {
                var result = mapper.GetMember(columnName);
                if (result != null)
                {
                    return result;
                }
            }
            catch (NotImplementedException)
            {
            }
        }

        return null;
    }
}