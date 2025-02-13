using Azure;
using Azure.AI.OpenAI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;

public class TitleService
{
    private readonly Kernel _kernel;
    private readonly string _deployment;
    private readonly string _endpoint;
    private readonly string _key;
    private readonly string? _gh_PAT;

    public TitleService()
    {
        #region AzureOpenAIChatCompletion
        _deployment = "gpt-4o";
        _endpoint = "https://models.inference.ai.azure.com";
        _gh_PAT = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
        #endregion

        var aoaiClient = new AzureOpenAIClient(new Uri(_endpoint), new AzureKeyCredential(_gh_PAT));

        _kernel = Kernel.CreateBuilder()
            .AddAzureOpenAIChatCompletion(_deployment, aoaiClient)
            .Build();
    }

    public async Task<string?> GenerateTitle(string briefDescription)
    {
        KernelArguments kernelArguments = new()
        {
            { "briefDescription", briefDescription },
        };

        string? result;
        try
        {
            #pragma warning disable SKEXP0040
            var prompty = _kernel.CreateFunctionFromPromptyFile("title.prompty");
            #pragma warning restore SKEXP0040
            result = await prompty.InvokeAsync<string>(_kernel, kernelArguments);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
        return result;
    }
}
