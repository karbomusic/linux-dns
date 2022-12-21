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
                        Console.WriteLine("{0}: Running {1} queries for {2} with no delay...\r\n", DateTime.Now.ToLongTimeString(), iterations, domain);

                        for (int i = 0; i < iterations; i++)
                        {
                            Console.Write("{0}: Iteration: {1} ", DateTime.Now.ToLongTimeString(), i);
                            doDnsQuery(domain);
                        }
                        break;

                    case 3:  // loop per arg values + delay
                        domain = args[0];
                        iterations = Int32.Parse(args[1]);
                        delay = Int32.Parse(args[2]);

                        Console.WriteLine("{0}: Running {1} queries for {2} with {3}ms delay...\r\n", DateTime.Now.ToLongTimeString(), iterations, domain, delay);
                        for (int i = 0; i < iterations; i++)
                        {
                            Console.Write("{0}: Iteration: {1} waiting {2} ms per interation.", DateTime.Now.ToLongTimeString(), i, delay);
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
                // something went wrong

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
            IPHostEntry result = System.Net.Dns.GetHostEntry(hostname);
            foreach (IPAddress addr in result.AddressList)
            {
                Console.WriteLine("RR: {0}", addr);
            }

        }
    }
}
