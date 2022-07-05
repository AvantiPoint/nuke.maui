using Nuke.Common.Utilities;

namespace AvantiPoint.Nuke.Maui.CI;

internal struct WorkflowSecret
{
    public WorkflowSecret(string secret)
    {
        if(secret.Contains('='))
        {
            var parts = secret.Split('=');
            Name = parts[0];
            Secret = parts[1];
        }
        else
        {
            Name = secret;
            Secret = secret.SplitCamelHumpsWithKnownWords().JoinUnderscore().ToUpperInvariant();
        }
    }

    public string Name { get; }

    public string Secret { get; }

    public static implicit operator WorkflowSecret(string secret) =>
        new(secret);
}
