# Depuración remota con [Visual Studio Code](https://code.visualstudio.com/) contra una Raspberry PI (ARM) usando .NET Core

## Introducción
El propósito de este proyecto es documentar la configuración de Visual Studio Core para desarrollar con dotnet Core contra una Raspberry PI (3 model B+), depurando la ejecución en remoto contra el propio dispositivo RPI, y ya de paso probar las librerías [dotnet/IoT](https://github.com/dotnet/iot):

- [System.Device.Gpio](https://www.nuget.org/packages/System.Device.Gpio)
- [Iot.Device.Bindings](https://www.nuget.org/packages/Iot.Device.Bindings)

## Hardware

- [Raspberry PI 3 Model B+](https://www.amazon.es/Raspberry-Pi-Modelo-Quad-Core-Cortex-A53/dp/B01CD5VC92/ref=sr_1_5?adgrpid=55370211799&gclid=Cj0KCQjwhdTqBRDNARIsABsOl9_XuV-11w1ou5v2E8CJSscBMwfcuj8J_InfQMlVR0yHqltFcVEcIS8aAvjhEALw_wcB&hvadid=275352942953&hvdev=c&hvlocphy=1005548&hvnetw=g&hvpos=1t1&hvqmt=b&hvrand=340737966009266941&hvtargid=aud-611252828140%3Akwd-297141455894&hydadcr=23141_1737717&keywords=raspberry+pi+3+official&qid=1565878317&s=gateway&sr=8-5), con el SO raspbian (yo lo suelo instalar con [noobs](https://www.raspberrypi.org/downloads/noobs/))
- [Pimoroni Explorer HAT Pro](https://www.amazon.es/Pimoroni-PIM082-Explorer-HAT-Pro/dp/B00WWQ20MG/ref=sr_1_2?__mk_es_ES=%C3%85M%C3%85%C5%BD%C3%95%C3%91&keywords=Pimoroni+Explorer+Hat&qid=1565878434&refinements=p_85%3A831314031&rnid=831276031&rps=1&s=gateway&sr=8-2): Esto no es necesario para demostrar la configuración, pero ya lo tenía por casa y simplifica algunas cosillas para informáticos con nulos conocimientos en electrónica como yo.

![image](https://user-images.githubusercontent.com/10654401/63101233-e88c4b80-bf78-11e9-87ff-20e7a2809c40.png)

- Un PC en el que se pueda trabajar con Visual Studio Code. Yo lo he probado con Windows y Linux, pero sospecho que con Mac también debería funcionar


## Preparación de la Raspberry PI

### Instalación del SDK de .NET Core

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
4. Descarga los dos ficheros comprimidos en la raspberry pi
    ```
    wget https://download.visualstudio.microsoft.com/download/pr/3cb1d917-19cc-4399-9a53-03bb5de223f6/be3e011601610d9fe0a4f6b1962378ea/dotnet-sdk-2.2.401-linux-arm.tar.gz
    wget https://download.visualstudio.microsoft.com/download/pr/13798f38-c14e-4944-83c9-4f5b7c535f4d/1e1c3414f3ad791098d1f654640f9bcf/aspnetcore-runtime-2.2.6-linux-arm.tar.gz
    ```
5. Descomprime los dos ficheros en el directorio dotnet del directorio HOME. **Fíjate bien en las versiones si no has bajado las mismas que yo**. Configura también las variables necesarias para que el SO encuentre los binarios de .NET (DOTNET_ROOT y PATH).
    ```
    mkdir -p $HOME/dotnet
    tar zxf dotnet-sdk-2.2.401-linux-arm.tar.gz -C $HOME/dotnet
    tar zxf aspnetcore-runtime-2.2.6-linux-arm.tar.gz -C $HOME/dotnet
    export DOTNET_ROOT=$HOME/dotnet 
    export PATH=$PATH:$HOME/dotnet
    ```
6. Ya debería estar instalado y preparado .Net Core en la Raspberry. Compruebalo ejecutando el comando `dotnet --info` (nótese que tengo instaladas las versiones 2.2 y 2.1 tanto del SDK como del runtime de ASP.NET Core):
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





**-- ESTE TEXTO ESTÁ TODAVÍA EN CONSTRUCCIÓN. ESPERO ACABARLO EN BREVE --**