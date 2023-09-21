using Google.Protobuf.Compiler;
using Google.Protobuf.Reflection;

// using Microsoft.CodeAnalysis.CSharp;
// using static Microsoft.CodeAnalysis.SyntaxNode;

namespace contract_plugin_csharp;

// Generates the overall "container" for the generated C# contract
public class CSharpContainer
{
    // TODO remove after development
    // This is for debugging purposes only
    private static void DumpCodeRequestTxtToFile(string textToWrite, string filePath)
    {
        try
        {
            // Write the text to the file.
            File.WriteAllText(filePath, textToWrite);

            Console.WriteLine($"Text successfully written to the file: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DumpCodeRequestTxtToFile error: {ex.Message}");
        }
    }

    //TODO Implement following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L349
    private string GenerateStaticMethodField(MethodDescriptor methodDescriptor)
    {
        throw new NotImplementedException();
    }

    //TODO Implement following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L484
    private string GenerateStubClass(ServiceDescriptor serviceDescriptor)
    {
        throw new NotImplementedException();
    }

    //TODO Implement following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L514
    private string GenerateReferenceClass(ServiceDescriptor serviceDescriptor, uint flags)
    {
        throw new NotImplementedException();
    }

    //TODO Implement following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L386
    private string GenerateAllServiceDescriptorsProperty(ServiceDescriptor serviceDescriptor)
    {
        throw new NotImplementedException();
    }

    //TODO Implement following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L332
    private string GenerateMarshallerFields(ServiceDescriptor serviceDescriptor)
    {
        throw new NotImplementedException();
    }

    //TODO Implement following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L106
    private string GenerateDocCommentBody(SourceCodeInfo sourceCodeInfo)
    {
        // request.ProtoFile[0].SourceCodeInfo.Location[0] this object can be extracted from CodeGeneratorRequest
        throw new NotImplementedException();
    }

    //TODO Implement following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L612
    public string Generate(ServiceDescriptor serviceDescriptor,uint flags)
    {
        /*
        if(!request.IsInitialized())
        {
            //TODO return an error insted
            return new CodeGeneratorResponse();
        }
        DumpCodeRequestTxtToFile(request.ToString(),"codeGeneratorRequest.txt");
        */

        // set as response
        throw new NotImplementedException();
    }
}
