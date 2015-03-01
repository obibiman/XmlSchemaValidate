using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Config;

namespace XmlVerifyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            var rd = new ReadData();
            rd.ValidateData();
        }
    }
}
