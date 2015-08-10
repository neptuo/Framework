using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSharpKit.UI.Views;

namespace TestSharpKit.UI
{
    public class Presenter
    {
        private static int counter;
        private readonly MainView view;

        public Presenter(MainView view)
        {
            Ensure.NotNull(view, "view");
            this.view = view;

            counter++;
        }

        public void Render()
        {
            view.Render(counter);
        }
    }
}
