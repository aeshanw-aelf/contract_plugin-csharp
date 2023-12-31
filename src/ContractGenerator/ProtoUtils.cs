using Google.Protobuf.Reflection;

namespace ContractGenerator;

public class ProtoUtils
{
    //TODO Implement https://github.com/protocolbuffers/protobuf/blob/e57166b65a6d1d55fc7b18beaae000565f617f22/src/google/protobuf/compiler/csharp/names.cc#L73
    public static string GetClassName(IDescriptor descriptor)
    {
        return ToCSharpName(descriptor.FullName, descriptor.File);
    }

    public static string GetReflectionClassName(FileDescriptor descriptor)
    {
        var result = GetFileNamespace(descriptor);
        if (result.Length > 0) result += '.';
        result += GetReflectionClassUnqualifiedName(descriptor);
        return "global::" + result;
    }

    private static string GetFileNameBase(IDescriptor descriptor)
    {
        var protoFile = descriptor.Name;
        var lastSlash = protoFile.LastIndexOf('/');
        var stringBase = protoFile[(lastSlash + 1)..];
        return UnderscoresToPascalCase(StripDotProto(stringBase));
    }

    private static string StripDotProto(string protoFile)
    {
        var lastIndex = protoFile.LastIndexOf(".", StringComparison.Ordinal);
        return protoFile[..lastIndex];
    }

    public static string GetReflectionClassUnqualifiedName(FileDescriptor descriptor)
    {
        // TODO: Detect collisions with existing messages,
        // and append an underscore if necessary.
        return GetFileNameBase(descriptor) + "Reflection";
    }


    // Implementation follows C++ original https://github.com/AElfProject/contract-plugin/blob/453bebfec0dd2fdcc06d86037055c80721d24e8a/src/contract_csharp_generator.cc#L251
    public static string GetAccessLevel(GeneratorOptions options)
    {
        return options.InternalAccess ? "internal" : "public";
    }

    private static string ToCSharpName(string name, FileDescriptor fileDescriptor)
    {
        var result = GetFileNamespace(fileDescriptor);
        if (!string.IsNullOrEmpty(result)) result += '.';

        var classname = string.IsNullOrEmpty(fileDescriptor.Package)
            ? name
            :
            // Strip the proto package from full_name since we've replaced it with
            // the C# namespace.
            name[(fileDescriptor.Package.Length + 1)..];

        classname = classname.Replace(".", ".Types.");
        classname = classname.Replace(".proto", ""); //strip-out the .proto
        return "global::" + result + classname;
    }

    /// <summary>
    ///     This Util gets the PropertyName based on the proto. Copied from the C++ original
    ///     https://github.com/protocolbuffers/protobuf/blob/e57166b65a6d1d55fc7b18beaae000565f617f22/src/google/protobuf/compiler/csharp/csharp_helpers.cc#L255C35-L255C50
    /// </summary>
    public static string GetPropertyName(FieldDescriptor descriptor)
    {
        var reservedMemberNames = new HashSet<string>
        {
            "Types",
            "Descriptor",
            "Equals",
            "ToString",
            "GetHashCode",
            "WriteTo",
            "Clone",
            "CalculateSize",
            "MergeFrom",
            "OnConstruction",
            "Parser"
        };

        // TODO: consider introducing csharp_property_name field option
        var propertyName = UnderscoresToPascalCase(GetFieldName(descriptor));

        // Avoid either our own type name or reserved names. Note that not all names
        // are reserved - a field called to_string, write_to etc would still cause a problem.
        // There are various ways of ending up with naming collisions, but we try to avoid obvious
        // ones.
        if (propertyName == descriptor.ContainingType.Name
            || reservedMemberNames.Contains(propertyName))
            propertyName += "_";

        return propertyName;
    }

    // Groups are hacky:  The name of the field is just the lower-cased name
    // of the group type.  In C#, though, we would like to retain the original
    // capitalization of the type name.
    private static string GetFieldName(FieldDescriptor descriptor)
    {
        return descriptor.FieldType == FieldType.Group ? descriptor.MessageType.Name : descriptor.Name;
    }


    internal static string UnderscoresToPascalCase(string input)
    {
        return UnderscoresToCamelCase(input, true);
    }

    /// <summary>
    ///     Extract the C# Namespace for the target contract based on the Proto data.
    /// </summary>
    //TODO Implementation https://github.com/protocolbuffers/protobuf/blob/e57166b65a6d1d55fc7b18beaae000565f617f22/src/google/protobuf/compiler/csharp/names.cc#L66
    public static string GetFileNamespace(FileDescriptor fileDescriptor)
    {
        return fileDescriptor.GetOptions().HasCsharpNamespace
            ? fileDescriptor.GetOptions().CsharpNamespace
            : UnderscoresToCamelCase(fileDescriptor.Package, true, true);
    }

    /// <summary>
    ///     This Util does more than just convert underscores to camel-case. copied from the C++ original
    ///     https://github.com/protocolbuffers/protobuf/blob/e57166b65a6d1d55fc7b18beaae000565f617f22/src/google/protobuf/compiler/csharp/names.cc#L138
    /// </summary>
    internal static string UnderscoresToCamelCase(string input, bool capNextLetter, bool preservePeriod = false)
    {
        var result = "";
        for (var i = 0; i < input.Length; i++)
            switch (input[i])
            {
                case >= 'a' and <= 'z':
                {
                    if (capNextLetter)
                        result += (char)(input[i] + ('A' - 'a'));
                    else
                        result += input[i];
                    capNextLetter = false;
                    break;
                }
                case >= 'A' and <= 'Z':
                {
                    if (i == 0 && !capNextLetter)
                        // Force first letter to lower-case unless explicitly told to
                        // capitalize it.
                        result += (char)(input[i] + ('a' - 'A'));
                    else
                        // Capital letters after the first are left as-is.
                        result += input[i];
                    capNextLetter = false;
                    break;
                }
                case >= '0' and <= '9':
                    result += input[i];
                    capNextLetter = true;
                    break;
                default:
                {
                    capNextLetter = true;
                    if (input[i] == '.' && preservePeriod) result += '.';

                    break;
                }
            }

        // Add a trailing "_" if the name should be altered.
        if (input.Length > 0 && input[^1] == '#') result += '_';

        // https://github.com/protocolbuffers/protobuf/issues/8101
        // To avoid generating invalid identifiers - if the input string
        // starts with _<digit> (or multiple underscores then digit) then
        // we need to preserve the underscore as an identifier cannot start
        // with a digit.
        // This check is being done after the loop rather than before
        // to handle the case where there are multiple underscores before the
        // first digit. We let them all be consumed so we can see if we would
        // start with a digit.
        // Note: not preserving leading underscores for all otherwise valid identifiers
        // so as to not break anything that relies on the existing behaviour
        if (result.Length > 0 && '0' <= result[0] && result[0] <= '9'
            && input.Length > 0 && input[0] == '_')
            // Concatenate a _ at the beginning
            result = '_' + result;
        return result;
    }
}
