using Google.Protobuf.Reflection;

// using Microsoft.CodeAnalysis.CSharp;
// using static Microsoft.CodeAnalysis.SyntaxNode;

namespace ContractGenerator;

// Generates the overall "container" for the generated C# contract
public class ContractContainerGenerator
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

    /// <summary>
    /// Generate will produce a chunk of C# code BaseClass for the AElf Contract.
    /// </summary>
    //TODO Implement based on https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L422
    private string GenerateContractBaseClass(ServiceDescriptor service)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Generates instantiations for static readonly aelf::Method fields based on the proto
    /// </summary>
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

    /// <summary>
    /// Generates the Class for the ReferenceState as part of the aelf contract
    /// </summary>
    //TODO Implement following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L514
    private static string GenerateReferenceClass(ServiceDescriptor serviceDescriptor, byte flags)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Generates the IReadOnlyList of ServiceDescriptors as part of the contract
    /// </summary>
    //TODO Implement following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L386
    private static string GenerateAllServiceDescriptorsProperty(ServiceDescriptor serviceDescriptor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Generates a section of instantiated aelf Marshallers as part of the contract
    /// </summary>
    //TODO Implement following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L332
    private static string GenerateMarshallerFields(ServiceDescriptor serviceDescriptor)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Generates the header comments for the C# Container of the AElf Contract.
    /// </summary>
    //TODO Implement following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L106
    private static string GenerateDocCommentBody(SourceCodeInfo sourceCodeInfo)
    {
        // request.ProtoFile[0].SourceCodeInfo.Location[0] this object can be extracted from CodeGeneratorRequest
        throw new NotImplementedException();
    }

    private static string GetServerClassName(ServiceDescriptor service)
    {
        return service.Name + "Base";
    }

    private static string GetStateTypeName(ServiceDescriptor service)
    {
        return service.GetOptions().GetExtension(AElf.OptionsExtensions.CsharpState).ToString();
    }

    //TODO Implementation following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L60
    private static string GenerateDocCommentBody(ServiceDescriptor service)
    {
        throw new NotImplementedException();
    }

    //TODO Implementation https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L251
    private static string GetAccessLevel(byte flags)
    {
        throw new NotImplementedException();
    }

    //TODO Implement https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L115
    private static string GetServiceContainerClassName(ServiceDescriptor service)
    {
        //TODO service null check
        return $"{service.Name}Container";
    }

    //TODO Implement https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L204
    // public static List<ServiceDescriptor> GetFullService(ServiceDescriptor service){
    //     List<ServiceDescriptor> allDependedServices = new List<ServiceDescriptor>();
    //     SortedSet<int> seen;
    //     DepthFirstSearch(service, &allDependedServices, &seen);
    //     Dictionary<string, ServiceDescriptor> services = new Dictionary<string, ServiceDescriptor>();
    //     foreach(var dependedService in allDependedServices)
    //     {
    //         services.Add(dependedService.File.Name, dependedService);
    //     }
    //     List<ServiceDescriptor> result = new List<ServiceDescriptor>();
    //     List<string> bases = new List<string>();
    //     SortedSet<string> seenBases;
    //
    //     //FIXME DepthFirstSearchForBase(service, &bases, &seenBases, services);
    //     foreach(var baseItem in bases){
    //         int lastIndex = result.Count();
    //         result.Insert(lastIndex,services[baseItem]); //push to back of list
    //     }
    //     return result;
    // }

    //TODO Implement https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L222
    // public Methods GetFullMethod(ServiceDescriptor service){
    //     List<ServiceDescriptor> services = GetFullService(service);
    //     Methods methods;
    //     for(Services::iterator itr = services.begin(); itr != services.end(); ++itr){
    //         for(int i = 0; i < (*itr)->method_count(); i++){
    //             methods.push_back((*itr)->method(i));
    //         }
    //     }
    //     return methods;
    // }

    /// <summary>
    /// Generate will produce a chunk of C# code that serves as the container class of the AElf Contract.
    /// </summary>
    //TODO Implement following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L612
    public string Generate(ServiceDescriptor serviceDescriptor,byte flags)
    {
        throw new NotImplementedException();
    }
}
