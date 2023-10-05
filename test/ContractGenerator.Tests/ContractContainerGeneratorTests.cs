namespace ContractGenerator.Tests;

public class ContractContainerGeneratorTests : TestBase
{
    [Fact]
    public void TestGenerateContractBaseClass_NoErrors()
    {
        var indentPrinter = new IndentPrinter();
        var fileDescriptors = GetFileDescriptors("contract_with_bases");
        var svc = fileDescriptors[^1].Services.Last();

        ContractContainerGenerator.GenerateContractBaseClass(indentPrinter, svc);
        var contractBaseCodeStr = indentPrinter.PrintOut();
        const string expectedCodeStr =
            @"/// <summary>Base class for the contract of ContractWithBasesBase</summary>
public abstract partial class ContractWithBasesBase : AElf.Sdk.CSharp.CSharpSmartContract<DummyState>
{
  public abstract global::Google.Protobuf.WellKnownTypes.Empty GrandParentMethod(global::Google.Protobuf.WellKnownTypes.Empty input);
  public abstract global::Google.Protobuf.WellKnownTypes.Empty ParentMethod(global::Google.Protobuf.WellKnownTypes.Empty input);
  public abstract global::Google.Protobuf.WellKnownTypes.Empty Update(global::Google.Protobuf.WellKnownTypes.StringValue input);
}

";
        Assert.Equal(expectedCodeStr, contractBaseCodeStr);
    }
}
