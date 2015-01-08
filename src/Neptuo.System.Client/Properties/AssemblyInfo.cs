using SharpKit.JavaScript;
using System.Client;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Neptuo.System.Client")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Neptuo.System.Client")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("d1e2d5d1-2de4-4f19-bbce-30e6e5a132bf")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(VersionInfo.Version)]
[assembly: AssemblyInformationalVersion(VersionInfo.Version)]
[assembly: AssemblyFileVersion(VersionInfo.Version)]

[assembly: JsMergedFile(Filename = "res/System.js", Sources = new string[]
{
	"Compilation/JsCompiler.js",
	"Internal/Core.js",
	"Internal/CoreEx.js",
})]

[assembly: JsMergedFile(Filename = "Neptuo.System.Client.js", Sources = new string[]
{
	"res/System.js",
	"res/System.IO.js",
	"res/System.Linq.js",
	"res/System.Collections.js",
	"res/System.Reflection.js",
	"res/System.Diagnostics.js",
	"res/System.ComponentModel.js",
	"res/System.Text.js",
	"res/System.Ext.js"
})]

//[assembly: JsMergedFile(Filename = "../MagicWare.Client.Clr.TestingWeb/res/MagicWare.Client.Clr.js", Sources = new string[] { "Neptuo.System.Client.js" })]
[assembly: JsMergedFile(Filename = "Neptuo.System.Client.min.js", Sources = new string[] { "Neptuo.System.Client.js" }, Minify = true)]
[assembly: JsExport(UseStrict = true)]