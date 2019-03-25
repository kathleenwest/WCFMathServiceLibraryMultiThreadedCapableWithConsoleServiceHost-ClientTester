using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib;
using System.Numerics;
using Utilities;

namespace MathClient
{
    /// <summary>
    /// MathClient
    /// This client program demonstrates multi-threaded access with 
    /// various combinations of instance context modes and 
    /// concurrency levels. The client will access these methods 
    /// using sequential and multi-threaded techniques to 
    /// demonstrate the difference in performance.
    /// The goal of this client is to test a batch of calls 
    /// to the service using a traditional sequential loop 
    /// and a parallelized loop. Results of the performance
    /// are printed to the console window. 
    /// </summary>
    class Program
    {
        #region fields

        // the proxy object that connects to the service
        static IMathService m_Proxy = null;

        // holds the number of iterations to perform per test
        static int m_Tests = 250;

        // holds the initial number to use in each loop when calculating square roots
        static BigInteger m_StartNum = BigInteger.Parse("1234567890123456");

        #endregion fields

        /// <summary>
        /// Main entry method for the program
        /// </summary>
        /// <param name="args">No arguments used</param>
        static void Main(string[] args)
        {
            // initialize the client proxy channel to the service
            m_Proxy = ProxyGen.GetChannel<IMathService>();

            // have the user enter new values for m_Tests and m_StartNum
            m_Tests = Utilities.ConsoleHelpers.ReadInt("Please enter the number of iterations to perform per test: ", 1, 1000000);

            // Utilities class did not have a BigInteger helper method so I made my own here
            string bigInteger = Utilities.ConsoleHelpers.ReadString("Enter the initial number to use in each loop when calculating square roots: ");
            
            // Makes sure it can parse to a BigInteger before continuing
            while(!BigInteger.TryParse(bigInteger, out m_StartNum))
            {
                // Could not Parse Your Big Integer, Please try again
                Console.WriteLine("Could not parse your big integer, please try again: ");
                bigInteger = Utilities.ConsoleHelpers.ReadString("Enter the initial number to use in each loop when calculating square roots: ");
            } // end of while loop 

            // Now, to make sure both tests are treated “equally”, make a quick call 
            // to the service to “spin it up”. That way when the tests are executed 
            // there is no initial delay. Just call either Sqrt or IsPrime on m_Proxy 
            // with an arbitrary number.
            bool temp = m_Proxy.IsPrime(330);

            // Now, make a call to the TestSync and TestAsync methods 
            // and store their return values in two different TimeSpan objects, 
            // maybe called syncTime and asyncTime, respectively.
            TimeSpan syncTime = TestSync();
            TimeSpan asyncTime = TestAsync();

            // Outputs to the console the TotalSeconds value from 
            // each of the TimeSpan objects with an appropriate header 
            // for each.The user can then see the apparent difference 
            // in performance between the synchronous and asynchronous calls, if any.
            Console.WriteLine(new string('=',40));
            Console.WriteLine("{0,30}","Time Spans in Seconds");
            Console.WriteLine(new string('_', 40));
            Console.WriteLine("{0, 15} {1, 15}", "Synchronous", "Asynchronous");
            Console.WriteLine("{0, 15} {1, 15}", syncTime.TotalSeconds, asyncTime.TotalSeconds);
            Console.WriteLine(new string('=', 40));

            // Wait for the user to want to quit the application
            Console.WriteLine("Press <Enter> to Quit...");
            Console.ReadLine();

        } // end of main method

        /// <summary>
        /// The TestSync method will call the service method IsPrime 
        /// in a synchronous manner. The method accepts no parameters 
        /// and returns a TimeSpan object which will contain 
        /// the total time spent in the method. It iterates for a set
        /// number of times, adding one to the original number and determining
        /// if that number if prime or not. 
        /// </summary>
        /// <returns>(TimeSpan) time elapased for the synchronous test call</returns>
        private static TimeSpan TestSync()
        {
            DateTime start = DateTime.Now;
            for (BigInteger i = m_StartNum; i < m_StartNum + m_Tests; i++)
            {
                bool isPrime = m_Proxy.IsPrime(i);
                if (isPrime)
                {
                    Console.WriteLine("{0} is prime", i);
                }
            }
            DateTime end = DateTime.Now;
            return end.Subtract(start);
        }

        /// <summary>
        /// The TestAsync method will do virtually the same thing as TestSync 
        /// except it will use a Parallel.For loop to call the service method 
        /// from the thread pool.
        /// </summary>
        /// <returns></returns>
        private static TimeSpan TestAsync()
        {
            DateTime start = DateTime.Now;

            // Uses the Thread Pool to process the work
            Parallel.For(0, m_Tests, i => 
            {
                bool isPrime = m_Proxy.IsPrime(m_StartNum + i);
                if (isPrime)
                {
                    Console.WriteLine("{0} is prime", m_StartNum + i);
                }
            });

            DateTime end = DateTime.Now;
            return end.Subtract(start);
        } // end of method

    } // end of class
} // end of namespace
