
echo off
c:
cd\
cd "Program Files (x86)"
cd "Cisco Systems"
cd "VPN Client"
call vpnclient.exe connect ehdf

timeout /t 3

call vpnclient.exe stat

timeout /t 3

cd\

echo on