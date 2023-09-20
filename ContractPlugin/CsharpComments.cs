using Google.Protobuf.Reflection;

namespace contract_plugin_csharp;

public class CsharpComments
{
    public static string Generate(FileDescriptorProto fileDescriptor,bool leading)
    {
        //can extract SourceLocation info from fileDescriptor.SourceCodeInfo.Location[0]
        //TODO Implementation
        return "";
    }
}
