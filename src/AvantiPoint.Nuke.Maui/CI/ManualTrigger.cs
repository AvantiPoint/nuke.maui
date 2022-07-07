namespace AvantiPoint.Nuke.Maui.CI;

public class ManualTrigger
{
    public IEnumerable<string> OptionalInputs { get; set; } = Array.Empty<string>();
    public IEnumerable<string> RequiredInputs { get; set; } = Array.Empty<string>();
}
