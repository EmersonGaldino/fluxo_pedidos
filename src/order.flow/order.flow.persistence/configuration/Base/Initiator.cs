using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Dapper;
using order.flow.domain.model.Base.user;

namespace order.flow.persistence.configuration.Base;

public class Initiator
{
    private static bool _register = false;

    #region Methods
    public static void RegisterModels()
    {
        if (_register) return;

        var tipoPadrao = typeof(UserModel);
        var entitidades = tipoPadrao
            .Assembly
            .GetTypes()?
            .Where(type => type.Namespace != null &&
                           (type.GetCustomAttribute<TableAttribute>() != null ||
                            type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Any(x => Attribute.IsDefined(x, typeof(ColumnAttribute)))
                           ) &&
                           !type.IsGenericType && type.IsClass).ToList();
        entitidades?.ForEach(type =>
        {
            dynamic typeMap =
                Activator.CreateInstance(typeof(ColumnAttributeTypeMapper<>).MakeGenericType(type));
            SqlMapper.SetTypeMap(type, typeMap);
        });
        _register = true;

        typeof(Initiator).Assembly.GetTypes()?.Where(x =>
            x.IsAssignableToGenericType(typeof(SqlMapper.TypeHandler<>)) && !x.IsAbstract
        )?.ToList().ForEach(x =>
        {
            var handler = Activator.CreateInstance(x);
            SqlMapper.AddTypeHandler(x.BaseType.GenericTypeArguments[0], (SqlMapper.ITypeHandler)handler);
        });
    }
    #endregion

}