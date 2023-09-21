using System.Text;
using Google.Protobuf.Reflection;

namespace ContractPluginCsharp;

internal class ContractBaseClassGenerator: IGenerator<ServiceDescriptor>
{
    //TODO Implement based on https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L422
    public string Generate(ServiceDescriptor service)
    {
        throw new NotImplementedException();
    }
}
