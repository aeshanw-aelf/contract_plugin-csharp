using System.Text;
using Google.Protobuf.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ContractGenerator;

public class FlagConstants
{
    public const byte GenerateContract = 0x01; // hex for 0000 0001
    public const byte GenerateStub = 0x02;     // hex for 0000 0010
    public const byte GenerateReference = 0x04; // hex for 0000 0100
    public const byte GenerateEvent = 0x08;    // hex for 0000 1000
    public const byte InternalAccess = 0x80;   // hex for 1000 0000

    public const byte GenerateContractWithEvent = GenerateContract | GenerateEvent;
    public const byte GenerateStubWithEvent = GenerateStub | GenerateEvent;
}

//This is the main entry-point into this project and is exposed to external users
public class ContractGenerator
{
    /// <summary>
    /// GetServicesFilename generates Services FileName based on the FileDescriptor
    /// </summary>
    private static string GetServicesFilename(FileDescriptor fileDescriptor)
    {
        //TODO Implementation
        return "";
    }

    /// <summary>
    /// Generates a set of C# files from the input stream containing the proto source. This is the primary entry-point into the ContractPlugin.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
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
            var output = new StringBuilder();
            //TODO Implement logic as per
            //GenerateEvent
            var cSharpEventClass = new ContractEventClassGenerator();
            const byte flag = FlagConstants.GenerateStub; //TODO need to make this dynamic like in the C++
            foreach (var descriptorMsg in fileDescriptor.MessageTypes)
            {
                output.AppendLine(ContractEventClassGenerator.Generate(descriptorMsg, flag));
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

            var generatedCsCodeBody = output.ToString();
            var outputFileName = GetServicesFilename(fileDescriptor);

            response.File.Add(
                new CodeGeneratorResponse.Types.File
                {
                    Name = outputFileName,
                    Content = generatedCsCodeBody
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
