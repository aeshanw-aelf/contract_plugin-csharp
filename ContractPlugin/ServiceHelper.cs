using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace contract_plugin_csharp;

// private Services List<ServiceDescriptor>
internal static class ServiceHelper
{
    public static string GetServerClassName(ServiceDescriptor service)
    {
        return service.Name + "Base";
    }

    public static string GetStateTypeName(ServiceDescriptor service)
    {
        //aelf.csharp_state is 505030 as per proto
        var ext = new Extension<ServiceOptions, FieldCodec<string>>(505030, default);
        return service.GetOptions().GetExtension(ext).ToString();
    }

    public static string GenerateDocCommentBody(ServiceDescriptor service)
    {
        //TODO Implementation
        return "";
    }

    public static string GetAccessLevel(char flags)
    {
        //TODO Implementation
        return "";
    }

    public static string GetServiceContainerClassName(ServiceDescriptor service)
    {
        //TODO service null check
        return $"{service.Name}Container";
    }

    //TODO Implement
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

    //TODO Implement
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
}
