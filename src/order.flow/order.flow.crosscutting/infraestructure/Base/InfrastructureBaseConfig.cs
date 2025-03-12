using order.flow.crosscutting.infraestructure.application;
using order.flow.crosscutting.infraestructure.dataBase;

namespace order.flow.crosscutting.infraestructure.Base;

public class InfrastructureBaseConfig
{
    public DataBaseConfig DataBase { get; set; }
    public ApplicationConfig ApplicationConfig { get; set; }
}