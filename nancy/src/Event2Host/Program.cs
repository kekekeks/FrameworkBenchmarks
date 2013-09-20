using System;
using System.Configuration;
using Nancy.Hosting.Event2;
using Nancy;
using System.Threading;

namespace Event2Host
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string host = args [0];
			int port = int.Parse(args [1]);
			string dbHost = args [2];
			LibLocator.Init ();
			NancyBenchmark.DbModule.MYSQL_CONNECTION_STRING = "server=localhost;user id=benchmarkdbuser;password=benchmarkdbpass;database=hello_world"
			                                                  .Replace ("localhost", dbHost);
			AddToIndex ();
			var threads = 20 * Environment.ProcessorCount;
			ThreadPool.SetMaxThreads(threads, threads);
			ThreadPool.SetMinThreads(threads, threads);
			new NancyEvent2Host (host, port, new DefaultNancyBootstrapper ()).Start ();

		}

		static void AddToIndex ()
		{
			Nancy.Bootstrapper.AppDomainAssemblyTypeScanner
				.AddAssembliesToScan (typeof(NancyBenchmark.JsonModule).Assembly);
		}
	}
}
