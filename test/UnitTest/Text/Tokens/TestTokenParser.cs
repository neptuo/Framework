using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Text.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Text.Tokens
{
    [TestClass]
    public class TestTokenParser
    {
        [TestMethod]
        public void WithParameter_Single()
        {
            ParseTemplate("{ProductID ID=5}", true);
        }

        [TestMethod]
        public void WithParameter_Inner()
        {
            ParseTemplate("{ProductID ID=5, Converter={x:State NullToBool, AppendOnly}}", true);
        }

        [TestMethod]
        public void WithParameter_Multi()
        {
            ParseTemplate("{ProductID ID=5}{DestinationID}", true);
        }

        [TestMethod]
        public void WithParameter_Multi_WithSeparator()
        {
            ParseTemplate("{ProductID ID=5}xx{DestinationID}", true);
        }

        [TestMethod]
        public void SuperComplex()
        {
            ParseTemplate("{ProductID ID=5, Converter={x:State NullToBool, AppendOnly}}x{DestinationID}", true);
        }

        [TestMethod]
        public void MultiLine()
        {
            string template = @"asdkja sldkjas ldkajsd lkajsd 
as
as{ProductID ID=5} askdh aksjdh alskdj asldkj alskdj.";

            ParseTemplate(template, true);
        }

        [TestMethod]
        public void Url()
        {
            ParseTemplate("http://localhost:60000/{Locale}/{DestinationID}/products/{ProductID}/info", true);
        }

        private static void ParseTemplate(string template, bool result)
        {
            TokenParser parser = new TokenParser();
            parser.Configuration.AllowTextContent = true;
            parser.Configuration.AllowMultipleTokens = true;
            parser.Configuration.AllowAttributes = true;
            parser.Configuration.AllowDefaultAttributes = true;
            parser.Configuration.AllowEscapeSequence = false;

            parser.OnParsedToken += OnToken;

            Assert.AreEqual(result, parser.Parse(template));
        }

        static void OnToken(object sender, TokenEventArgs e)
        {
            Console.WriteLine("Starting at {0}, to {1}, source.substring: {2}", e.StartPosition, e.EndPosition, e.OriginalContent.Substring(e.StartPosition, e.EndPosition - e.StartPosition));
            Console.WriteLine(e.Token.Fullname);
            PrintLineInfo(e.Token);
            Console.WriteLine(String.Join(";", e.Token.DefaultAttributes));
            Console.WriteLine(String.Join(";", e.Token.Attributes.Select(a => String.Format("{0}:{1}", a.Name, a.Value))));
            Console.WriteLine("---");
        }

        static void PrintLineInfo(IDocumentSpan lineInfo)
        {
            Console.WriteLine("{0}:{1} -> {2}:{3}", lineInfo.LineIndex, lineInfo.ColumnIndex, lineInfo.EndLineIndex, lineInfo.EndColumnIndex);
        }
    }
}
