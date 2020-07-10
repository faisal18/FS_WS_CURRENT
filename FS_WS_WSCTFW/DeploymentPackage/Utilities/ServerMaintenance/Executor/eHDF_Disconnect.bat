
echo off
c:
cd\
cd "Program Files (x86)"
cd "Cisco Systems"
cd "VPN Client"


call vpnclient.exe disconnect

timeout /t 3

call vpnclient.exe stat

timeout /t 1

REM cd\

echo on