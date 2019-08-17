:: dotnet publish -r linux-arm
:: cd bin\Debug\netcoreapp2.2\linux-arm
:: scp -r publish\*.* pi@harlequin.local:/home/pi/Work/dotnet/TestPi.ConsoleApp

dotnet publish -r linux-arm /p:ShowLinkerSizeComparison=true 
pushd .\bin\Debug\netcoreapp2.2\linux-arm\publish
c:\work\putty\pscp -pw harlequin -v -r .\TestPi* pi@harlequin.local:/home/pi/Work/dotnet/TestPi.ConsoleApp
popd