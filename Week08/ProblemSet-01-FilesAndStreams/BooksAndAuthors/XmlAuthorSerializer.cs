using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BooksAndAuthors
{
    public class XmlAuthorSerializer : IAuthorSerializer
    {
        public void SerializeAuthor(Author author, string filename)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filename))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Author));
                xs.Serialize(sw, author);
            }
        }

        public Author DeserializeAuthor(string filename)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filename))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Author));
                return (Author)xs.Deserialize(sr);
            }
        }
    }
}
