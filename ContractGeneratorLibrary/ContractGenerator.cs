using System.Text;
using Google.Protobuf;
using Google.Protobuf.Compiler;
using Google.Protobuf.Reflection;

namespace ContractGeneratorLibrary;

//This is the main entry-point into this project and is exposed to external users
public class ContractGenerator
{
    /// <summary>
    /// Generate will return a output stream including the complete set of C# files for the Contract project. This is the primary entry-point into the ContractPlugin.
    /// </summary>
    public CodeGeneratorResponse Generate(Stream stdin)
    {
        throw new NotImplementedException();
    }
}
