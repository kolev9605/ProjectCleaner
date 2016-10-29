# Project Cleaner

Simple C# program to clean your projects from assembly/trash files.

## Functionality

The API is pretty straightforward:
`
//Initialize the Cleaner class with path to be cleaned and params which describes what to be cleaned
Cleaner cleaner = new Cleaner(@"C:\Users\Kolev\Desktop\Tets", 
StrategyType.VisualStudio, StrategyType.JetBrains);

cleaner.Clean();
Console.WriteLine(cleaner.Statistics());
`

### Supported cleaning methods
Currently there are only 2 types supported:
`
StrategyType.VisualStudio //bin, obj folders
StrategyType.JetBrains //.idea folder
`
