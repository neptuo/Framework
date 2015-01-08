..\.nuget\NuGet.exe pack Neptuo.DataAccess.EntityFramework.csproj -Prop Configuration=Release
..\.nuget\NuGet.exe push Neptuo.DataAccess.EntityFramework.%1.nupkg -Source http://packages.neptuo.com/nuget
del Neptuo.DataAccess.EntityFramework.%1.nupkg

..\.nuget\NuGet.exe pack Neptuo.DataAccess.EntityFramework.csproj -Prop Configuration=Release -Symbols
..\.nuget\NuGet.exe push Neptuo.DataAccess.EntityFramework.%1.symbols.nupkg -Source http://packages.neptuo.com/nuget
del Neptuo.DataAccess.EntityFramework.%1.symbols.nupkg