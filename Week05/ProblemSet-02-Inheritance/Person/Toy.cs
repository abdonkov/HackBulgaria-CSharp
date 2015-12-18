using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    class Toy
    {
        public string Type { get; private set; }
        public string Color { get; private set; }
        public string Size { get; private set; }

        public Toy(string type, string color, string size)
        {
            Type = type;
            Color = color;
            Size = size;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Type: ");
            sb.AppendLine(Type);
            sb.Append("Color: ");
            sb.AppendLine(Color);
            sb.Append("Size: ");
            sb.AppendLine(Size);

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
