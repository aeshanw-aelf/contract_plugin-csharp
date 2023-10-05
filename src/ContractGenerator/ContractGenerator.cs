using Google.Protobuf;
using Google.Protobuf.Compiler;
using Google.Protobuf.Reflection;

namespace ContractGenerator;

public class FlagConstants
{
    public const byte GenerateContract = 0x01; // hex for 0000 0001
    public const byte GenerateStub = 0x02; // hex for 0000 0010
    public const byte GenerateReference = 0x04; // hex for 0000 0100
    public const byte GenerateEvent = 0x08; // hex for 0000 1000
    public const byte InternalAccess = 0x80; // hex for 1000 0000

    public const byte GenerateContractWithEvent = GenerateContract | GenerateEvent;
    public const byte GenerateStubWithEvent = GenerateStub | GenerateEvent;
}

//This is the main entry-point into this project and is exposed to external users
public class ContractGenerator
{
    /// <summary>
    ///     GetServicesFilename generates Services FileName based on the FileDescriptor
    /// </summary>
    private static string GetServicesFilename(FileDescriptor fileDescriptor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Generates a set of C# files from the input stream containing the proto source. This is the primary entry-point into
    ///     the ContractPlugin.
    /// </summary>
    public CodeGeneratorResponse Generate(Stream stdin)
    {
        throw new NotImplementedException();
    }

    private static T Deserialize<T>(Stream stream) where T : IMessage<T>, new()
    {
        return new MessageParser<T>(() => new T()).ParseFrom(stream);
    }
}
