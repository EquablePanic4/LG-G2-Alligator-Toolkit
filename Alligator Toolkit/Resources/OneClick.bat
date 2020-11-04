@echo off
cd "%AppData%\Sees Hard\Alligator Toolkit"
echo ---------------------------------
echo LG Root - One click script
echo Written by avicohh
echo ---------------------------------
echo.
echo Please make sure that USB debugging is enabled, and LG drivers are installed.
echo.
echo Starting adb server..
adb.exe kill-server > nul
adb.exe start-server > nul
echo.
echo Waiting for device..
adb.exe wait-for-device
echo.
echo Device detected!
echo.
echo Pushing files..
adb.exe push busybox /data/local/tmp/ && adb.exe push lg_root.sh /data/local/tmp/ && adb.exe push UPDATE-SuperSU-v2.46.zip /data/local/tmp/
echo.
echo Rebooting..
adb reboot
echo.

echo Looking for LG serial port..
echo.
adb.exe wait-for-device
for /f "tokens=2*" %%a in ('reg query HKLM\hardware\devicemap\SERIALCOMM /v \Device\LG*ANDNETDIAG* 2^>nul ^| find "REG_SZ" 2^>nul') do set "comPath=%%~b"
if "%comPath%" == "" (
echo Serial port not found, please insert the phone manually into Download mode.
echo.
echo Disconnect the USB cable and turn off the phone.
echo Then press and hold the Volume Up button, and while you're doing that connect the USB cable again.
echo.
echo Waiting for device..
echo.
) else (
echo Phone found at %comPath%!
echo.
echo Rebooting into Download mode..
echo.
Send_Command.exe \\.\%comPath% < enterDownload | echo off
echo Waiting for device..
echo.
ping 127.0.0.1 -n 15 | echo off
)

set comPath=
:wait-for-download
for /f "tokens=2*" %%a in ('reg query HKLM\hardware\devicemap\SERIALCOMM /v \Device\LG*ANDNETDIAG* 2^>nul ^| find "REG_SZ" 2^>nul') do set "comPath=%%~b"
if "%comPath%" == "" goto wait-for-download
echo Phone found at %comPath%!
echo.
echo Rooting phone..
echo.
echo If you don't see the SuperSu installer script runs within about a minute,
echo then the root failed.
echo.
ping 127.0.0.1 -n 15 | echo off
Send_Command.exe \\.\%comPath% < rootDownload
echo.
echo Rebooting..
Send_Command.exe \\.\%comPath% < leaveDownload | echo off
echo.
echo Done!
pause > nul | echo Press any key to exit..