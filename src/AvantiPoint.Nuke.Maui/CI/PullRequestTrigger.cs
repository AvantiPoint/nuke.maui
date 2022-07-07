namespace AvantiPoint.Nuke.Maui.CI;

public class PullRequestTrigger
{
    public IEnumerable<string> Branches { get; set; } = Array.Empty<string>();
    public IEnumerable<string> IncludePaths { get; set; } = Array.Empty<string>();
    public IEnumerable<string> ExcludePaths { get; set; } = Array.Empty<string>();

    public bool Disabled { get; set; }
    public bool? Batch { get; set; }
    public bool? AutoCancel { get; set; }

    public static implicit operator PullRequestTrigger(string branch) =>
        new() { Branches = new[] { branch } };

    public static implicit operator PullRequestTrigger(string[] branches) =>
        new() { Branches = branches };

    public static implicit operator PullRequestTrigger(bool disabled) =>
        new() { Disabled = disabled };
}
