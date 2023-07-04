# EmbeddedWebApp
A console app which starts a web app internally

When starting this in Visual Studio / Rider in debug configuration everything looks as expected:
![Debugging](WorkingBlazor.png)

# Build release app

Working version:

First build the Blazor app in release configuration and copy the output to the target path.
Then build the console app in release configuration and copy the output to the target path.
Then start the console app.

```bash
export TARGET_PATH=$HOME/Downloads/testapp
dotnet publish DxBlazorServerApp1/DxBlazorServerApp1.csproj -p:PublishSingleFile=true --self-contained true --configuration Release -o  $TARGET_PATH
dotnet publish EmbeddedWebApp/EmbeddedWebApp.csproj -p:PublishSingleFile=true --self-contained true --configuration Release -o  $TARGET_PATH
cd $TARGET_PATH
.\EmbeddedWebApp
```


# Cannot connect to localhost with Microsoft Edge

open `edge://net-internals/#hsts` and delete localhost in section "Delete domain security policies"