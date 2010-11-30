using System;
using System.Text;
using System.Xml.Xsl;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace XSLUtility
{
    class XSLUtility : XsltArgumentList
    {
        private XslCompiledTransform xsl;
        private XsltSettings settings = new XsltSettings(true, true);
        private XmlResolver resolver = new XmlUrlResolver();

        // byte order mark for utf8
        private bool bom = false;

        /// <summary>
        /// XSLUtility is a reusable helper utility to reduce the complexity xslt operations.
        /// </summary>
        /// <param name="xsl_file">the xslt file this utility should use</param>
        public XSLUtility(string xsl_file)
        {
            // load xsl
            xsl = new XslCompiledTransform(true);
            xsl.Load(xsl_file, settings, resolver);
        }

        /// <summary>
        /// passes a parameter to the xsl file
        /// </summary>
        /// <param name="name">name of parameter</param>
        /// <param name="text">value to pass</param>
        public void AddParam(string name, string text)
        {
            this.AddParam(name, "", text);
        }

        /// <summary>
        /// transforms an xml document using an xslt file to another xml document, xslt file set with constructor
        /// </summary>
        /// <param name="document">path to xml file</param>
        /// <param name="output_file">path to xml output file</param>
        public void transformXmlDocument(string document, string output_file)
        {
            // load the word document
            FileInfo file = new FileInfo(document);
            FileInfo output = new FileInfo(output_file);
            this.transformXmlDocument(file, output);
        }

        /// <summary>
        /// transforms an xml document using an xslt file to another xml document, xslt file set with constructor
        /// </summary>
        /// <param name="document">xml file to transform</param>
        /// <param name="output_file">file to output to</param>
        public void transformXmlDocument(FileInfo document, FileInfo output_file)
        {
            XPathDocument doc = new XPathDocument(document.FullName);
            this.transform(doc, output_file);
        }

        /// <summary>
        /// transform xml text using xslt to an output file
        /// </summary>
        /// <param name="xmltext"></param>
        /// <param name="output"></param>
        public void transformXml(string xmltext, FileInfo output)
        {
            XmlDocument xdoc = new XmlDocument();
            XPathDocument doc = new XPathDocument(new XmlNodeReader(xdoc));

            // load the text
            xdoc.LoadXml(xmltext);
            this.transform(doc, output);
        }

        /// <summary>
        /// transform xml text using xslt to an output file
        /// </summary>
        /// <param name="xmltext"></param>
        /// <param name="output"></param>
        public void transformXml(string xmltext, string output_file)
        {
            FileInfo output = new FileInfo(output_file);
            this.transformXml(xmltext, output);
        }

        private void transform(XPathDocument doc, FileInfo output)
        {
            // use the settings from the xsl file to write
            XmlWriterSettings writerSettings = xsl.OutputSettings.Clone();

            // some of the SCORM LMSs freak if the byte order mark is included, false disables the BOM
            writerSettings.Encoding = new UTF8Encoding(bom);

            XmlWriter writer = XmlWriter.Create(output.FullName, writerSettings);

            xsl.Transform(doc, this, writer);

            writer.Close();
        }
    }
}
