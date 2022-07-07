namespace AvantiPoint.Nuke.Maui.CI;

public interface ICIStage
{
    string Name { get; }
    IEnumerable<ICIJob> Jobs { get; }
    string? Environment { get; }
}
