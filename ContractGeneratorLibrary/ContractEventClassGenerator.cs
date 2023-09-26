using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.Compiler;

namespace ContractGeneratorLibrary;

public class ContractEventClassGenerator
{
    /// <summary>
    /// Generate will produce a chunk of C# code that serves as the Event class of the AElf Contract. Events are used internally to represent events that have happened during the execution of a smart contract.
    /// </summary>
    //TODO Implement following https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L546
    public string Generate(MessageDescriptor descriptor, uint flags)
    {
        // descriptor.Fields[0].ContainingType.Name;
        throw new NotImplementedException();
    }

    /// <summary>
    /// Determines if the proto-message is of EventType based on Aelf.options
    /// </summary>
    public bool IsEventMessageType(MessageDescriptor message){
        return message.GetOptions().GetExtension(AElf.OptionsExtensions.IsEvent);
    }

    /// <summary>
    /// Determines if the proto-message is of IndexedType based on Aelf.options
    /// </summary>
    public bool IsIndexedField(FieldDescriptor field)
    {
        return field.GetOptions().GetExtension(AElf.OptionsExtensions.IsIndexed);
    }

    /// <summary>
    /// Determines if the proto-message is of ViewType based on Aelf.options
    /// </summary>
    public bool IsViewOnlyMethod(MethodDescriptor method)
    {
        return method.GetOptions().GetExtension(AElf.OptionsExtensions.IsView);
    }

    /// <summary>
    /// Counts the size of the ServiceBase type of the proto-message based on Aelf.options.
    /// replaces the C++ method https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L148
    /// </summary>
    public int GetServiceBaseCount(ServiceDescriptor service)
    {
        return service.GetOptions().GetExtension(AElf.OptionsExtensions.Base).Count;
    }

    /// <summary>
    /// GetServiceBase returns ServiceBase type at a particular index of the proto-message based on Aelf.options.
    /// replaces the C++ method https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L152C1-L154C2
    /// </summary>
    public string GetServiceBase(ServiceDescriptor service, int index)
    {
        //TODO maybe throw exception for -ve index
        return service.GetOptions().GetExtension(AElf.OptionsExtensions.Base)[index];
    }
}
