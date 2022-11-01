using CursorFinder.Services;
using System;
using System.ServiceModel;

namespace CursorFinderHost
{
    /// <summary>
    /// Запускать от имени Администратора
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var host = new ServiceHost(typeof(CursorFinderService)))
                {
                    host.Open();
                    Console.WriteLine("Host started...");

                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }

        }
    }

}


