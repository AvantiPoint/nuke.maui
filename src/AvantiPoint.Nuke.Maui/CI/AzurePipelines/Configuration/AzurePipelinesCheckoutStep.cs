using Nuke.Common.Utilities;

namespace AvantiPoint.Nuke.Maui.CI.AzurePipelines.Configuration;

// https://docs.microsoft.com/en-us/azure/devops/pipelines/repos/pipeline-options-for-git?view=azure-devops&tabs=yaml#checkout-submodules
internal class AzurePipelinesCheckoutStep : AzurePipelinesStep
{
    public CheckoutSubmodules InclueSubmodules { get; set; }
    public bool? IncludeLargeFileStorage { get; set; }
    public int? FetchDepth { get; set; }
    public bool? Clean { get; set; }

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine("- checkout: self");
        using (writer.Indent())
        {
            if (IncludeLargeFileStorage.HasValue && IncludeLargeFileStorage.Value)
            {
                writer.WriteLine($"lfs: true");
            }

            if (InclueSubmodules != CheckoutSubmodules.False)
            {
                writer.WriteLine($"submodules: {InclueSubmodules}".ToLower());
            }

            if (FetchDepth.HasValue)
            {
                writer.WriteLine($"fetchDepth: {FetchDepth.Value}");
            }

            if (Clean.HasValue)
            {
                writer.WriteLine($"clean: {Clean.Value}".ToLower());
            }
        }
    }
}
