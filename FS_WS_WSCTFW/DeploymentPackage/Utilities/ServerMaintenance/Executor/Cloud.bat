
echo off
c:
cd\
cd "Program Files (x86)"
cd "Cisco Systems"
cd "VPN Client"
call vpnclient.exe connect ehdf

timeout /t 5

call vpnclient.exe stat

timeout /t 5

cd\

echo on