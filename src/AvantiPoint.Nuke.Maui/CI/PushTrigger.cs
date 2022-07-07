namespace AvantiPoint.Nuke.Maui.CI;

public class PushTrigger
{
    public IEnumerable<string> Branches { get; set; } = Array.Empty<string>();
    public IEnumerable<string> BranchesIgnore { get; set; } = Array.Empty<string>();
    public IEnumerable<string> Tags { get; set; } = Array.Empty<string>();
    public IEnumerable<string> TagsIgnore { get; set; } = Array.Empty<string>();
    public IEnumerable<string> IncludePaths { get; set; } = Array.Empty<string>();
    public IEnumerable<string> ExcludePaths { get; set; } = Array.Empty<string>();

    public bool Disabled { get; set; }
    public bool? Batch { get; set; }
    public bool? AutoCancel { get; set; }

    public static implicit operator PushTrigger(string branch) =>
        new () { Branches = new[] { branch } };

    public static implicit operator PushTrigger(string[] branches) =>
        new()
        {
            Branches = branches.Where(x => !x.StartsWith("!")),
            BranchesIgnore = branches.Where(x => x.StartsWith("!")).Select(x => x.Substring(1))
        };
}
