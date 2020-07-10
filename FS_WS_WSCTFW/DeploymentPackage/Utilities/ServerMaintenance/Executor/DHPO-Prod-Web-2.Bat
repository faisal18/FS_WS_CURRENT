echo off
c:
cd\
cd "PSTools"
call eHDF-VPN-Connect.Bat
c:
cd\
cd "PSTools"
Psservice.exe \\10.156.62.28 -u Administrator -p web12345!@#$%% restart Spooler
c:
cd\
cd "PSTools"

Call eHDF-VPN-Disconnect.Bat