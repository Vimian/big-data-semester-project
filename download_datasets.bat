ECHO off
cls

cd dataset

if not exist "./datasets/" mkdir "./datasets/"

ECHO Downloading datasets
ECHO.

ECHO Downloading stock data
ECHO.

bash download_from_google_drive.sh "1N7IFJg509Fr7Bwo7Ulh0afofL85Rtqk6" "./datasets/Stock.zip"

ECHO Downloading gold data
ECHO.

bash download_from_google_drive.sh "1N5hWyOBN7SpCIkzLnR_iO6_NeLkJnbOb" "./datasets/Gold.zip"

ECHO Downloading bitcoin tweet data
ECHO.

bash download_from_google_drive.sh "1N33KTzX7YG8KfLiokxhvoIfRYyOiQZqv" "./datasets/BitcoinTweets.zip"

ECHO.
ECHO Downloaded datasets successful
ECHO.

cd datasets

ECHO Downloading 7zip
ECHO.

bash ./../download_7zip.sh

"7zr.exe" x "7z.7z" -o"./7z"

ECHO.
ECHO Unzipping datasets
ECHO.

"./7z/7za.exe" x "./Stock.zip" -o"./Stock"
"./7z/7za.exe" x "./Gold.zip" -o"./Gold"
"./7z/7za.exe" x "./BitcoinTweets.zip" -o"./BitcoinTweets"

ECHO.
ECHO Unzipped datasets successful
pause
