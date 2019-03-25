# WCFMathServiceLibraryMultiThreadedCapableWithConsoleServiceHost&ClientTester

WCF Math Service Library with Multi-Threaded Capabilities & Console Service Host and a Client “Tester” ChannelFactory Implementation

Project Blog Article here: 
https://portfolio.katiegirl.net/2019/03/25/wcf-math-service-library-with-multi-threaded-capabilities-console-service-host-and-a-client-tester-channelfactory-implementation/


About

This project presents a WCF Service Library (MathServiceLib) that consists of a math library that contains complex, long-running methods. This WCF Math Service and Client demonstrates multi-threaded access with various combinations of instance context modes and concurrency levels. The client accesses these methods using sequential and multi-threaded techniques to demonstrate the difference in performance.
The service is hosted via a console application (MathServiceHost). The client (MathClient) uses the ChannelFactory pattern as opposed to “Add Service Reference” with SVCUTIL. The client and service share a common assembly (SharedLib) that contains the key contract and data model information. 
Furthermore, a Utilities project is used by the client console application to facilitate user data entry and the complicated details of building and managing the WCF ChannelFactory connection implementation. The ProxyGen class inside the Utilities project abstracts the details of implementing and managing a generic ChannelFactory connection to a generic service for a client. Note: The Utilities project library was included as base code for my lab project to facilitate speedy completion; we were not expected to code this Utilities project ourselves due to complexity and time constraints. The remaining projects in the solution (SharedLib, MathClient, MathServiceHost, and MathServiceLib), I completed individually per requirements for the lab project.


Architecture

The MathService Visual Studio solution application consists of five project assemblies:

•	SharedLib (Class Library) “Service and Data Contracts”
•	MathServiceLib (WCF Service Library) “Implements the Service”
•	MathServiceHost (Console Application) “Hosts the Service”
•	MathClient (Console Application) “Client Tester to Service”
•	Utilities (Class Library) “Helper classes”


