using System;
using Microsoft.Owin.Hosting;
using NuSearch.Domain;
using NuSearch.Web.Plumbing;

namespace NuSearch.Web
{
	class Program
	{
		static void Main(string[] args)
		{
			var url = "http://+:8085";

			var options = new StartOptions
			{
				ServerFactory = "Nowin",
				Port = 8085
			};

			using (WebApp.Start<Bootstrap>(options))
			{
				Console.WriteLine("Running on {0}", url);
				Console.WriteLine("Press enter to exit");
				Console.ReadLine();
			}
		}
	}
}
