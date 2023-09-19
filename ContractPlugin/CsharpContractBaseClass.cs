using System.Text;
using Google.Protobuf.Reflection;

namespace contract_plugin_csharp;

internal class CSharpContractBaseClass
{
    public string Generate(ServiceDescriptor service)
    {
        StringBuilder output = new StringBuilder();
        //TODO Implement
        return output.ToString();
    }
}
