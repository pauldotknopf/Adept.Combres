using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Combres;

namespace Adept.Combres
{
    public class CoffeeScriptJavascriptFilter : ISingleContentFilter
    {
        public string TransformContent(ResourceSet resourceSet, Resource resource, string content)
        {
            var extension = Path.GetExtension(resource.Path);
            if (extension != null && !extension.Equals(".coffee", StringComparison.InvariantCultureIgnoreCase))
                return content;

            var engine = new CoffeeSharp.CoffeeScriptEngine();
            return engine.Compile(content, filename: resource.Path);
        }

        public bool CanApplyTo(ResourceType resourceType)
        {
            return resourceType == ResourceType.JS;
        }
    }
}
