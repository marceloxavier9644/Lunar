@echo off

REM Fecha o processo LunarAtualizador.exe
taskkill /f /im LunarAtualizador.exe

REM Aguarda 2 segundos para garantir que o processo foi encerrado
timeout /t 2 /nobreak

REM Copia os arquivos LunarAtualizador.exe e LunarBase.dll para a pasta de destino
xcopy "C:\Lunar\LunarAtualizador.exe" "C:\Lunar\Atualizador\" /y
xcopy "C:\Lunar\LunarBase.dll" "C:\Lunar\Atualizador\" /y

REM Cria um arquivo de texto na pasta de destino
echo Arquivo de texto criado pelo .bat > "C:\Lunar\Atualizador\AtualizaBanco.txt"

REM Abre o LunarAtualizador.exe da pasta de destino
start "" "C:\Lunar\Atualizador\LunarAtualizador.exe"