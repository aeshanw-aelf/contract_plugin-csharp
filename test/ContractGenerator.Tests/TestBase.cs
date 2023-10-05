using Google.Protobuf.Reflection;

namespace ContractGenerator.Tests;

public class TestBase
{
    protected static IReadOnlyList<FileDescriptor> GetFileDescriptors(string testcaseName)
    {
        using var stream = File.Open($"testcases/{testcaseName}/descriptor.bin", FileMode.Open);

        return FileDescriptorSetLoader.Load(stream);
    }
}
