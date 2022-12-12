# WinGif

Create animated gif files from foreground windows capture or frames saved in individual files.


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



----
Created from [Janda.Go](https://github.com/Jandini/Janda.Go)
