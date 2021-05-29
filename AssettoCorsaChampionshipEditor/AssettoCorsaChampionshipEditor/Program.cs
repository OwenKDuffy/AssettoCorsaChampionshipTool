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
            string[] cars = GetListOfCars(acDir);
            string[] tracks = GetListOfTracks(acDir);


            Championship c = new Championship(acDir + "\\content\\career\\series_dtm2020");
            c.PrettyPrintChampionshipDetails();
            c.ExportChampionshipToFile(acDir);

            //Console.WriteLine("Exporting");
            //Console.WriteLine("\nCars:\n");
            //Array.ForEach(cars, Console.WriteLine);
            //Console.WriteLine("\n\nTracks:\n");
            //Array.ForEach(tracks, Console.WriteLine);
        }

        private static string[] GetListOfCars(string gameDirectory)
        {
            return Directory.GetDirectories(gameDirectory + "\\content\\cars").Select(Path.GetFileName).ToArray();
        }
        private static string[] GetListOfTracks(string gameDirectory)
        {
            return Directory.GetDirectories(gameDirectory + "\\content\\tracks").Select(Path.GetFileName).ToArray();
        }
    }
}
