using MathServiceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MathServiceHost
{
    /// <summary>
    /// The MathServiceHost is a typical Console Application 
    /// being used to host the MathService service class
    /// for development and testing purposes
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main entry method of the Service Host application
        /// </summary>
        /// <param name="args">No arguments used</param>
        static void Main(string[] args)
        {
            // Create Service Host 
            ServiceHost host = null;
            Console.WriteLine("Service starting...");

            try
            {
                // Create new host service for the MathService
                host = new ServiceHost(typeof(MathService));
                // Open the host
                host.Open();

                // Keeps the service open until the user clicks enter
                Console.Write("Press <ENTER> to close the service...");
                Console.ReadLine();

            } // end of if

            // General catch of exceptions to the server host 
            // console application
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            } // end of catch

            // Always Executes to close the host
            finally
            {
                if (host != null)
                {
                    host.Close();
                }
            } // end of finally

        } // end of Main method

    } // end of class
} // end of namespace
