using HtmlPdfConverter;
using PuppeteerSharp;
using System.Linq;

async Task GeneratePdf(string Filename)
{
    string outputFilePath = "//var/tmp/" + Filename.Split('.')[0] + ".pdf";
    var browserFetcher = new BrowserFetcher();
    await browserFetcher.DownloadAsync();
    await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true , Args = new string[] { "--no-sandbox"} });
    await using var page = await browser.NewPageAsync();
    await page.GoToAsync($"file:///var/tmp/{Filename}");
    await page.PdfAsync(outputFilePath);
}

Thread.Sleep(5000);
string filename = "";
ConfigProvider converterParameters = new ConfigProvider();
if (!converterParameters.GetConfigParams())
{
    Console.WriteLine("Error! Converter couldn't start. Please, check config file (.config)");
    return -1;
}
Console.WriteLine($"Converter (id = {converterParameters.Id}) is running");
//Find previous active document
filename = await Client.Instance.GetCurrentDocument(converterParameters.Id);
while (true)
{
    if (!string.IsNullOrEmpty(filename))
    {
        Console.WriteLine($"Generating pdf file for {filename}");
        await GeneratePdf(filename);
        File.Delete("//var/tmp/" + filename);
        await Client.Instance.RemoveDocument(converterParameters.Id);
    }
    Console.WriteLine("Waiting file for converting...");
    Thread.Sleep(5000);
    filename = await Client.Instance.GetNewDocument(converterParameters.Id);
}

return 0;

