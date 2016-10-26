using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Text.Tokens
{
    [TestClass]
    public class T_Text_Tokens_TokenWriter
    {
        [TestMethod]
        public void KeyValueCollection()
        {
            TokenWriter writer = new TokenWriter("Hello, my name is {Name} and I am from {City}.");
            string result = writer.Format(new KeyValueCollection().Add("Name", "Peter").Add("City", "Prague"));
            Assert.AreEqual("Hello, my name is Peter and I am from Prague.", result);
        }

        [TestMethod]
        public void NameMapper()
        {
            TokenWriter writer = new TokenWriter("Hello, my name is {Name} and I am from {City}.");
            string result = writer.Format(name =>
            {
                if (name == "Name")
                    return "Peter";

                if (name == "City")
                    return "Prague";

                return null;
            });
            Assert.AreEqual("Hello, my name is Peter and I am from Prague.", result);
        }

        [TestMethod]
        public void FullWithAttributes()
        {
            TokenWriter writer = new TokenWriter("Hello, my name is {Name} and I am from {City, Default=Prague}.", true);
            string result = writer.Format(FormatToken);
        }

        private string FormatToken(Token token)
        {
            if (token.Fullname == "Name")
                return "Peter";

            if (token.Fullname == "City")
                return token.Attributes.First(a => a.Name == "Default").Value;

            return null;
        }
    }
}
