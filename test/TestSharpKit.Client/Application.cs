using Neptuo.Activators;
using SharpKit.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSharpKit.UI;
using TestSharpKit.UI.Views;

public class Application
{
    public static void Start()
    {
        //HtmlContext.alert("Hell, World!");

        IDependencyContainer dependencyContainer = new SimpleDependencyContainer();
        dependencyContainer.Definitions
            .AddTransient<Presenter>()
            .AddScoped<MainView>();

        Presenter p1 = dependencyContainer.Resolve<Presenter>();
        p1.Render();

        Presenter p2 = dependencyContainer.Resolve<Presenter>();
        p2.Render();
    }
}
