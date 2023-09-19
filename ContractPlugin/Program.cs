using System.Text;
using Google.Protobuf;
using Google.Protobuf.Compiler;
using Google.Protobuf.Reflection;

namespace contract_plugin_csharp;

// assume current directory is the output directory, and it contains protoc cli.
// protoc --plugin=protoc-gen-contract_plugin_csharp --contract_plugin_csharp_out=./ --proto_path=%userprofile%\.nuget\packages\google.protobuf.tools\3.21.1\tools --proto_path=./ chat.proto

internal enum Flags : uint
{
    GENERATE_CONTRACT = 0,
    GENERATE_STUB = 0x2, // hex for 0000 0010
    GENERATE_REFERENCE = 0x4, // hex for 0000 0100
    GENERATE_EVENT = 0x8, // hex for 0000 1000
    INTERNAL_ACCESS = 0x80 // hex for 1000 0000
}

internal class Program
{
    private static string GetFileNamespace(FileDescriptorProto fileDescriptor)
    {
        //TODO Implementation
        return "";
    }

    private static string GetServicesFilename(FileDescriptor fileDescriptor)
    {
        //TODO Implementation
        return "";
    }

private static void Main(string[] args)
    {
        // you can attach debugger
        // System.Diagnostics.Debugger.Launch();

        // get request from standard input
        CodeGeneratorRequest request;
        using (var stdin = Console.OpenStandardInput())
        {
            request = Deserialize<CodeGeneratorRequest>(stdin);
        }

        var response = new CodeGeneratorResponse();
        StringBuilder output = new StringBuilder();

        //TODO Implement logic as per
        var cSharpEventClass = new CSharpEventClass();
        var idx = 0;
        var flag = (uint)Flags.GENERATE_STUB; //TODO need to make this dynamic like in the C++
        foreach (var fileDescriptorProto in request.ProtoFile)
        {
            var descriptorMsg = fileDescriptorProto.MessageType[idx];
            output.AppendLine(cSharpEventClass.Generate(descriptorMsg, flag));
            idx++;
        }
        idx = 0;
        foreach (var fileDescriptorProto in request.ProtoFile)
        {
            FileDescriptor fileDescriptor = (FileDescriptor)fileDescriptorProto; //FIXME need to figure out how to parse fileDescriptorProto to fileDescriptor before passing down to funcs
            var csharpContainer = new CSharpContainer();
            output.AppendLine(csharpContainer.Generate(fileDescriptor.Services[idx],flag));
            idx++;
        }

        //TODO Experiment with Roslyn-programmatic code-formatter
        // var generatedCSCodeNodeRoot = CSharpSyntaxTree
        //     .ParseText(generatedCSCodeBody)
        //     .GetRoot();
        //
        // generatedCSCodeBody = generatedCSCodeNodeRoot

        string generatedCSCodeBody = output.ToString();
        FileDescriptor fileDescriptor = (FileDescriptor)fileDescriptorProto; //FIXME need to figure out how to parse fileDescriptorProto to fileDescriptor before passing down to funcs
        string outputFileName = GetServicesFilename(fileDescriptor);

        response.File.Add(
            new CodeGeneratorResponse.Types.File
            {
                Name = outputFileName,
                Content = generatedCSCodeBody
            }
        );

        // set result to standard output
        using (var stdout = Console.OpenStandardOutput())
        {
            response.WriteTo(stdout);
        }
    }

    private static T Deserialize<T>(Stream stream) where T : IMessage<T>, new()
    {
        return new MessageParser<T>(() => new T()).ParseFrom(stream);
    }
}
