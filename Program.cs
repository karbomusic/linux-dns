using System;
using System.Net;
using System.Net.Http.Headers;

namespace LinuxDnsQuery
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string domain;
            int iterations;
            int delay;

        retry:
            try
            {
                switch (args.Length)
                {
                    case 0: // no args
                        doDnsQuery("yahoo.com");
                        break;

                    case 1: // just the hostname is expected
                        doDnsQuery(args[0]);
                        break;

                    case 2: // loop per arg values
                        domain = args[0];
                        iterations = Int32.Parse(args[1]);
                        Console.WriteLine("Running {0} queries for {1} with no delay...\r\n", iterations, domain);

                        for (int i = 0; i < iterations; i++)
                        {
                            Console.Write("Iteration: {0} ", i+1);
                            doDnsQuery(domain);
                        }
                        break;

                    case 3:  // loop per arg values + delay
                        domain = args[0];
                        iterations = Int32.Parse(args[1]);
                        delay = Int32.Parse(args[2]);

                        Console.WriteLine("Running {0} queries for {1} with {2}ms delay...\r\n", iterations, domain, delay);
                        for (int i = 0; i < iterations; i++)
                        {
                            Console.Write("Iteration: {0} waiting {1} ms per interation.", i+1, delay);
                            doDnsQuery(domain);
                            System.Threading.Thread.Sleep(delay);
                        }
                        break;

                    default: // no matching arg pattern, list usage
                        Console.WriteLine("Usage: LinuxDnsQuery with No args = yahoo.com with manual retry\r\nLinuxDnsQuery <hostname>\r\n LinuxDnsQuery <hostname iterations>\r\n LinuxDnsQuery <hostname> <iterations> <delay>");
                        break;
                }
            }
            catch (Exception e)
            {
                // something went wrong. If we reproduce the issue,
                // the code will fall into this catch statement.
                Console.WriteLine(e.ToString());
            }

            // Test is complete, ask user if they wish to run again
            Console.Write("Test complete. Run again? (y/n):");
            if (Console.ReadKey().Key.ToString().ToLower() == "y")
            {
                Console.WriteLine();
                goto retry;
            }
        }

        static void doDnsQuery(string hostname = "")
        {
            if (hostname == "")
            {
                hostname = "yahoo.com";
            }

            Console.WriteLine("Running dns query for {0}", hostname);
            IPHostEntry result = System.Net.Dns.GetHostByName(hostname);
            foreach (IPAddress addr in result.AddressList)
            {
                Console.WriteLine("RR: {0}", addr);
            }
        }
    }
}
