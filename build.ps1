$version="1.01"

cd src
#dotnet clean CommodoreBasicReformatter\CommodoreBasicReformatter.csproj
#dotnet build CommodoreBasicReformatter\CommodoreBasicReformatter.csproj

function buildAll([bool] $selfcontained)
{
    rmdir CommodoreBasicReformatter\bin\Debug -Recurse -Force 
	
	if($selfcontained){$arg ="true"}
	else { $arg = "false"}
	"arg="+$arg
	
	# win
	dotnet publish CommodoreBasicReformatter\CommodoreBasicReformatter.csproj -r win-x64 --self-contained $arg 
	dotnet clean CommodoreBasicReformatter\CommodoreBasicReformatter.csproj -r win-x64 

	dotnet publish CommodoreBasicReformatter\CommodoreBasicReformatter.csproj -r win-x86 --self-contained $arg 
	dotnet clean CommodoreBasicReformatter\CommodoreBasicReformatter.csproj -r win-x86 

	# linux
	dotnet publish CommodoreBasicReformatter\CommodoreBasicReformatter.csproj -r linux-x64 --self-contained $arg 
	dotnet clean CommodoreBasicReformatter\CommodoreBasicReformatter.csproj -r linux-x64 
	dotnet publish CommodoreBasicReformatter\CommodoreBasicReformatter.csproj -r linux-x64 --self-contained $arg 
	dotnet clean CommodoreBasicReformatter\CommodoreBasicReformatter.csproj -r linux-arm

	# mac
	dotnet publish CommodoreBasicReformatter\CommodoreBasicReformatter.csproj -r osx-x64 --self-contained $arg 
	dotnet clean CommodoreBasicReformatter\CommodoreBasicReformatter.csproj -r osx-x64 
	
	cd CommodoreBasicReformatter\bin\debug\netcoreapp2.2

	$name=""
	if($selfcontained) {
		$name ="CommodoreBasicReformatter$($version)_selfcontained.zip"
	}
	else {
		$name ="CommodoreBasicReformatter$($version).zip"
	}

	"7zip"
	& 'C:\Program Files\7-Zip\7z.exe' a  -bb0 -bd -tzip $($name) . 

	"Move"
	mv *.zip ..\..\..\..\..
	del *.zip
	"CD"
	cd ..\..\..\..\..	
}

buildAll($False)
# wait for .net core 3 - has singlefile and reducesize flags
#cd src
#buildAll($True)
