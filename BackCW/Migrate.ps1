param (
    [Parameter(Mandatory=$true)][String]$name
)

$Env:DbConnectionString = "User ID=postgres;Password=coolpassword;Host=127.0.0.1;Port=5432;Pooling=true;"
dotnet ef migrations add $name
dotnet ef migrations script
