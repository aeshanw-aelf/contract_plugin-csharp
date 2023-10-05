namespace ContractGenerator.Tests;

public class EventTypeGeneratorTests : TestBase
{
    [Fact]
    public void TestGenerateEvent_NoErrors()
    {
        var indentPrinter = new IndentPrinter();
        var fileDescriptors = GetFileDescriptors("helloworld");
        var msg = fileDescriptors[^1].MessageTypes.Last();

        EventTypeGenerator.GenerateEvent(indentPrinter, msg, FlagConstants.GenerateEvent);
        var eventCodeStr = indentPrinter.PrintOut();
        const string expectedCodeStr =
            @"public partial class UpdatedMessage : aelf::IEvent<UpdatedMessage>
{
  public global::System.Collections.Generic.IEnumerable<UpdatedMessage> GetIndexed()
  {
    return new List<UpdatedMessage>
    {
    };
  }

  public UpdatedMessage GetNonIndexed()
  {
    return new UpdatedMessage
    {
      Value = Value,
    };
  }
}

";
        Assert.Equal(expectedCodeStr, eventCodeStr);
    }
}
