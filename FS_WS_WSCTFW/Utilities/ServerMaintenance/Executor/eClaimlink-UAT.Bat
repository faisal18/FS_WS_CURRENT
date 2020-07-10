echo off
c:
cd\
cd "PSTools"
call eHDF.Bat
c:
cd\
cd "PSTools"
Psservice.exe \\10.156.62.30 -u dh.local\portal.admin -p $ecure@dmin!@#4!!$ restart W3SVC
c:
cd\
cd "PSTools"

Call eHDF_Disconnect.Bat


