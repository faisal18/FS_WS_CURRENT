echo off
c:
cd\
cd "PSTools"
call eHDF-VPN-Connect.Bat
c:
cd\
cd "PSTools"
Psservice.exe \\10.156.62.30 -u dh.local\portal.admin -p $ecure@dmin!@#4!!$ restart Spooler
c:
cd\
cd "PSTools"

Call eHDF-VPN-Disconnect.Bat


