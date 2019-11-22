
param (

    [string]$source = $(throw "-source is required."),

    [string]$target = $(throw "-target is required.")
)

Copy-Item -Path $source -Destination $target 
