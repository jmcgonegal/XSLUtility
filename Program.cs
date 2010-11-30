using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XSLUtility
{
    class Program : XSLUtility
    {
        Program(FileInfo xsl)
            : base(xsl.FullName)
        {
            // add this object's public functions as callable by the namespace 'urn:custom'
            this.AddExtensionObject("urn:custom", this);
        }
        /// <summary>
        /// just an example
        /// </summary>
        /// <param name="text">a string to display</param>
        /// <returns>a string passed back to xslt</returns>
        public string CallableFunction(string text)
        {
            // print the xslt argument
            Console.WriteLine(text);

            return "Passed back to XSLT";
        }
        static void Main(string[] args)
        {
            FileInfo xsl = new FileInfo("example.xslt");
            Program prog = new Program(xsl);

            FileInfo output = new FileInfo("output.xml");
            

            // add a prameter
            prog.AddParam("example", "hey there");

            // transform the xml text
            prog.transformXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><root></root>", output);
            
            // another example
            //FileInfo input = new FileInfo("input.xml");
            //prog.transformXmlDocument(input, output);

            Console.WriteLine();
            // show the xml output file
            
            Console.WriteLine(output.Name + " contents:");
            Console.WriteLine(File.ReadAllText(output.FullName));

            Console.WriteLine();
            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();
        }
    }
}
