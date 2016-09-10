using Neptuo.EventSourcing.Domains;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("Neptuo.EventSourcing.Domains")]
[assembly: AssemblyDescription("Domain classes for implementing EventSourcing and CQRS.")]

#if !DEBUG

Fix version attribute!

#endif

[assembly: AssemblyVersion(VersionInfo.Version)]
[assembly: AssemblyInformationalVersion(VersionInfo.Version + "-beta1")]
[assembly: AssemblyFileVersion(VersionInfo.Version)]

[assembly: InternalsVisibleTo("Neptuo.EventSourcing")]