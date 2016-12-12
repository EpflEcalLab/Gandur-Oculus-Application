@echo off
Timeout /t 30 /nobreak
:StartAuto
Timeout /t 3 /nobreak >nul
echo Start Application
start /wait "" "C:\Users\Admin\Desktop\LastBuild.exe"
echo End Application
goto StartAuto