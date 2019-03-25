using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib;
using System.Numerics;
using System.ServiceModel;

namespace MathServiceLib
{
    /// <summary>
    /// MathServiceLib
    /// Implements the IMathService interface methods.
    /// The service consists of a math library 
    /// that contains complex, long-running methods.
    /// WCF Service Library project
    /// </summary>
    // The InstanceContextMode ensures EACH client call will receive a new service instance
    // Concurrency Mode =  Single is for Synchronous Access, Multiple = Multi-threading
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
    public class MathService : IMathService
    {
        /// <summary>
        /// The IsPrime method will use a brute force (read: slow) 
        /// algorithm to determine if a number is prime. There are 
        /// some optimizations built in, such as limiting the test 
        /// to odd numbers that are not divisible by 5 up to the 
        /// square root of the given value.
        /// </summary>
        /// <param name="value">(BigInteger) number to be evaluated</param>
        /// <returns>(bool) true if result is a prime number, false otherwise</returns>
        public bool IsPrime(BigInteger value)
        {
            if (value <= 1)
            {
                // Eliminate 0 & 1 
                return false;
            }

            else if ((value <= 3) || (value == 5))
            {
                // 2, 3 & 5 are prime 
                return true;
            }

            else if (value.IsEven)
            {
                // Even numbers are not prime 
                return false;
            }

            // Check for numbers divisible by 5 
            if (value % 5 == 0)
            {
                // Any multiple of 5 (except 5) is not prime 
                return false;
            }

            // If n is a positive composite integer, then n has a prime divisor 
            // less than or equal to sqrt(n) 
            BigInteger max = Sqrt(value);
            int counter = 3;

            for (BigInteger i = 3; i <= max; i += 2)
            {
                if (counter == 4)
                {
                    counter = 0; continue;
                }

                counter++;

                if (value % i == 0)
                {
                    // Found a factor, not prime 
                    return false;
                }
            } 
            
            // No factors found 
            return true;

        } // end of method

        /// <summary>
        /// The Sqrt method will calculate the approximate 
        /// square root for an arbitrarily large integer 
        /// value. By the way, that is what BigInteger 
        /// provides – a data structure to hold numbers 
        /// far larger that can be stored in traditional 
        /// data types. Since the method uses the Math 
        /// library, it is only going to be as precise 
        /// as a double.
        /// </summary>
        /// <param name="number">(Big Integer) number to determine approximate square root</param>
        /// <returns>(BigInteger) number of the approximate square root</returns>
        public BigInteger Sqrt(BigInteger number)
        {
            return (BigInteger)Math.Exp(BigInteger.Log(number) / 2);
        } // end of method

    } // end of class
} // end of namespace
