using AElf;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ContractGenerator;

public static class FileDescriptorSetLoader
{
    private static readonly ExtensionRegistry _extensionRegistry = new();

    static FileDescriptorSetLoader()
    {
        _extensionRegistry.Add(OptionsExtensions.Identity);
        _extensionRegistry.Add(OptionsExtensions.Base);
        _extensionRegistry.Add(OptionsExtensions.CsharpState);
        _extensionRegistry.Add(OptionsExtensions.IsView);
        _extensionRegistry.Add(OptionsExtensions.IsEvent);
        _extensionRegistry.Add(OptionsExtensions.IsIndexed);
    }

    public static IReadOnlyList<FileDescriptor> Load(Stream stream)
    {
        var fds = FileDescriptorSet.Parser.WithExtensionRegistry(_extensionRegistry).ParseFrom(stream);
        var fileInByteStrings = fds.File.Select(f => f.ToByteString());
        return FileDescriptor.BuildFromByteStrings(fileInByteStrings, _extensionRegistry);
    }
}
