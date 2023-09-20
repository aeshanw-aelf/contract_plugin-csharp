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

    private string GenerateStaticMethodField(MethodDescriptor methodDescriptor)
    {
        return "";
    }

    private string GenerateStubClass(ServiceDescriptor serviceDescriptor)
    {
        return "";
    }

    private string GenerateReferenceClass(ServiceDescriptor serviceDescriptor, uint flags)
    {
        return "";
    }

    private string GenerateAllServiceDescriptorsProperty(ServiceDescriptor serviceDescriptor)
    {
        return "";
    }

    private string GenerateMarshallerFields(ServiceDescriptor serviceDescriptor)
    {
        return "";
    }

    private string GenerateDocCommentBody(SourceCodeInfo sourceCodeInfo)
    {
        // request.ProtoFile[0].SourceCodeInfo.Location[0] this object can be extracted from CodeGeneratorRequest
        return "";
    }

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
        //TODO Implement

        // set as response
        return "";
    }
}
