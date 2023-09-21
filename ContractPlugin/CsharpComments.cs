using Google.Protobuf.Reflection;

namespace contract_plugin_csharp;

public class CsharpComments
{
    //TODO Implementation following https://github.com/AElfProject/contract-plugin/blob/master/src/contract_csharp_generator_helpers.h#L37
    public static string Generate(FileDescriptorProto fileDescriptor,bool leading)
    {
        //can extract SourceLocation info from fileDescriptor.SourceCodeInfo.Location[0]
        throw new NotImplementedException();
    }
}
