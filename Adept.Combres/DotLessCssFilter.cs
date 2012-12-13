using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Combres;
using dotless.Core;
using dotless.Core.Importers;
using dotless.Core.Input;
using dotless.Core.Parser;
using dotless.Core.Stylizers;

namespace Adept.Combres
{
    public class DotLessCssFilter : ISingleContentFilter
    {
        public string TransformContent(ResourceSet resourceSet, Resource resource, string content)
        {
            var engine = new LessEngine(new Parser(new ConsoleStylizer(), new Importer(new AdeptFileReader(resource.Path))));
            return engine.TransformToCss(content, resource.Path);
        }

        public bool CanApplyTo(ResourceType resourceType)
        {
            return resourceType == ResourceType.CSS;
        }

        public class AdeptFileReader : IFileReader
        {
            private readonly string _startingDirectory;

            public AdeptFileReader(string original)
            {
                _startingDirectory = original.Substring(0, original.LastIndexOf('/') + 1);
            }

            public bool DoesFileExist(string fileName)
            {
                return File.Exists(GetFilePath(fileName));
            }

            public string GetFileContents(string fileName)
            {
                return File.ReadAllText(GetFilePath(fileName));
            }

            public string GetFilePath(string fileName)
            {
                if (fileName.StartsWith("~") || fileName.StartsWith("/"))
                    return HttpContext.Current.Server.MapPath(fileName);
                return HttpContext.Current.Server.MapPath(_startingDirectory + "/" + fileName);
            }

            public byte[] GetBinaryFileContents(string fileName)
            {
                return File.ReadAllBytes(GetFilePath(fileName));
            }
        }
    }
}
