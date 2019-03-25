using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.ServiceModel;

namespace SharedLib
{
    /// <summary>
    /// Service contract for the MathService
    /// </summary>
    [ServiceContract]
    public interface IMathService
    {
        /// <summary>
        /// IsPrime
        /// Determines if a number is prime. 
        /// Works with very large integers 
        /// </summary>
        /// <param name="number">(BigInteger) number to determine if prime</param>
        /// <returns>(bool) true if a prime number, else false</returns>
        [OperationContract]
        bool IsPrime(BigInteger number);

        /// <summary>
        /// Sqrt
        /// Determines the square root of a number
        /// Works with large integers
        /// </summary>
        /// <param name="number">(BigInteger) number to determine square root</param>
        /// <returns>(BigInteger) number of the square root</returns>
        [OperationContract]
        BigInteger Sqrt(BigInteger number);

    } // end of interface
} // end of namespace
