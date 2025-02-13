// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Console.WriteLine("Please enter a brief description of the show:");
string? BriefDescription = Console.ReadLine();

var titleService = new TitleService();
var title = await titleService.GenerateTitle(BriefDescription);
Console.WriteLine($"The title of the show is: {title}");
Console.ReadLine();