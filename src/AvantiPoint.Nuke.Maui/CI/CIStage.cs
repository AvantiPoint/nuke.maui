namespace AvantiPoint.Nuke.Maui.CI;

public class CIStage : ICIStage
{
    public string Name { get; set; } = string.Empty;

    public IEnumerable<ICIJob> Jobs { get; set; } = Array.Empty<ICIJob>();

    public string? Environment { get; set; }
}
