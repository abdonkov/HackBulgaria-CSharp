using System;
using System.Collections.Generic;
using System.Text;

namespace XmlLibrary
{
    public class XmlMarkupBuilder
    {
        List<StringBuilder> lines;
        Stack<string> openedTagNames;
        Stack<int> openedTagLineNumbers;
        StringBuilder curIndent;
        bool addedFirstTag;
        bool finalized;
        const string tab = "   ";

        public XmlMarkupBuilder()
        {
            lines = new List<StringBuilder>();
            openedTagNames = new Stack<string>();
            openedTagLineNumbers = new Stack<int>();
            curIndent = new StringBuilder();
            addedFirstTag = false;
            finalized = false;
        }

        public XmlMarkupBuilder OpenTag(string tagName)
        {
            if (finalized) throw new XmlMarkupBuilderFinalizedException();
            else if (!addedFirstTag)
            {
                openedTagNames.Push(tagName);
                openedTagLineNumbers.Push(lines.Count);
                lines.Add(new StringBuilder().Append(curIndent).Append("<").Append(tagName));
                addedFirstTag = true;
            }
            else
            {
                if (openedTagNames.Count == 0) throw new XmlMarkupNoRootXmlObjectException();
                else
                {
                    openedTagNames.Push(tagName);
                    openedTagLineNumbers.Push(lines.Count);
                    lines.Add(new StringBuilder().Append(curIndent).Append("<").Append(tagName));
                }
            }

            curIndent.Append(tab);

            return this;
        }

        public XmlMarkupBuilder AddAttr(string attrName, string attrValue)
        {
            if (finalized) throw new XmlMarkupBuilderFinalizedException();
            else if (openedTagNames.Count == 0) throw new XmlMarkupNoOpenedTagException("No opened tag to add attribute!");
            else
            {
                lines[openedTagLineNumbers.Peek()]
                    .Append(" ").Append(attrName).Append("=\"").Append(attrValue).Append("\"");
            }

            return this;
        }

        public XmlMarkupBuilder AddText(string text)
        {
            if (finalized) throw new XmlMarkupBuilderFinalizedException();
            else if (openedTagNames.Count == 0) throw new XmlMarkupNoOpenedTagException();
            else
            {
                lines.Add(new StringBuilder().Append(curIndent).Append(text));
            }
            return this;
        }

        public XmlMarkupBuilder CloseTag()
        {
            if (finalized) throw new XmlMarkupBuilderFinalizedException();
            else if (openedTagNames.Count == 0) throw new XmlMarkupNoOpenedTagException("No opened tag to close!");
            else
            {
                curIndent.Length -= tab.Length;
                lines[openedTagLineNumbers.Pop()].Append(">");
                lines.Add(new StringBuilder()
                    .Append(curIndent).Append("</").Append(openedTagNames.Pop()).Append(">"));
            }

            return this;
        }

        public XmlMarkupBuilder Finalize()
        {
            while(openedTagNames.Count > 0)
            {
                CloseTag();
            }
            finalized = true;
            return this;
        }

        public string GetResult()
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < lines.Count - 1; i++)
            {
                output.Append(lines[i]);
                output.AppendLine();
            }
            output.Append(lines[lines.Count - 1]);
            return output.ToString();
        }
    }
}
