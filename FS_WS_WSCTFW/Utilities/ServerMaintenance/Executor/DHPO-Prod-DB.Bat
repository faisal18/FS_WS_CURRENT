echo off
c:
cd\
cd "PSTools"
call eHDF.Bat
timeout /t 5 
c:
cd\
cd "PSTools"
Psservice.exe \\10.162.176.24 -u dh.local\Fazeel.sheikh -p Abcd1234$$ restart MSSQLSERVER
c:
cd\
cd "PSTools"
timeout /t 5 


c:
cd\
cd "PSTools"
Call eHDF_Disconnect.Bat


