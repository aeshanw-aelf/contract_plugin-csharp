using AElf;
using Google.Protobuf.Reflection;

namespace ContractGenerator;

public static class EventTypeGenerator
{
    public static void GenerateEvent(IndentPrinter printer, MessageDescriptor message, byte flags)
    {
        if (!IsEventMessageType(message)) return;
        printer.Print(
            $"{ProtoUtils.GetAccessLevel(flags)} partial class {message.Name} : aelf::IEvent<{message.Name}>");
        printer.Print("{");
        {
            printer.Indent();
            // GetIndexed
            printer.Print($"public global::System.Collections.Generic.IEnumerable<{message.Name}> GetIndexed()");
            printer.Print("{");
            {
                printer.Indent();
                printer.Print($"return new List<{message.Name}>");
                printer.Print("{");
                var fields = message.Fields.InFieldNumberOrder();
                foreach (var field in fields)
                {
                    if (field == null) continue;
                    if (!IsIndexedField(field)) continue;
                    printer.Print($"new {message.Name}");
                    printer.Print("{");
                    {
                        printer.Indent();
                        var propertyName = ProtoUtils.GetPropertyName(field);
                        printer.Print($"{propertyName} = {propertyName}");
                        printer.Outdent();
                    }
                    printer.Print("},");
                }

                printer.Print("};");
                printer.Outdent();
            }
            printer.Print("}\n"); // end GetIndexed

            // GetNonIndexed
            printer.Print($"public {message.Name} GetNonIndexed()");
            printer.Print("{");
            {
                printer.Indent();
                printer.Print($"return new {message.Name}");
                printer.Print("{");
                {
                    printer.Indent();
                    var fields = message.Fields.InFieldNumberOrder();
                    foreach (var field in fields)
                    {
                        if (field == null) continue;
                        if (IsIndexedField(field)) continue;
                        var propertyName = ProtoUtils.GetPropertyName(field);
                        printer.Print($"{propertyName} = {propertyName},");
                    }

                    printer.Outdent();
                }
                printer.Print("};");
                printer.Outdent();
            }
            printer.Print("}");
            printer.Outdent();
        }

        printer.Print("}\n");
    }

    #region Helper Methods

    /// <summary>
    ///     Determines if the proto-message is of IndexedType based on Aelf.options
    /// </summary>
    private static bool IsIndexedField(FieldDescriptor field)
    {
        return field.GetOptions() != null && field.GetOptions().GetExtension(OptionsExtensions.IsIndexed);
    }

    /// <summary>
    ///     Determines if the proto-message is of EventType based on Aelf.options
    /// </summary>
    private static bool IsEventMessageType(MessageDescriptor message)
    {
        return message.GetOptions().GetExtension(OptionsExtensions.IsEvent);
    }

    #endregion
}
