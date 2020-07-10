

REM echo off
REM c:
REM cd\
REM cd "PSTools"
REM Psservice.exe \\10.5.3.42 -u administrator -p "^OM4IS(G$nV%%+tIA+:r55G" restart Spooler
REM call HyperVConnection.bat
REM timeout /t 5 


echo off
c:
cd\
cd "PSTools"
REM Psservice.exe \\10.5.3.35 -u 10.5.3.35\administrator -p "ux(6j@^smpM^64c.}2v,QY" restart Spooler
REM Psservice.exe \\10.4.52.32 -u 10.4.52.32\administrator -p "ux(6j@^smpM^64c.}2v,QY" restart glassfish4
REM Psservice.exe \\10.5.3.42 -u dhc.local\fsheikh -p "fsheikh.FF" restart MSSQLSERVER
Psservice.exe \\10.162.176.85 -u dh.local\fsheikh -p "Abcd1234$$" restart MSSQLSERVER
REM Psservice.exe \\10.5.3.42 -u dhc.local\fsheikh -p "fsheikh.FF" restart Spooler


timeout /t 5 
REM echo off
REM c:
REM cd\
REM cd "PSTools"
REM Psservice.exe \\10.5.3.42 -u administrator -p "^OM4IS(G$nV%%+tIA+:r55G" restart Spooler
REM call HyperV-DisConnect.bat
REM timeout /t 5 

REM PSEXEC \\10.4.52.32 -u 10.4.52.32\administrator -p "ux(6j@^smpM^64c.}2v,QY" "C:\Users\Administrator\Documents\Fazeel\RestartServ.bat"