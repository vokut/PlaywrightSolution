1. Install scoop and Allure CLI
Set-ExecutionPolicy RemoteSigned -Scope CurrentUser;irm get.scoop.sh | iex
scoop install allure

2. Build solution and run this in PowerShell
<solution folder>\Spinberry.Tests\bin\Debug\net8.0\playwright.ps1 install
