c:
cd\
cd "FS_PoolRestarter" 
REM Psexec \\{Computer Name of ISS7 Server} %systemroot%\System32\inetsrv\appcmd recycle apppool my-app-pool

REM PsService.exe \\10.5.3.33 -u dhc\fsheikh -p "fsheikh.FF" restart spooler

REM Psexec.exe \\10.5.3.33 -u dhc\fsheikh -p "fsheikh.FF" %systemroot%\System32\inetsrv\appcmd recycle apppool "DefaultAppPool" 
REM >> "C:\inetpub\wwwroot\AdminUtils\EmailQueue\TechSupport_AppPoolRestarted_Automation.TXt


REM  Psexec.exe \\10.5.3.33 -u dhc\fsheikh -p "fsheikh.FF" "%systemroot%\System32\schtasks.exe" /RUN /TN "apppoolrestarter"  

REM "%systemroot%\System32\schtasks.exe" /run /S "10.11.13.48" /U dhc\fsheikh /P "fsheikh.FF" /TN "apppoolrestarter"  

REM "%systemroot%\System32\schtasks.exe" /run /S "10.5.3.33" /U dhc\fsheikh /P "fsheikh.FF" /TN "apppoolrestarter" 
"%systemroot%\System32\schtasks.exe" /run /S "10.162.176.71" /U dh.local\fazeel.sheikh /P "Abcd1234$$" /TN "apppoolrestarter"  

REM Psexec.exe \\10.11.13.48 -u dhc\fsheikh -p "fsheikh.FF" "C:\Users\fsheikh\Documents\fazeel\AppPoolRestart.bat"
REM >> "C:\inetpub\wwwroot\AdminUtils\EmailQueue\TechSupport_AppPoolRestarted_Automation.TXt


REM %SYSTEMROOT%\System32\inetsrv\appcmd recycle  apppool /apppool.name:"AutomationServices" >> "C:\inetpub\wwwroot\AdminUtils\EmailQueue\TechSupport_AppPoolRestarted_Automation.TXT

c:
cd\
cd "FS_PoolRestarter"