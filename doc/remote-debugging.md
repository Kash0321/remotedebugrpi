# Depuración remota

[Volver al readme.md](/readme.md)

Los pasos para poner en marcha la depuración remota los seguí [desde este otro gran post de Scott Hanselman](https://www.hanselman.com/blog/RemoteDebuggingWithVSCodeOnWindowsToARaspberryPiUsingNETCoreOnARM.aspx), así que de nuevo el mérito de este post es traducirlos ...

## En la Raspberry Pi

Vamos a conectamos por SSH a la Raspberry, y para eso, si no lo hemos hecho ya, hemos de activar esta funcionalidad:

- Conectando la raspberry a un teclado, ratón y monitor, la forma más visual y cómoda es acceder a las preferencias del sistema y abrir la aplicación de configuración:

    ![RaspiSettingsSSH1](https://user-images.githubusercontent.com/10654401/63646717-7275aa80-c717-11e9-9e61-4c6dfcec3cac.png)

    ![RaspiSettingsSSH2](https://user-images.githubusercontent.com/10654401/63646718-75709b00-c717-11e9-8c95-79a5b92663fc.png)

- Tenemos una alternativa más linuxera desde la terminal, ejecutando en ella el siguiente comando y usando la aplicación correspondiente:

    ```
    sudo raspi-config
    ```

    ![RaspiSettingsSSHTerm1](https://user-images.githubusercontent.com/10654401/63646795-a7cec800-c718-11e9-9dd0-657270b4ef06.png)
    ![RaspiSettingsSSHTerm2](https://user-images.githubusercontent.com/10654401/63646796-aac9b880-c718-11e9-956f-a4bcfdc7f66e.png)


Una vez hecho esto, ya no necesitamos tener conectada la raspberry al monitor, y demás periféricos de entrada/salida, podemos conectarnos a ella vía SSH desde cualquier otro equipo, y para eso, hay muchas formas de hacerlo:

- Desde Windows
    - Windows 10 incluye cliente de SSH por defecto (desde hace poco) así que bastaría con abrir una consola de línea de comandos y escribir 
    ```
    ssh [usuario en la raspberry]@[IP o nombre del equipo]
    ```
    por ejemplo en mi caso, desde mi red local:
    ```
    C:\ssh pi@harlequin
    Linux harlequin 4.19.66-v7+ #1253 SMP Thu Aug 15 11:49:46 BST 2019 armv7l

    The programs included with the Debian GNU/Linux system are free software;
    the exact distribution terms for each program are described in the
    individual files in /usr/share/doc/*/copyright.

    Debian GNU/Linux comes with ABSOLUTELY NO WARRANTY, to the extent
    permitted by applicable law.
    Last login: Sun Aug 18 15:17:44 2019 from 192.168.1.89
    Linux harlequin 4.19.66-v7+ #1253 SMP Thu Aug 15 11:49:46 BST 2019 armv7l

    The programs included with the Debian GNU/Linux system are free software;
    the exact distribution terms for each program are described in the
    individual files in /usr/share/doc/*/copyright.

    Debian GNU/Linux comes with ABSOLUTELY NO WARRANTY, to the extent
    permitted by applicable law.
    Last login: Sun Aug 18 15:17:44 2019 from 192.168.1.89
    pi@harlequin:~ $
    ```
    - Vía [PuTTy](https://www.putty.org/). Atención porque más adelante vamos a usar uno de los binarios que suele proveer la instalación estándar de PuTTy, para otras tareas.

- Desde linux, prácticamente todas las distribuciones disponen de paquetes de clientes SSH, yo uso Ubuntu y lo trae instalado por defecto, en particular, también se puede hacer usando el WSL de Windows 10

    ![WSLSSHToRaspi](https://user-images.githubusercontent.com/10654401/63646889-eb760180-c719-11e9-89ba-0b3a4e08347a.png)

Ahora que estamos conectados: 

1. Vamos a instalar el depurador remoto:

    ```
    curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l ~/vsdbg
    ```

    La instalación del VSDbg tiene esta pinta, en el momento en el que la he ejecutado en mi Raspberry Pi:

    ```
    pi@harlequin:~ $ curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l ~/vsdbg
    Info: Creating install directory
    Using arguments
        Version                    : 'latest'
        Location                   : '/home/pi/vsdbg'
        SkipDownloads              : 'false'
        LaunchVsDbgAfter           : 'false'
        RemoveExistingOnUpgrade    : 'false'
    Info: Using vsdbg version '16.2.10709.2'
    Info: Previous installation at '/home/pi/vsdbg' not found
    Info: Using Runtime ID 'linux-arm'
    Downloading https://vsdebugger.azureedge.net/vsdbg-16-0-11220-2/vsdbg-linux-arm.zip
    Info: Successfully installed vsdbg at '/home/pi/vsdbg'
    ```

    En el futuro, para actualizar la versión del depurador remoto, sólo tenéis que ejecutar el comando de nuevo, comprobará si existe una versión nueva y la actualizará. Por ejemplo, si lo ejecutamos ahora otra vez, nos indica que ya tenemos la última versión instalada:

    ```
    pi@harlequin:~ $ curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l ~/vsdbg
    Info: Last installed version of vsdbg is '16.2.10709.2'
    Info: VsDbg is up-to-date
    Info: Using vsdbg version '16.2.10709.2'
    Using arguments
        Version                    : 'latest'
        Location                   : '/home/pi/vsdbg'
        SkipDownloads              : 'true'
        LaunchVsDbgAfter           : 'false'
        RemoveExistingOnUpgrade    : 'false'
    Info: Skipping downloads
    ```

2. Para poder depurar remotamente, necesitamos que este depurador (VSDbg) se ejecute con permisos root en raspbian, para lo cual tenemos que asegurarnos de que el usuario root tiene una contraseña asignada, con el siguiente comando.

    
    ```
    sudo passwd root
    ```

    Durante la ejecución, nos pide la nueva contraseña a establecer al usuario root. Para facilitar la explicación de algunos pasos más adelante, le he asignado como contraseña ```raspberry```, recordémosla porque la utilizaremos dentro de un rato.

    ```
    pi@harlequin:~ $ sudo passwd root
    Introduzca la nueva contraseña de UNIX:
    Vuelva a escribir la nueva contraseña de UNIX:
    passwd: contraseña actualizada correctamente
    pi@harlequin:~ $
    ```

3. También tenemos que habilitar las conexiones SSH para el usuario root. Editamos el fichero sshd_config usando nano por ejemplo

    ```
    sudo nano /etc/ssh/sshd_config
    ```
    y le añadimos una línea tal que así:
    ```
    PermitRootLogin yes
    ```

4. Por último, reiniciamos la Raspberry Pi

    ```
    sudo reboot
    ```


## En el equipo con Visual Studio Code instalado
Ya tenemos la Raspberry Pi preparada, vamos ahora con el equipo en el que vamos a trabajar, y que tendrá disponible: 

- [Visual Studio Code](https://code.visualstudio.com), con [la extensión de C#](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp) instalda

- La misma versión del [SDK .NET Core](https://dotnet.microsoft.com/download) instalada que la Raspberry Pi

Visual Studio Code es un editor de código multiplataforma (Windows Linux y MacOS), creado or Microsoft, con un conjunto de extensiones inmenso que permiten convertirlo en un entorno de desarrollo bastante potente, en particular para .NET Core, pero no se limita a este ecosistema, y soporta una enorme cantidad de lenguajes y frameworks, desde un PC/Mac cualquiera.

No olvidemos que nuestro objetivo es depurar aplicaciones que desarrollamos con la intención de ejecutarse en un dispositivo distinto, la Raspberry Pi, con un hardware muy particular y diferente al sistema desde el que estamos trabajando.

Para ilustrar los pasos que restan, en [este repositorio](https://github.com/Kash0321/remotedebugrpi) están los ficheros de configuración que vamos a analizar a continuación, y el código de ejemplo que se va a ejecutar en la Raspberry Pi. Centrémonos por ahora en la depuración remota, ya echaremos un vistazo al código en próximos artículos.

Para poder depurar el código, lo primero que tenemos que hacer es publicarlo en la Raspberry Pi. Para eso, necesitamos:

1. Lanzar el comando [```dotnet publish```](https://docs.microsoft.com/es-es/dotnet/core/tools/dotnet-publish) para un entorno de tiempo de ejecución (runtime) específico, en este caso ```linux-arm```, que es el entorno de ejecución de la Raspberry Pi, podríamos establecer muchas más opciones, pero si tenemos sincronizados los frameworks por defecto entre el equipo de desarrollo y el de ejecución no va  aser necesario (gustaos todo lo que queráis...). Este comando compila la aplicación, lee sus dependencias especificadas en el archivo de proyecto y publica el conjunto resultante de archivos en un directorio.

    ```
    dotnet publish -r linux-arm
    ```

2. Enviar, vía copia segura, el resultado de la publicación anterior, al un directorio del dispositivo que lo va a ejecutar. Aquí hay muuuchas opciones, una de las más sencillas es usar pushd / popd para gestionar el directorio de trabajo y devolverlo al estado anterior una vez ejecutado lo que queremos, y usar el binario desplegado con PuTTy que hemos mencionado anteriormente pscp.exe para enviar el contenido. Como todo esto lo vamos a taner que ejecutar n veces (cada vez que modifiquemos el código y queramos depurarlo), lo colocamos todo en un fichero [```publish.bat```](https://github.com/Kash0321/remotedebugrpi/blob/master/TestPi.ConsoleApp/publish.bat) 

    ```
    dotnet publish -r linux-arm /p:ShowLinkerSizeComparison=true 
    pushd .\bin\Debug\netcoreapp2.2\linux-arm\publish
    c:\work\putty\pscp -pw harlequin -v -r .\* pi@harlequin.local:/home/pi/Work/dotnet/TestPi.ConsoleApp
    popd
    ```

3. Ahora tenemos que añadir dos cosas a la carpeta ```.vscode``` dentro del proyecto, un fichero ```launch.json``` para configurar la preparación de la depuración y algunas tareas en el fichero ```tasks.json``` para gestionarlo todo (VSCode lo suele generar automáticamente cuando intentas depurar -F5- una aplicación) 

    ![configfiles](https://user-images.githubusercontent.com/10654401/63650065-a3210880-c746-11e9-815e-dc6347388c47.png)

    ![configfiles2](https://user-images.githubusercontent.com/10654401/63650080-da8fb500-c746-11e9-8883-02fa3d0ebb2a.png)

    **launch.json:** Prestad atención a que aquí estamos mezclando directorios locales al equipo de desarrollo y remotos en la Raspberry Pi

    ```json
    {
        "version": "0.2.0",
        "configurations": [
            {
                "name": "Publish & .NET Core Launch (remote console)",
                "type": "coreclr",
                "request": "launch",
                "preLaunchTask": "publish",
                "program": "/home/pi/dotnet/dotnet",
                "args": ["/home/pi/Work/dotnet/TestPi.ConsoleApp/TestPi.ConsoleApp.dll"], //Ruta remota a la dll que arranca el código que vamos a depurar
                "cwd": "/home/pi/Work/dotnet/TestPi.ConsoleApp", //Ruta en la Raspberry Pi al deirectorio de ejecución
                "stopAtEntry": false,
                "console": "internalConsole",
                "pipeTransport": {
                    "pipeCwd": "${workspaceFolder}",
                    "pipeProgram": "C:\\work\\putty\\plink.exe", //Ruta local hasta la línea de comandos de putty (en el equipo de desarrollo)
                    "pipeArgs": [
                        "-pw",
                        "raspberry", //Contraseña para el usuario root en la Raspberry Pi
                        "root@harlequin.local" //Usuario (debe ser root) y nombre de host (o IP) de la Raspberry Pi
                    ],
                    "debuggerPath": "/home/pi/vsdbg/vsdbg" //Ruta al visual studio debugger que hemos instalado en la Raspberry Pi
                }
            }
        ]
    }
    ```

    **tasks.json:**

    ```json
    {
        "version": "2.0.0",
        "tasks": [
            {
                "label": "build",
                "command": "dotnet",
                "type": "process",
                "args": [
                    "build",
                    "${workspaceFolder}\\TestPi.ConsoleApp\\TestPi.ConsoleApp.csproj"
                ],
                "problemMatcher": "$msCompile"
            },
            {
                "label": "publish",
                "type": "shell",
                "dependsOn": "build",
                "presentation": {
                    "reveal": "always",
                    "panel": "new"
                },
                "options": {
                    "cwd": "${workspaceFolder}\\TestPi.ConsoleApp"
                },
                "windows": {
                    "command": "${cwd}\\TestPi.ConsoleApp\\publish.bat"
                },
                "problemMatcher": []
            }
        ]
    }
    ```

## Resultado final!!

El efecto final de esto, es que cuando queremos depurar nuestro código, pulsamos F5, el código se compila, publica, se envía a la Raspberry Pi y se inicia la depuración remota, como por arte de magia...

![image](https://user-images.githubusercontent.com/10654401/63650685-aa97e000-c74d-11e9-9e3e-fe922af7fc69.png)

## Y una optimización de regalo

Casi todo esto estaba en los posts de Scott Hanselman, sólo lo he ampliado con más detalle y algunas ilustraciones, pero pronto me di cuenta de que hacía falta un ajuste para optimizar el trabajo: 

Cada vez que pulsamos F5, estamos copiando hasta las Raspberry un montón de ficheros que se corresponden con las dependencias del proyecto, lo cual lleva mucho tiempo, y ahce un poco desesperante el loop desarrollo - compilación - despliegue - depuración, así que he realizado un cambio sutil, en lugar de un solo fichero .bat de despliegue, he creado 2

- [`publish.bat`](https://github.com/Kash0321/remotedebugrpi/blob/master/TestPi.ConsoleApp/publish.bat) tiene un cambio con respecto al original, en lugar de copiar los ficheros con `.\*` lo hace con `.\TestPi` (esto hay que modificarlo en cada proyecto evidentemente), para que copie sólo las dll's correspondientes a nuestro código

    ```
    dotnet publish -r linux-arm /p:ShowLinkerSizeComparison=true 
    pushd .\bin\Debug\netcoreapp2.2\linux-arm\publish
    c:\work\putty\pscp -pw harlequin -v -r .\TestPi* pi@harlequin.local:/home/pi/Work/dotnet/TestPi.ConsoleApp
    popd
    ```

- [`republish.bat`](https://github.com/Kash0321/remotedebugrpi/blob/master/TestPi.ConsoleApp/republish.bat): se correponde con el `publish.bat` original

    ```
    dotnet publish -r linux-arm /p:ShowLinkerSizeComparison=true 
    pushd .\bin\Debug\netcoreapp2.2\linux-arm\publish
    c:\work\putty\pscp -pw harlequin -v -r .\* pi@harlequin.local:/home/pi/Work/dotnet/TestPi.ConsoleApp
    popd
    ```

- `tasks.json` tiene una tarea adicional para gestionar la republicación:

    ```json
    {
        "version": "2.0.0",
        "tasks": [
            {
                "label": "build",
                "command": "dotnet",
                "type": "process",
                "args": [
                    "build",
                    "${workspaceFolder}\\TestPi.ConsoleApp\\TestPi.ConsoleApp.csproj"
                ],
                "problemMatcher": "$msCompile"
            },
            {
                "label": "publish",
                "type": "shell",
                "dependsOn": "build",
                "presentation": {
                    "reveal": "always",
                    "panel": "new"
                },
                "options": {
                    "cwd": "${workspaceFolder}\\TestPi.ConsoleApp"
                },
                "windows": {
                    "command": "${cwd}\\TestPi.ConsoleApp\\publish.bat"
                },
                "problemMatcher": []
            },
            {
                "label": "republish",
                "type": "shell",
                "dependsOn": "build",
                "presentation": {
                    "reveal": "always",
                    "panel": "new"
                },
                "options": {
                    "cwd": "${workspaceFolder}\\TestPi.ConsoleApp"
                },
                "windows": {
                    "command": "${cwd}\\TestPi.ConsoleApp\\republish.bat"
                },
                "problemMatcher": []
            }
        ]
    }
    ```

- `launch.json` tiene una preparación adicional para gestionar la republicación

    ```json
    {
        "version": "0.2.0",
        "configurations": [
            {
                "name": "Publish & .NET Core Launch (remote console)",
                "type": "coreclr",
                "request": "launch",
                "preLaunchTask": "publish",
                "program": "/home/pi/dotnet/dotnet",
                "args": ["/home/pi/Work/dotnet/TestPi.ConsoleApp/TestPi.ConsoleApp.dll"], //Ruta remota a la dll que arranca el código que vamos a depurar
                "cwd": "/home/pi/Work/dotnet/TestPi.ConsoleApp", //Ruta en la Raspberry Pi al deirectorio de ejecución
                "stopAtEntry": false,
                "console": "internalConsole",
                "pipeTransport": {
                    "pipeCwd": "${workspaceFolder}",
                    "pipeProgram": "C:\\work\\putty\\plink.exe", //Ruta local hasta la línea de comandos de putty (en el equipo de desarrollo)
                    "pipeArgs": [
                        "-pw",
                        "raspberry", //Contraseña para el usuario root en la Raspberry Pi
                        "root@harlequin.local" //Usuario (debe ser root) y nombre de host (o IP) de la Raspberry Pi
                    ],
                    "debuggerPath": "/home/pi/vsdbg/vsdbg" //Ruta al visual studio debugger que hemos instalado en la Raspberry Pi
                }
            },
            {
                "name": "RE-Publish & .NET Core Launch (remote console)",
                "type": "coreclr",
                "request": "launch",
                "preLaunchTask": "republish",
                "program": "/home/pi/dotnet/dotnet",
                "args": ["/home/pi/Work/dotnet/TestPi.ConsoleApp/TestPi.ConsoleApp.dll"],
                "cwd": "/home/pi/Work/dotnet/TestPi.ConsoleApp",
                "stopAtEntry": false,
                "console": "internalConsole",
                "pipeTransport": {
                    "pipeCwd": "${workspaceFolder}",
                    "pipeProgram": "C:\\work\\putty\\plink.exe",
                    "pipeArgs": [
                        "-pw",
                        "raspberry", 
                        "root@harlequin.local"
                    ],
                    "debuggerPath": "/home/pi/vsdbg/vsdbg"
                }
            }
        ]
    }
    ```
Ahora desde la pestaña de depuración de VS Code, podemos seleccionar el modo de publicación o republicación, y con el primero, nos evitamos un montón de tiempo en cada iteración del loop de desarrollo - compilación - despliegue - depuración:

![image](https://user-images.githubusercontent.com/10654401/63650948-8d184580-c750-11e9-801e-c8c7d41cac03.png)



