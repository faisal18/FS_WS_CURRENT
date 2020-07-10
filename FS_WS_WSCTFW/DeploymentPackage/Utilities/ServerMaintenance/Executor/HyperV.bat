@if (@CodeSection == @Batch) @then



@echo off
cd C:\Program Files (x86)\Cisco\Cisco AnyConnect Secure Mobility Client\

rem Use %SendKeys% to send keys to the keyboard buffer
set SendKeys=CScript //nologo //E:JScript "%~F0"

rem Start the other program in the same Window
start "" /B cmd

%SendKeys% "echo off{ENTER}"

%SendKeys% "vpncli.exe{ENTER}"
ping -n 2 -w 1 127.0.0.1 > NUL

%SendKeys% "disconnect{ENTER}"
ping -n 10 -w 1 127.0.0.1 > NUL

goto :EOF


@end


// JScript section

var WshShell = WScript.CreateObject("WScript.Shell");
WshShell.SendKeys(WScript.Arguments(0));