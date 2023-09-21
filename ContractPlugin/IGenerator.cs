namespace ContractPluginCsharp;

public interface IGenerator<in T,V>
{
    public string Generate(T descriptor,V flags);
}

public interface IGenerator<in T>
{
    public string Generate(T descriptor);
}
