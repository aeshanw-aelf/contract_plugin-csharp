using Google.Protobuf.Reflection;

namespace ContractPluginCsharp;

public class ContractEventClassGenerator: IGenerator<MessageDescriptor,uint>
{
    //TODO Implement following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L546
    public string Generate(MessageDescriptor descriptor, uint flags)
    {
        // descriptor.Fields[0].ContainingType.Name;
        throw new NotImplementedException();
    }
}
