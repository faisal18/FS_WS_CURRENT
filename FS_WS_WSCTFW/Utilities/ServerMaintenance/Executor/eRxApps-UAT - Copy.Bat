echo off
c:
cd\
cd "PSTools"
call eHDF.Bat
c:
cd\
cd "PSTools"
REM Psservice.exe \\10.156.62.137 -u dh.local\Fazeel.sheikh -p Abcd1234$$ restart Spooler
Psservice.exe \\10.156.62.137 -u Fazeel.sheikh@dh.local -p Abcd1234$$ restart W3SVC
c:
cd\
cd "PSTools"

Call eHDF_Disconnect.Bat


