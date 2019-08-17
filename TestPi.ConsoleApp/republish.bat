:: dotnet publish -r linux-arm
:: cd bin\Debug\netcoreapp2.2\linux-arm
:: scp -r publish\*.* pi@192.168.203.97:/home/pi/Work/dotnet/TestPi.ConsoleApp

dotnet publish -r linux-arm /p:ShowLinkerSizeComparison=true 
pushd .\bin\Debug\netcoreapp2.2\linux-arm\publish
c:\work\putty\pscp -pw harlequin -v -r .\* pi@192.168.203.97:/home/pi/Work/dotnet/TestPi.ConsoleApp
popd