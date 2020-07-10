

echo off
c:
cd\
cd "PSTools"
REM psservice  restart spooler
psservice  restart CVPND
REM psservice \\10.11.13.181 restart spooler


timeout /t 3 
