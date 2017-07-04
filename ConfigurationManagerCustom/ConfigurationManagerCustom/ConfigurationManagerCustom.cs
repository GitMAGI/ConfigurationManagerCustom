using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public sealed class ConfigurationManagerCustom
{
    public ConfigurationManagerCustom(string filename)
    {
        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        try
        {
            doc.Load(filename);
        }
        catch (Exception)
        {
            throw;
        }

        try
        {
            System.Xml.XmlNodeList nodes = doc.DocumentElement.SelectNodes("/configuration/appSettings/add");
            if (nodes == null)
                throw new NullReferenceException(string.Format("No Add Nodes into Configuration file '{0}'", filename));

            Dictionary<string, string> tmp = new Dictionary<string, string>();

            foreach (System.Xml.XmlNode node in nodes)
            {
                if (tmp == null)
                    tmp = new Dictionary<string, string>();
                tmp.Add(node.Attributes["key"].InnerText, node.Attributes["value"].InnerText);
            }

            if (tmp != null)
                this.AppSettings = new ReadOnlyDictionary<string, string>(tmp);

        }
        catch (Exception)
        {
            throw;
        }

    }

    public ReadOnlyDictionary<string, string> AppSettings;
}
