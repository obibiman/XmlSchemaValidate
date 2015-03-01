using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml.Schema;
using System.Xml.Linq;
using System.Xml;
using log4net;
using log4net.Config;

namespace XmlVerifyConsole
{
    public class ReadData
    {        
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ReadData()
        {
            XmlConfigurator.Configure();
        }

        private readonly string _xmlFile = ConfigurationManager.AppSettings["xmlFile"];
        private readonly string _xsdFile = ConfigurationManager.AppSettings["xsdFile"];

        public void ValidateData()
        {
            if (!File.Exists(_xmlFile))
                return;
            var xmlDoc = XDocument.Load(_xmlFile,LoadOptions.SetLineInfo);

            if (!File.Exists(_xsdFile))
                return;
            var xsdDoc = XDocument.Load(_xsdFile);

            var schemas = new XmlSchemaSet();
            schemas.Add("", XmlReader.Create(new StringReader(xsdDoc.ToString())));

            xmlDoc.Validate(schemas, OnValidationEventHandler);
            //Console.ReadLine();
        }

        private static void OnValidationEventHandler(object o, ValidationEventArgs args)
        {
            var errMsg = String.Format("<{0}> Occurred on line: {1}, position: {2}", args.Message, args.Exception.LineNumber, args.Exception.LinePosition);

            switch (args.Severity)
            {
                case XmlSeverityType.Error:
                    Log.Info(errMsg);
                    break;
                case XmlSeverityType.Warning:
                    Log.Info(errMsg);
                    break;
            }
        }
    }
}
