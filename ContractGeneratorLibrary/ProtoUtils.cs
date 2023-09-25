using Google.Protobuf.Reflection;

namespace ContractGeneratorLibrary;

public class ProtoUtils
{
    //TODO Implement https://github.com/protocolbuffers/protobuf/blob/e57166b65a6d1d55fc7b18beaae000565f617f22/src/google/protobuf/compiler/csharp/names.cc#L73
    public static string GetClassName(FileDescriptor fileDescriptor,char flags)
    {
        throw new NotImplementedException();
    }

    //TODO Implement https://github.com/protocolbuffers/protobuf/blob/e57166b65a6d1d55fc7b18beaae000565f617f22/src/google/protobuf/compiler/csharp/names.cc#L66C36-L66C50
    public static string GetFileNamespace(FileDescriptor fileDescriptor)
    {
        throw new NotImplementedException();
    }
    
    //TODO Implement https://github.com/protocolbuffers/protobuf/blob/e57166b65a6d1d55fc7b18beaae000565f617f22/src/google/protobuf/compiler/csharp/csharp_helpers.cc#L255C35-L255C50
    public static string GetPropertyName(FieldDescriptor fieldDescriptor)
    {
        throw new NotImplementedException();
    }
    
    
}