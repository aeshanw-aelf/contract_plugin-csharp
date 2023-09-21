using System.Text;
using Google.Protobuf;
using Google.Protobuf.Compiler;
using Google.Protobuf.Reflection;

namespace ContractPluginCsharp;

// assume current directory is the output directory, and it contains protoc cli.
// protoc --plugin=protoc-gen-contract_plugin_csharp --contract_plugin_csharp_out=./ --proto_path=%userprofile%\.nuget\packages\google.protobuf.tools\3.21.1\tools --proto_path=./ chat.proto

internal class Program
{
private static void Main(string[] args)
    {
        // you can attach debugger
        // System.Diagnostics.Debugger.Launch();

        Stream stream = Console.OpenStandardInput();

        ContractGenerator contractGenerator = new ContractGenerator();
        CodeGeneratorResponse response = contractGenerator.Generate(stream);

        // set result to standard output
        using (var stdout = Console.OpenStandardOutput())
        {
            response.WriteTo(stdout);
        }
    }
}
