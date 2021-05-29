using System;
using System.IO;
using System.Linq;

namespace AssettoCorsaChampionshipEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            string acDir = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\assettocorsa";
            Console.WriteLine(acDir);
            string[] cars = Directory.GetDirectories(acDir + "\\content\\cars").Select(Path.GetFileName).ToArray();
            string[] tracks = Directory.GetDirectories(acDir + "\\content\\tracks").Select(Path.GetFileName).ToArray();
            //
            //
            Console.WriteLine("\nCars:\n");
            Array.ForEach(cars, Console.WriteLine);
            Console.WriteLine("\n\nTracks:\n");
            Array.ForEach(tracks, Console.WriteLine);
        }
    }
}
