# Project Cleaner

Simple C# program to clean your projects\` assembly/trash files.

## Functionality

The API is pretty straightforward:
```C#
//Initialize the Cleaner class with path to be cleaned and params which describes what to be cleaned
Cleaner cleaner = new Cleaner(@"E:\Projects", 
StrategyType.VisualStudio, StrategyType.JetBrains);

cleaner.Clean();
Console.WriteLine(cleaner.Statistics());
```

### Supported cleaning strategies
Currently there are only 2 strategies supported:
```C#
StrategyType.VisualStudio // -> bin, obj folders
StrategyType.JetBrains // -> .idea folder
```
