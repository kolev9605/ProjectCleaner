namespace ProjCleaner
{
    using System;

    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            Cleaner cleaner = new Cleaner(@"C:\Users\Kolev\Desktop\Tets", StrategyType.VisualStudio, StrategyType.JetBrains);

            cleaner.Clean();
            Console.WriteLine(cleaner.Statistics());
        }
    }
}