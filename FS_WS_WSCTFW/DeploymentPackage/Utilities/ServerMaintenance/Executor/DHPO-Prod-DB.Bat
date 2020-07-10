echo off
c:
cd\
cd "PSTools"
call eHDF-VPN-Connect.Bat
c:
cd\
cd "PSTools"
Psservice.exe \\10.156.62.53 -u dh.local\Fazeel.sheikh -p Abcd1234$$ restart Spooler
c:
cd\
cd "PSTools"

Call eHDF-VPN-Disconnect.Bat


