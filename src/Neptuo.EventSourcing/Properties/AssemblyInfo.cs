using Neptuo.EventSourcing;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("Neptuo.EventSourcing")]
[assembly: AssemblyDescription("Event sourcing hosting infrastructure.")]

[assembly: AssemblyVersion(VersionInfo.Version)]
[assembly: AssemblyInformationalVersion(VersionInfo.Version + VersionInfo.Preview)]
[assembly: AssemblyFileVersion(VersionInfo.Version)]

[assembly: InternalsVisibleTo("UnitTest.Services")]