using System.Text;

namespace Carmarket.Infrastructure.Network.Contents
{
    public class XmlContent : StringContent
    {
        public XmlContent(string content)
            : this(content, Encoding.UTF8)
        {
        }

        public XmlContent(string content, Encoding encoding)
            : base(content, encoding, "application/xml")
        {
        }
    }

}
