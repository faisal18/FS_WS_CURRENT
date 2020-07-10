echo off
c:
cd\
cd "PSTools"
call eHDF-VPN-Connect.Bat
c:
cd\
cd "PSTools"
Psservice.exe \\10.156.62.52 -u Administrator -p "?<?z=*-l&o~<_e9OG=W='|#:fJ7T!7?('ac!!#$" restart Spooler
c:
cd\
cd "PSTools"

Call eHDF-VPN-Disconnect.Bat


