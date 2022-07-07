using System.Collections;

namespace AvantiPoint.Nuke.Maui.CI;

public class SecretImportCollection : IEnumerable
{
    private readonly List<WorkflowSecret> _secrets = new();

    public SecretImportCollection Add(string secret)
    {
        _secrets.Add(new WorkflowSecret(secret));
        return this;
    }

    public SecretImportCollection Add(string parameter, string secret)
    {
        _secrets.Add(new WorkflowSecret(parameter, secret));
        return this;
    }

    public IEnumerator GetEnumerator() => _secrets.GetEnumerator();
}
