using Microsoft.Build.Evaluation;
using System;
using System.Xml;

namespace loadProjectTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const string filepath = @"G:\Practice\Simple\ConsoleApp1\ConsoleApp1\ConsoleApp1.csproj";
            try
            {
                var projCollection = new ProjectCollection();
                Project proj = projCollection.LoadProject(filepath);
                var debugPropertyGroup = proj.Xml.PropertyGroups;
                projCollection.UnloadProject(proj);
            }
            catch (System.Exception)
            {
                ChangeToolVersion(filepath, "4.0");
                LoadProjectFromFile(filepath);
                ChangeToolVersion(filepath, "15.0");
            }
        }

        private static void ChangeToolVersion(string filepath, string v)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNamespaceManager mgr = new XmlNamespaceManager(xmlDoc.NameTable);
            mgr.AddNamespace("vsTool15", "http://schemas.microsoft.com/developer/msbuild/2003");
            xmlDoc.DocumentElement.Attributes[0].Value = v;
            xmlDoc.Save(filepath);
        }

        private static void LoadProjectFromFile(string filepath)
        {
            using (var projCollection = new ProjectCollection())
            {
                Project proj = projCollection.LoadProject(filepath);
                var debugPropertyGroup = proj.Xml.PropertyGroups;
                projCollection.UnloadProject(proj);
            }
        }
    }
}
