..\.nuget\NuGet.exe pack Neptuo.DataAccess.csproj -Prop Configuration=Release
..\.nuget\NuGet.exe push Neptuo.DataAccess.%1.nupkg -Source http://packages.neptuo.com/nuget

..\.nuget\NuGet.exe pack Neptuo.DataAccess.csproj -Prop Configuration=Release -Symbols
..\.nuget\NuGet.exe push Neptuo.DataAccess.%1.symbols.nupkg -Source http://packages.neptuo.com/nuget