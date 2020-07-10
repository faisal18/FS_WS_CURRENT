c:
cd\
cd windows
cd Microsoft.NET
cd Framework64
cd v4.0.30319

REM C:\Windows\Microsoft.NET\Framework64\v4.0.30319>aspnet_regiis.exe -app "/Utilities" -pe "appSettings"
aspnet_regiis.exe -app "/AdminUtils" -pd "appSettings"


Timeout /t 5