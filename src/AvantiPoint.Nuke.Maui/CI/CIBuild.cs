namespace AvantiPoint.Nuke.Maui.CI;

public abstract class CIBuild
{
    public virtual string Name => GetType().Name;
    public abstract IEnumerable<ICIStage> Stages { get; }
    public virtual PushTrigger? OnPush { get; }
    public virtual PullRequestTrigger? OnPull { get; }
    public virtual ManualTrigger? ManualTrigger { get; }
    public virtual string? OnCronSchedule { get; }
    public virtual CIVariableCollection Variables => new ();

    public virtual CheckoutSubmodules Submodules => CheckoutSubmodules.False;

    public virtual int FetchDepth => 0;

    public virtual bool Clean => true;

    public virtual bool EnableToken => false;
}
