using System.Text;
using Google.Protobuf;
using Google.Protobuf.Compiler;
using Google.Protobuf.Reflection;

namespace ContractPluginCsharp;

internal enum Flags : uint
{
    GENERATE_CONTRACT = 0,
    GENERATE_STUB = 0x2, // hex for 0000 0010
    GENERATE_REFERENCE = 0x4, // hex for 0000 0100
    GENERATE_EVENT = 0x8, // hex for 0000 1000
    INTERNAL_ACCESS = 0x80 // hex for 1000 0000
}

//This is the main entry-point into this project and is exposed to external users
public class ContractGenerator
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

    public CodeGeneratorResponse Generate(Stream stdin)
    {
        // get request from standard input
        CodeGeneratorRequest request;
        FileDescriptorSet descriptorSet;
         var response = new CodeGeneratorResponse();

         using (stdin)
         {
             request = Deserialize<CodeGeneratorRequest>(stdin); //TODO if this request seems to be unused perhaps remove?
             descriptorSet = FileDescriptorSet.Parser.ParseFrom(stdin);
         }

        var byteStrings = descriptorSet.File.Select(f => f.ToByteString()).ToList();
        var fileDescriptors = FileDescriptor.BuildFromByteStrings(byteStrings);
        //TODO need to confirm if the above method is correct on how to parse fileDescriptorProto to fileDescriptor before passing down to funcs

        //Based on the C++ example this whole method should only 1 fileDescriptor hence for a list we should probably handle/iterate over it
        foreach (var fileDescriptor in fileDescriptors)
        {
            StringBuilder output = new StringBuilder();
            //TODO Implement logic as per
            //GenerateEvent
            var cSharpEventClass = new ContractEventClassGenerator();
            var flag = (uint)Flags.GENERATE_STUB; //TODO need to make this dynamic like in the C++
            foreach (var descriptorMsg in fileDescriptor.MessageTypes)
            {
                output.AppendLine(cSharpEventClass.Generate(descriptorMsg, flag));
            }
            //GenerateContainer
            var csharpContainer = new ContractContainerGenerator();
            foreach (var serviceDescriptor in fileDescriptor.Services)
            {
                output.AppendLine(csharpContainer.Generate(serviceDescriptor, flag));
            }

            //TODO Experiment with Roslyn-programmatic code-formatter
            // var generatedCSCodeNodeRoot = CSharpSyntaxTree
            //     .ParseText(generatedCSCodeBody)
            //     .GetRoot();
            //
            // generatedCSCodeBody = generatedCSCodeNodeRoot

            string generatedCSCodeBody = output.ToString();
            string outputFileName = GetServicesFilename(fileDescriptor);

            response.File.Add(
                new CodeGeneratorResponse.Types.File
                {
                    Name = outputFileName,
                    Content = generatedCSCodeBody
                }
            );
        }

        return response;
    }

    private static T Deserialize<T>(Stream stream) where T : IMessage<T>, new()
    {
        return new MessageParser<T>(() => new T()).ParseFrom(stream);
    }
}
