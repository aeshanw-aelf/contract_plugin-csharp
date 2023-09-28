using Google.Protobuf.Reflection;

namespace ContractGenerator;

public class ProtoUtils
{
    //TODO Implement https://github.com/protocolbuffers/protobuf/blob/e57166b65a6d1d55fc7b18beaae000565f617f22/src/google/protobuf/compiler/csharp/names.cc#L73
    public static string GetClassName(DescriptorBase descriptor, byte flags)
    {
        return ToCSharpName(descriptor.FullName, descriptor.File);
    }

    // public string GetAccessLevel(uint flags) {
    //     return flags & Flags.INTERNAL_ACCESS ? "internal" : "public";
    // }

    private static string ToCSharpName(string name, FileDescriptor fileDescriptor)
    {
        var result = GetFileNamespace(fileDescriptor);
        if (!string.IsNullOrEmpty(result)) result += '.';

        string classname;
        if (string.IsNullOrEmpty(fileDescriptor.Package))
            classname = name;
        else
            // Strip the proto package from full_name since we've replaced it with
            // the C# namespace.
            classname = name.Substring(fileDescriptor.Package.Length + 1);

        classname = classname.Replace(".", ".Types.");

        return "global::" + result + classname;
    }

    //TODO Implement https://github.com/protocolbuffers/protobuf/blob/e57166b65a6d1d55fc7b18beaae000565f617f22/src/google/protobuf/compiler/csharp/csharp_helpers.cc#L255C35-L255C50
    private string GetPropertyName(FieldDescriptor descriptor)
    {
        // TODO: consider introducing csharp_property_name field option
        var propertyName = UnderscoresToPascalCase(GetFieldName(descriptor));

        // Avoid either our own type name or reserved names. Note that not all names
        // are reserved - a field called to_string, write_to etc would still cause a problem.
        // There are various ways of ending up with naming collisions, but we try to avoid obvious
        // ones.
        if (propertyName == descriptor.ContainingType.Name
            || propertyName == "Types"
            || propertyName == "Descriptor")
            propertyName += "_";

        return propertyName;
    }

    // Groups are hacky:  The name of the field is just the lower-cased name
    // of the group type.  In C#, though, we would like to retain the original
    // capitalization of the type name.
    public string GetFieldName(FieldDescriptor descriptor)
    {
        if (descriptor.FieldType == FieldType.Group)
            return descriptor.MessageType.Name;
        return descriptor.Name;
    }


    private string UnderscoresToPascalCase(string input)
    {
        return UnderscoresToCamelCase(input, true);
    }

    private string UnderscoresToCamelCase(string input, bool capNextLetter)
    {
        return UnderscoresToCamelCase(input, capNextLetter, false);
    }

    /// <summary>
    ///     Extract the C# Namespace for the target contract based on the Proto data.
    /// </summary>
    //TODO Implementation https://github.com/protocolbuffers/protobuf/blob/e57166b65a6d1d55fc7b18beaae000565f617f22/src/google/protobuf/compiler/csharp/names.cc#L66
    public static string GetFileNamespace(FileDescriptor fileDescriptor)
    {
        if (fileDescriptor.GetOptions().HasCsharpNamespace) return fileDescriptor.GetOptions().CsharpNamespace;
        return UnderscoresToCamelCase(fileDescriptor.Package, true, true);
    }

    /// <summary>
    ///     This Util does more than just convert underscores to camel-case. copied from the C++ original
    ///     https://github.com/protocolbuffers/protobuf/blob/e57166b65a6d1d55fc7b18beaae000565f617f22/src/google/protobuf/compiler/csharp/names.cc#L138
    /// </summary>
    public static string UnderscoresToCamelCase(string input, bool capNextLetter, bool preservePeriod)
    {
        var result = "";
        for (var i = 0; i < input.Length; i++)
            if ('a' <= input[i] && input[i] <= 'z')
            {
                if (capNextLetter)
                    result += (char)(input[i] + ('A' - 'a'));
                else
                    result += input[i];
                capNextLetter = false;
            }
            else if ('A' <= input[i] && input[i] <= 'Z')
            {
                if (i == 0 && !capNextLetter)
                    // Force first letter to lower-case unless explicitly told to
                    // capitalize it.
                    result += (char)(input[i] + ('a' - 'A'));
                else
                    // Capital letters after the first are left as-is.
                    result += input[i];
                capNextLetter = false;
            }
            else if ('0' <= input[i] && input[i] <= '9')
            {
                result += input[i];
                capNextLetter = true;
            }
            else
            {
                capNextLetter = true;
                if (input[i] == '.' && preservePeriod) result += '.';
            }

        // Add a trailing "_" if the name should be altered.
        if (input.Length > 0 && input[input.Length - 1] == '#') result += '_';

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
