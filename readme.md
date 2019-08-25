# Depuración remota con [Visual Studio Code](https://code.visualstudio.com/) contra una Raspberry PI (ARM) usando .NET Core

## Introducción
El propósito de este proyecto es documentar la configuración de Visual Studio Core para desarrollar con dotnet Core contra una Raspberry PI (3 model B+), depurando la ejecución en remoto contra el propio dispositivo RPI, y ya de paso probar las librerías [dotnet/IoT](https://github.com/dotnet/iot):

- [System.Device.Gpio](https://www.nuget.org/packages/System.Device.Gpio)
- [Iot.Device.Bindings](https://www.nuget.org/packages/Iot.Device.Bindings)

## Hardware

El hardware que he usado para el experimento es el siguiente:

- Un PC en el que se pueda trabajar con Visual Studio Code. Yo lo he probado con Windows y Linux, pero sospecho que con Mac también debería funcionar
- [Raspberry PI 3 Model B+](https://www.amazon.es/Raspberry-Pi-Modelo-Quad-Core-Cortex-A53/dp/B01CD5VC92/ref=sr_1_5?adgrpid=55370211799&gclid=Cj0KCQjwhdTqBRDNARIsABsOl9_XuV-11w1ou5v2E8CJSscBMwfcuj8J_InfQMlVR0yHqltFcVEcIS8aAvjhEALw_wcB&hvadid=275352942953&hvdev=c&hvlocphy=1005548&hvnetw=g&hvpos=1t1&hvqmt=b&hvrand=340737966009266941&hvtargid=aud-611252828140%3Akwd-297141455894&hydadcr=23141_1737717&keywords=raspberry+pi+3+official&qid=1565878317&s=gateway&sr=8-5), con el SO raspbian (yo lo suelo instalar con [noobs](https://www.raspberrypi.org/downloads/noobs/))
- [Pimoroni Explorer HAT Pro](https://www.amazon.es/Pimoroni-PIM082-Explorer-HAT-Pro/dp/B00WWQ20MG/ref=sr_1_2?__mk_es_ES=%C3%85M%C3%85%C5%BD%C3%95%C3%91&keywords=Pimoroni+Explorer+Hat&qid=1565878434&refinements=p_85%3A831314031&rnid=831276031&rps=1&s=gateway&sr=8-2): Esto no es necesario para demostrar la configuración, pero ya lo tenía por casa y simplifica algunas cosillas para informáticos con nulos conocimientos en electrónica como yo.

<div style="text-align: center;">
    <img alt="Raspberry Pi + Pimoroni Explorer HAT Pro" src="https://user-images.githubusercontent.com/10654401/63101233-e88c4b80-bf78-11e9-87ff-20e7a2809c40.png" width="600px" />
</div>

## Manos a la obra

- [Instalación del SDK de .NET Core en la Raspberry Pi](/doc/netcore-install.md)
- [Configuración de la depuración remota](/doc/remote-debugging.md)