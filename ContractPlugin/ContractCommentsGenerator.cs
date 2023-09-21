using Google.Protobuf.Reflection;

namespace ContractPluginCsharp;

public class ContractCommentsGenerator: IGenerator<FileDescriptorProto,bool>
{
    //TODO Implementation following https://github.com/AElfProject/contract-plugin/blob/master/src/contract_csharp_generator_helpers.h#L37
    public string Generate(FileDescriptorProto fileDescriptor,bool leading)
    {
        //can extract SourceLocation info from fileDescriptor.SourceCodeInfo.Location[0]
        throw new NotImplementedException();
    }
}
