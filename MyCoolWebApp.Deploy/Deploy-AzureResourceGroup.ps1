#Requires -Version 3.0
#Requires -Module AzureRM.Resources
#Requires -Module Azure.Storage

Param(
    [string] [Parameter(Mandatory=$true)] $ResourceGroupLocation,
    [string] $ResourceGroupName = 'MyCoolWebApp',
    [switch] $UploadArtifacts,
    [string] $StorageAccountName,
    [string] $StorageContainerName = $ResourceGroupName.ToLowerInvariant() + '-stageartifacts',
    [string] $TemplateFile = 'Templates\WebSiteSQLDatabase.json',
    [string] $TemplateParametersFile = 'Templates\WebSiteSQLDatabase.parameters.json',
    [string] $ArtifactStagingDirectory = '.',
    [string] $DSCSourceFolder = 'DSC'
)

Import-Module Azure -ErrorAction SilentlyContinue

try {
    [Microsoft.Azure.Common.Authentication.AzureSession]::ClientFactory.AddUserAgent("VSAzureTools-$UI$($host.name)".replace(" ","_"), "2.9.1")
} catch { }

Set-StrictMode -Version 3

$OptionalParameters = New-Object -TypeName Hashtable
$TemplateFile = [System.IO.Path]::GetFullPath([System.IO.Path]::Combine($PSScriptRoot, $TemplateFile))
$TemplateParametersFile = [System.IO.Path]::GetFullPath([System.IO.Path]::Combine($PSScriptRoot, $TemplateParametersFile))

$appName = $ResourceGroupName.Split('.')[0]
$customerName = $ResourceGroupName.Split('.')[1]
$environmentName = $ResourceGroupName.Split('.')[2]

if($appName.ToLower() -cmatch "^[a-z0-9-]*$")
{
	'Valid app name: ' + $appName
}
else
{
	'Invalid app name: ' + $appName
	Exit
}

if($environmentName.ToLower() -cmatch "^[a-z0-9-]*$")
{
	'Valid environment name: ' + $environmentName
}
else
{
	'Invalid environment name: ' + $environmentName
	Exit
}

if($customerName.ToLower() -cmatch "^[a-z0-9-]*$")
{
	'Valid customer name: ' + $customerName
}
else
{
	'Invalid customer name: ' + $customerName
	Exit
}

$webAppName = $appName + '-' + $customerName + '-' + $environmentName 
$sqlServerName = $appName.ToLowerInvariant() + '-' + $customerName.ToLowerInvariant() + '-' + $environmentName.ToLowerInvariant()

$storageAccountName = 'wfm' + $customerName.ToLower() + $environmentName.ToLower() + 'acc'
$storageAccountName = $storageAccountName.Replace('-', '')
'storage account name is: ' + $storageAccountName
'sql server name is: ' +  $sqlServerName

# Create or update the resource group using the specified template file and template parameters file
New-AzureRmResourceGroup -Name $ResourceGroupName -Location $ResourceGroupLocation -Verbose -Force -ErrorAction Stop

New-AzureRmResourceGroupDeployment -Name ((Get-ChildItem $TemplateFile).BaseName + '-' + ((Get-Date).ToUniversalTime()).ToString('MMdd-HHmm')) `
                                   -ResourceGroupName $ResourceGroupName `
                                   -TemplateFile $TemplateFile `
                                   -TemplateParameterFile $TemplateParametersFile `
                                   -appName $appName -customerName $customerName -environmentName $environmentName -storageAccountName $storageAccountName `
                                   -Force -Verbose
