echo off
c:
cd\
cd "PSTools"
call eHDF.Bat
c:
cd\
cd "PSTools"
Psservice.exe \\10.156.62.27 -u Administrator -p web12345!@#$%% restart W3SVC
c:
cd\
cd "PSTools"

Call eHDF_Disconnect.Bat