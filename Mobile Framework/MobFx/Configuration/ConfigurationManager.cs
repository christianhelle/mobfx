using System.Collections.Specialized;
using System.IO;
using System.Xml;
using ChristianHelle.Framework.WindowsMobile.IO;
using System;

namespace ChristianHelle.Framework.WindowsMobile.Configuration
{
    /// <summary>
    /// Provides support for reading AppSettings and ConnectionStrings from 
    /// application configuration file.
    /// </summary>
    public static class ConfigurationManager
    {
        /// <summary>
        /// Gets the current application's default configuration
        /// </summary>
        public static NameValueCollection AppSettings { get; private set; }

        /// <summary>
        /// Gets the connection string used to establish a connection to a data source. 
        /// </summary>
        public static NameValueCollection ConnectionStrings { get; private set; }

        static ConfigurationManager()
        {
            Refresh();
        }

        /// <summary>
        /// Re-reads from the application configuration file
        /// </summary>
        public static void Refresh()
        {
            var xmlDocument = LoadXmlDocument();
            AppSettings = ParseSection(xmlDocument, "appSettings", "key", "value");
            ConnectionStrings = ParseSection(xmlDocument, "connectionStrings", "name", "connectionString");
        }

        private static XmlDocument LoadXmlDocument()
        {
            var configFile = Path.Combine(DirectoryEx.GetCurrentDirectory(), "app.config");
            if (!File.Exists(configFile))
                configFile = FileEx.GetExecutablePath() + ".config";
            if (!File.Exists(configFile))
                throw new MobileApplicationException("App.Config file does not exist");

            var xmlDocument = new XmlDocument();
            using (var reader = new StreamReader(configFile))
                xmlDocument.LoadXml(reader.ReadToEnd());
            return xmlDocument;
        }

        private static NameValueCollection ParseSection(XmlDocument xmlDocument, string sectionName, string key, string value)
        {
            var nameValueCollection = new NameValueCollection();
            var oList = xmlDocument.GetElementsByTagName(sectionName);

            foreach (XmlNode oNode in oList)
                foreach (XmlNode oKey in oNode.ChildNodes)
                    nameValueCollection.Add(
                        oKey.Attributes[key].Value,
                        oKey.Attributes[value].Value);

            return nameValueCollection;
        }
    }
}
