using SharpKit.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSharpKit.UI.Views
{
    public class MainView
    {
        private static int counter;

        public MainView()
        {
            counter++;
        }

        public void Render(int parentCounter)
        {
            HtmlContext.document.body.innerHTML += String.Format("<br /><strong>Hello, World, {0} {1}!</strong>", counter, parentCounter);
        }
    }
}
