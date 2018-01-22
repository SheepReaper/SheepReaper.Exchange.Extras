$VerbosePreference='Continue'
$filter = { $_ -match "SheepReaper.Exchange.PowerShell" }

Write-Verbose "TEST: Module Loading"

if (([appdomain]::CurrentDomain.GetAssemblies() | Where-Object -FilterScript $filter) -ne $null) {
	Write-Error "ERROR: The assembly was loaded already, try killing the host process or restarting VS..."
	exit(-1)
}

Import-Module ..\SheepReaper.Exchange.PowerShell\bin\Debug\netstandard2.0\SheepReaper.Exchange.PowerShell.dll -ErrorAction Continue
Import-Module .\SheepReaper.Exchange.Powershell.dll -ErrorAction SilentlyContinue

if(([appdomain]::CurrentDomain.GetAssemblies() | Where-Object -FilterScript $filter) -ne $null) {
	Write-Verbose "SUCCESS: Module Loaded"
} else {
	Write-Error "FAIL: Module not Loaded" -ErrorAction Continue
	exit(1)
}

Test-ExchangeConnection -ErrorAction Continue *>&1
exit(0)