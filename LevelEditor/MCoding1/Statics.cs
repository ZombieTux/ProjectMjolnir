using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MCoding1
{
    public static class Statics
    {
        public static class Functions
        {
            public static bool nodeHasAttribute(XmlNode node, string attribute)
            {
                for(int i = 0; i < node.Attributes.Count; i++)
                {
                    XmlNode nodeAttribute = node.Attributes.Item(i);
                    if (nodeAttribute.Name == attribute)
                        return true;
                }

                return false;
            }

        }

    }
}
