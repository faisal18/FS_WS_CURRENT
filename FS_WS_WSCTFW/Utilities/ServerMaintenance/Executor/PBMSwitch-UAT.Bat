

REM echo off
REM c:
REM cd\
REM cd "PSTools"
REM Psservice.exe \\10.5.3.42 -u administrator -p "^OM4IS(G$nV%%+tIA+:r55G" restart Spooler
REM call HyperVConnection.bat
REM timeout /t 5  >NUL


echo off
c:
cd\
cd "PSTools"
REM Psservice.exe \\10.5.3.35 -u 10.5.3.35\administrator -p "ux(6j@^smpM^64c.}2v,QY" restart Spooler
REM Psservice.exe \\10.4.52.32 -u 10.4.52.32\administrator -p "ux(6j@^smpM^64c.}2v,QY" restart glassfish4
rem Psservice.exe \\10.5.3.35 -u dhc.local\fsheikh -p "fsheikh.FF" restart glassfish4

Psservice.exe \\10.5.3.35 -u dhc.local\fsheikh -p "fsheikh.FF" stop glassfish4

REM timeout /t 5  >NUL
ping -n 10 127.0.0.1 > nul
Psservice.exe \\10.5.3.35 -u dhc.local\fsheikh -p "fsheikh.FF" query glassfish4
ping -n 10 127.0.0.1 > nul
REM timeout /t 5  >NUL

Psservice.exe \\10.5.3.35 -u dhc.local\fsheikh -p "fsheikh.FF" start glassfish4
REM Psservice.exe \\10.5.3.35 -u dhc.local\fsheikh -p "fsheikh.FF" restart Spooler

REM ping -n 6 127.0.0.1 > nul
 ping -n 10 127.0.0.1 > nul
Psservice.exe \\10.5.3.35 -u dhc.local\fsheikh -p "fsheikh.FF" query glassfish4
 ping -n 6 127.0.0.1 > nul
REM timeout /t 5 >NUL
REM echo off
REM c:
REM cd\
REM cd "PSTools"
REM Psservice.exe \\10.5.3.42 -u administrator -p "^OM4IS(G$nV%%+tIA+:r55G" restart Spooler
REM call HyperV-DisConnect.bat
REM timeout /t 5 

REM PSEXEC \\10.4.52.32 -u 10.4.52.32\administrator -p "ux(6j@^smpM^64c.}2v,QY" "C:\Users\Administrator\Documents\Fazeel\RestartServ.bat"