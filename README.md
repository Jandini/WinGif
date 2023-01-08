# WinGif

[![build](https://github.com/Jandini/WinGif/actions/workflows/build.yml/badge.svg)](https://github.com/Jandini/WinGif/actions/workflows/build.yml)

Create animated gif files from foreground windows capture or frames saved in individual files. 
Created from [Janda.Go](https://github.com/Jandini/Janda.Go) powered by [AnimatedGif](https://github.com/mrousavy/AnimatedGif).

Download latest [WinGif](https://github.com/Jandini/WinGif/releases/download/0.2.0/WinGif.exe)

## Capture active window

Capture only "Chrome" window into `%TEMP%/chrome.gif` file and save frames as png files in `%TEMP%/chrome` directory.
```
wingif capture -s -t Chrome -o %TEMP%/chrome.gif -f %TEMP%/chrome
```

![chrome](https://user-images.githubusercontent.com/19593367/207031114-49891e15-c160-4346-b546-b943dbfe0adc.gif)


## Capture WinGif

The option `-l` allow to capture WinGif window. By default the WinGif window does not allow to capture itself. 
```
wingif capture -l -s -t wingif.exe -o %TEMP%\wingif.gif -f %TEMP%\wingif
```
![wingif2](https://user-images.githubusercontent.com/19593367/207033078-c1ed8e1f-db43-41c5-9228-0a86db18efc8.gif)



## Make gif file from frames

Make new gif file from png frames saved earilier in `%TEMP%\wingif` directory.
```
wingif make -i %TEMP%\wingif -o %TEMP%\wingif2.gif
```
![image](https://user-images.githubusercontent.com/19593367/207031957-4e51fd1c-fc4e-4d9f-98e3-7cdfb712fd81.png)




## Capture console application from the script

When you want to capture multiple actions / commands, the entire capture can be scripted. It triggers the idea to add keyboard type scripts to WinGif :)
Following example will capture a demo of Janda.Go template. The demo script will run following commands:
```
dotnet --version
dotnet new install Janda.Go
dotnet new consolego --help
dotnet new consolego -n HelloWorld
cd HelloWorld\src\HelloWorld
dotnet run HelloWorld
cd ..\..\..
dotnet new consolego -n HelloSerilog -us
cd HelloSerilog\src\HelloSerilog
dotnet run HelloSerilog
cd ..\..\..
dotnet new consolego -n HelloAllFeatures -al
cd HelloAllFeatures\src\HelloAllFeatures
dotnet run -- --help
dotnet run 
dotnet run -- go
dotnet run -- go -n HelloDir
```

Instead of user typing the above commands while WinGif is capturing, all the actions were scripted in `JandaGo.cmd` batch file. 

```batch
:: Perform cleanup before capturing the demo. This allows to run this script again and again.
rd HelloSerilog /s /q 2>nul
rd HelloAllFeatures /s /q 2>nul
rd HelloWorld /s /q 2>nul
dotnet new uninstall Janda.Go

set TITLE=Janda.Go Demo
:: Capture only when window with "Janda.Go Demo" title is active. Make sure the WinGif is available in PATH environment.
start WinGif -s -t "%TITLE%" -o .\JandaGo.gif

mode con:cols=120 lines=40

:: Demo starts here
@pause
@cls
@title %TITLE%
dotnet --version

@call :PauseBeforeNext
dotnet new install Janda.Go

@call :PauseBeforeNext
dotnet new consolego --help

@call :PauseBeforeNext
dotnet new consolego -n HelloWorld

@call :PauseBeforeNext
@cd HelloWorld\src\HelloWorld
dotnet run HelloWorld

@call :PauseBeforeNext
@cd ..\..\..
dotnet new consolego -n HelloSerilog -us

@call :PauseBeforeNext
@cd HelloSerilog\src\HelloSerilog
dotnet run HelloSerilog

@call :PauseBeforeNext
@cd ..\..\..
dotnet new consolego -n HelloAllFeatures -al

@call :PauseBeforeNext
@cd HelloAllFeatures\src\HelloAllFeatures
dotnet run -- --help

@call :PauseBeforeNext
dotnet run 

@call :PauseBeforeNext
dotnet run -- go

@call :PauseBeforeNext
dotnet run -- go -n HelloDir

@call :PauseBeforeNext
@cd ..\..\..

:: Changing the window title will stop WinGif capturing.
@title The End
@goto :EOF

:PauseBeforeNext
:: This will simulate user prompt and wait 3 seconds between commands.
@echo.
@echo | set /p="%cd%>"
@timeout /t 3 > nul
@cls
@goto :EOF
```

This is the result of `JandaGo.cmd` script.

![JandaGo](https://user-images.githubusercontent.com/19593367/211174559-b45486cd-20d8-49fe-839d-7d7a50d6395d.gif)



