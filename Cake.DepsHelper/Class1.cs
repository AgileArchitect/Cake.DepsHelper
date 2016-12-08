using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.DepsHelper
{
    public static class DepsHelperAliases
    {
        public static IEnumerable<Dependency> PackageDependencies(this ICakeContext context, FilePath path)
        {
            System.Xml.XmlDocument packages = new System.Xml.XmlDocument();
            var lines = System.IO.File.ReadAllLines(path.FullPath);
            var content = string.Join(Environment.NewLine, lines);
            content = content.Replace("utf-8", "utf-16");
            packages.LoadXml(content);
            if (packages.DocumentElement == null)
                return new Dependency[] { };

            var dependencies = packages.DocumentElement.SelectNodes("/packages/package").Cast<System.Xml.XmlNode>().Select(x => new Dependency { Name = x.Attributes["id"].Value, Version = x.Attributes["version"].Value }).ToList();
            return dependencies;
        }
    }

    public class Dependency
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }
}
