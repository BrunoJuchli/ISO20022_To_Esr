using System.Xml.Linq;

namespace VESR.ISO20022
{
    public static class XObjectExtensions
    {
        public static XName ResolveName(this XObject xObj, XName name)
        {
            if (string.IsNullOrEmpty(name.NamespaceName))
            {
                name = xObj.Document.Root.GetDefaultNamespace() + name.LocalName;
            }
            return name;
        }
    }
}
