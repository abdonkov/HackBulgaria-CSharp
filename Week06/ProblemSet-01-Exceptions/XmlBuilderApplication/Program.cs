using System;
using XmlLibrary;

namespace XmlBuilderApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlMarkupBuilder markupBuilder = new XmlMarkupBuilder();
            markupBuilder.OpenTag("author").AddAttr("name", "Pesho")
                .OpenTag("Book").AddAttr("id", "1").AddAttr("year", "1978")
                .AddText("Book about Cooking")
                .CloseTag()
                .OpenTag("Book").AddAttr("id", "2").AddAttr("year", "2006")
                .AddText("Ala Bala")
                .CloseTag()
                .CloseTag()
                .Finalize();

            Console.WriteLine(markupBuilder.GetResult());
            Console.WriteLine();
            Console.WriteLine("----Exceptions test!----");

            try
            {
                XmlMarkupBuilder markupBuilderExceptionsTest = new XmlMarkupBuilder();
                string validMarkup = markupBuilderExceptionsTest.OpenTag("body").AddAttr("background", "0xFF0000").AddText("Helo HTML!").Finalize().GetResult();
                markupBuilderExceptionsTest.CloseTag(); //BOOOM! Object finalized! Exception!
            }
            catch (XmlMarkupBuilderFinalizedException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                XmlMarkupBuilder markupBuilderExceptionsTest2 = new XmlMarkupBuilder();
                markupBuilderExceptionsTest2.OpenTag("a").CloseTag().OpenTag("a"); //BOOOM! You need to have a root XML object, XML is not a list!
            }
            catch (XmlMarkupNoRootXmlObjectException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                XmlMarkupBuilder markupBuilderExceptionsTest3 = new XmlMarkupBuilder();
                markupBuilderExceptionsTest3.OpenTag("a").CloseTag().CloseTag(); //BOOOM! What the hell are we closing?!
            }
            catch (XmlMarkupNoOpenedTagException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                XmlMarkupBuilder markupBuilderExceptionsTest4 = new XmlMarkupBuilder();
                markupBuilderExceptionsTest4.OpenTag("a").CloseTag().AddAttr("href", "https://www.youtube.com/watch?v=P5ft_7Bcyc4"); //BOOOM! What are you adding attribute to?
            }
            catch (XmlMarkupNoOpenedTagException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
