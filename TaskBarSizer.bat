@echo off
setlocal enableextensions enabledelayedexpansion
color f
title Windows 11 Task Bar Sizer
echo https://github.com/SrgjanX/TaskBarSizer

echo -- -- -- -- --

echo Enter value from 1 to 3 (1:Small, 2:Normal, 3:Big)

set /p UserInput=Enter number: 

if !UserInput! GEQ 1 (
    if !UserInput! LEQ 3 (

      set regValue = 0
      set /a regValue = !UserInput! - 1

      REG ADD HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced /v TaskbarSI /t REG_DWORD /d !regValue! /f
      REM Restart explorer:
      taskkill /IM "explorer.exe" /F
      explorer.exe
      echo Explorer succesfully restarted
      
      goto :end
    )
    goto :invalidNumber
  )

:invalidNumber
color c
echo Unsupported value entered.

:end

pause