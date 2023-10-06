using AElf;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ContractGenerator.Tests;

public class ProtoUtilsTests
{
    private static readonly ExtensionRegistry _extensionRegistry = new();

    static ProtoUtilsTests()
    {
        _extensionRegistry.Add(OptionsExtensions.Identity);
        _extensionRegistry.Add(OptionsExtensions.Base);
        _extensionRegistry.Add(OptionsExtensions.CsharpState);
        _extensionRegistry.Add(OptionsExtensions.IsView);
        _extensionRegistry.Add(OptionsExtensions.IsEvent);
        _extensionRegistry.Add(OptionsExtensions.IsIndexed);
    }

    private static FileDescriptorSet GetFileDescriptorSet(string testcaseName)
    {
        var descriptor = File.ReadAllBytes($@"testcases/{testcaseName}/descriptor.bin");
        return FileDescriptorSet.Parser.WithExtensionRegistry(_extensionRegistry).ParseFrom(descriptor);
    }

    [Theory]
    [InlineData("foo_bar.baz", "FooBar.Baz")]
    public void Test_UnderscoresToCamelCase_CapNextLetter_And_PreservingPeriod(string input, string output)
    {
        var o = ProtoUtils.UnderscoresToCamelCase(input, true, true);
        Assert.Equal(output, o);
    }

    [Theory]
    [InlineData("foo_bar.baz", "FooBarBaz")]
    public void Test_UnderscoresToCamelCase_CapNextLetter_And_NotPreservingPeriod(string input, string output)
    {
        var o = ProtoUtils.UnderscoresToCamelCase(input, true);
        Assert.Equal(output, o);
    }

    [Theory]
    [InlineData("foo_bar.baz", "FooBarBaz")]
    public void Test_UnderscoresToPascalCase_CapNextLetter_And_NotPreservingPeriod(string input, string output)
    {
        var o = ProtoUtils.UnderscoresToPascalCase(input);
        Assert.Equal(output, o);
    }

    [Fact]
    public void GetClassName_ReturnsCorrectClassName()
    {
        // Arrange: Create a DescriptorBase with a known FullName and File
        var fds = GetFileDescriptorSet("helloworld");
        var byteStrings = fds.File.Select(f => f.ToByteString());
        var fileDescriptors = FileDescriptor.BuildFromByteStrings(byteStrings, _extensionRegistry);
        var svc = fileDescriptors[^1].Services.Last();

        var opt = svc.GetOptions();
        var state = opt.GetExtension(OptionsExtensions.CsharpState);

        // Act: Call the GetClassName method
        var className = ProtoUtils.GetClassName(svc);

        // Assert: Verify the expected result
        Assert.Equal("global::AElf.Contracts.HelloWorld.HelloWorld", className);
    }

    [Fact]
    public void GetCsharpComments_ReturnsComments()
    {
        // Arrange: Create a DescriptorBase with a known FullName and File
        var fds = GetFileDescriptorSet("helloworld");
        var byteStrings = fds.File.Select(f => f.ToByteString());
        var fileDescriptors = FileDescriptor.BuildFromByteStrings(byteStrings, _extensionRegistry);
        var file = fileDescriptors[^1];

        // Act: Call the GetClassName method
        var comments = ProtoUtils.GetCsharpComments(file, true);
        const string expectedComments = @"// These are test header comments!
// The namespace of this class
// The name of the state class the smart contract is going to use to access blockchain state
// Actions (methods that modify contract state)
// Stores the value in contract state
// Views (methods that don't modify contract state)
// Get the value stored from contract state
// An event that will be emitted from contract method call
";


        // Assert: Verify the expected result
        Assert.Equal(expectedComments, comments);
    }

    [Fact]
    public void GetPropertyName_ReturnsCorrectPropertyName()
    {
        // Arrange: Create a DescriptorBase with a known FullName and File
        var fds = GetFileDescriptorSet("helloworld");
        var byteStrings = fds.File.Select(f => f.ToByteString());
        var fileDescriptors = FileDescriptor.BuildFromByteStrings(byteStrings, _extensionRegistry);
        var svc = fileDescriptors[^1].Services.Last();

        var opt = svc.GetOptions();
        var state = opt.GetExtension(OptionsExtensions.CsharpState);

        var fields = fileDescriptors[^1].MessageTypes.Last().Fields;

        var fieldsList = fields.InFieldNumberOrder();
        foreach (var field in fieldsList)
        {
            Assert.NotNull(field);
            // Act: Call the GetPropertyName method
            var propertyName = ProtoUtils.GetPropertyName(field);

            // Assert: Verify the expected result
            Assert.Equal("Value", propertyName);
        }
    }
}
