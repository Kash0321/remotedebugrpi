# Instalación del SDK de .NET Core

[Volver al readme.md](/readme.md)

He extraído los pasos de [este post de Scott Hanselman](https://www.hanselman.com/blog/InstallingTheNETCore2xSDKOnARaspberryPiAndBlinkingAnLEDWithSystemDeviceGpio.aspx), los recopilo a continuación:

1. Instalar algunos paquetes necesarios (requisitos previos, una sola vez)
    ```
    sudo apt-get install curl libunwind8 gettext
    ```
2. Navegar a la siguiente URL: [https://dotnet.microsoft.com/download/dotnet-core/2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2)
    * Observa que puedes trabajar con versiones anteriores (o futuras) de NET Core tan solo modificando la parte final de la URL (/2.1, /2.0...)
3. Accede a los links de descarga de los binarios para ARM32 para linux del SDK y del runtime de ASP.NET Core (este último no es necesario si no vas a desarrollar aplicaciones WEB/WEB API), de la versión específica que quieras instalar...
    ![image](https://user-images.githubusercontent.com/10654401/63222589-45bb1380-c1aa-11e9-8e8f-d5336754777f.png)
4. Para cada uno de ellos, aterrizarás en una página como esta, en la que debes copiar el enlace con el texto **_"click here to download manually"_**
    ![image](https://user-images.githubusercontent.com/10654401/63222713-8bc4a700-c1ab-11e9-9b4a-1fabd4b21771.png)
5. Descarga los dos ficheros comprimidos en la raspberry pi
    ```
    wget https://download.visualstudio.microsoft.com/download/pr/3cb1d917-19cc-4399-9a53-03bb5de223f6/be3e011601610d9fe0a4f6b1962378ea/dotnet-sdk-2.2.401-linux-arm.tar.gz
    wget https://download.visualstudio.microsoft.com/download/pr/13798f38-c14e-4944-83c9-4f5b7c535f4d/1e1c3414f3ad791098d1f654640f9bcf/aspnetcore-runtime-2.2.6-linux-arm.tar.gz
    ```
6. Descomprime los dos ficheros en el directorio dotnet del directorio HOME. **Fíjate bien en las versiones si no has bajado las mismas que yo**. Configura también las variables necesarias para que el SO encuentre los binarios de .NET (DOTNET_ROOT y PATH).
    ```
    mkdir -p $HOME/dotnet
    tar zxf dotnet-sdk-2.2.401-linux-arm.tar.gz -C $HOME/dotnet
    tar zxf aspnetcore-runtime-2.2.6-linux-arm.tar.gz -C $HOME/dotnet
    export DOTNET_ROOT=$HOME/dotnet 
    export PATH=$PATH:$HOME/dotnet
    ```
7. Ya debería estar instalado y preparado .Net Core en la Raspberry. Compruebalo ejecutando el comando `dotnet --info` (nótese que tengo instaladas las versiones 2.2 y 2.1 tanto del SDK como del runtime de ASP.NET Core):
    ```
    pi@harlequin:~ $ dotnet --info
    .NET Core SDK (reflecting any global.json):
    Version:   2.2.401
    Commit:    729b316c13

    Runtime Environment:
    OS Name:     raspbian
    OS Version:  9
    OS Platform: Linux
    RID:         linux-arm
    Base Path:   /home/pi/dotnet/sdk/2.2.401/

    Host (useful for support):
    Version: 2.2.6
    Commit:  7dac9b1b51

    .NET Core SDKs installed:
    2.1.801 [/home/pi/dotnet/sdk]
    2.2.401 [/home/pi/dotnet/sdk]

    .NET Core runtimes installed:
    Microsoft.AspNetCore.All 2.1.12 [/home/pi/dotnet/shared/Microsoft.AspNetCore.All]
    Microsoft.AspNetCore.All 2.2.6 [/home/pi/dotnet/shared/Microsoft.AspNetCore.All]
    Microsoft.AspNetCore.App 2.1.12 [/home/pi/dotnet/shared/Microsoft.AspNetCore.App]
    Microsoft.AspNetCore.App 2.2.6 [/home/pi/dotnet/shared/Microsoft.AspNetCore.App]
    Microsoft.NETCore.App 2.1.12 [/home/pi/dotnet/shared/Microsoft.NETCore.App]
    Microsoft.NETCore.App 2.2.6 [/home/pi/dotnet/shared/Microsoft.NETCore.App]

    To install additional .NET Core runtimes or SDKs:
    https://aka.ms/dotnet-download
    ```

    [Volver al readme.md](/readme.md)