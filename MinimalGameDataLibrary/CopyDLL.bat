@echo off
setlocal

:: Get the script's directory
for %%i in ("%~dp0.") do set "batchDir=%%~fi"

:: Specify source and destination folders (use explicit paths)
set "sourcePath=%batchDir%\bin\Debug\MinimalGameDataLibrary.dll"
set "destinationFolder=C:\MyWorks\GameDev\Unity\Projects\NDRBackendTest\Assets\DLLs"
set "destinationPath=%destinationFolder%\MinimalGameDataLibrary.dll"

:: Set the auto overwrite flag (if you trust the source)
set "autoOverwrite=true"
:: Is allowed to Create the destination folder if it doesn't exist
set "autoCreateOnNotDestDirFound=true"

:: Perform the copy operation
if not exist "%sourcePath%" (
    echo Source file not found: %sourcePath%
    goto :eof
)

:: Create the destination folder if it doesn't exist
if not exist "%destinationFolder%" (
    if "%autoCreateOnNotDestDirFound%"=="true" (
        mkdir "%destinationFolder%" || (
            echo Failed to create destination folder: %destinationFolder%
            goto :eof
        )
    ) else (
        echo Destination folder not found: %destinationFolder%
        goto :eof
    )
)

:: Check if destination file exists and prompt for confirmation (if auto overwrite is disabled)
if exist "%destinationPath%" (
    if "%autoOverwrite%"=="false" (
        choice /C YN /M "Destination file already exists. Overwrite? [Y/N]:"
        if errorlevel 2 goto :eof
    )
)

:: Perform the copy operation
copy "%sourcePath%" "%destinationPath%" /Y

:: Log the copy operation to a log file
set "logFile=%batchDir%\CopyLog.txt"
echo Copied %sourcePath% to %destinationPath% on %date% at %time% >> "%logFile%"

:: Display a success message
echo Copy operation completed successfully.

endlocal
