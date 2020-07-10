c:
cd\
cd "Program Files (x86)"
cd "Cisco Systems"
cd "VPN Client"
vpnclient.exe Disconnect
timeout /t 5
vpnclient.exe stat
