using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml.Linq;

namespace HostingGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var hosting = new Hosting(HostingSettings.DefaultSettings);

            using (var writer = new StreamWriter(@"E:\hosting.xml"))
            {
                writer.WriteLine("<hosting>");
                foreach (Stay stay in hosting.Stays())
                {
                    var xstay =
                        new XElement("reside", new XAttribute("id", stay.Id),
                            new XElement("arrival", stay.Arrival.ToShortDateString()),
                            new XElement("departure", stay.Departure.ToShortDateString())
                        );
                    writer.WriteLine(xstay.ToString());
                }
                writer.Write("</hosting>");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
