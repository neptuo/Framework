using Neptuo.ComponentModel;
using Neptuo.Text.Positions;
using Neptuo.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Tokens
{
    static class TestTokens
    {
        public static void Test()
        {
            //string template = "{ProductID ID=5, Converter={x:State NullToBool, AppendOnly}}x{DestinationID}";
            //string template = "{ProductID ID=5, Converter={x:State NullToBool, AppendOnly}}{DestinationID}";
            string template = @"asdkja sldkjas ldkajsd lkajsd 
as
as{ProductID ID=5} askdh aksjdh alskdj asldkj alskdj.";
            //string template = "http://localhost:60000/{Locale}/{DestinationID}/products/{ProductID}/info";

            TokenParser parser = new TokenParser();
            parser.Configuration.AllowTextContent = true;
            parser.Configuration.AllowMultipleTokens = true;
            parser.Configuration.AllowAttributes = true;
            parser.Configuration.AllowDefaultAttributes = true;
            parser.Configuration.AllowEscapeSequence = false;

            parser.OnParsedToken += OnToken;
            if (parser.Parse(template))
                Console.WriteLine("=> Parsed");
            else
                Console.WriteLine("=> Not parsed");
        }

        static void OnToken(object sender, TokenEventArgs e)
        {
            Console.WriteLine("Starting at {0}, to {1}, source.substring: {2}", e.StartPosition, e.EndPosition, e.OriginalContent.Substring(e.StartPosition, e.EndPosition - e.StartPosition + 1));
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
