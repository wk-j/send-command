## Send Command

[![NuGet](https://img.shields.io/nuget/v/wk.SendCommand.svg)](https://www.nuget.org/packages/wk.SendCommand)

```bash
dotnet tool install -g wk.SendCommand
```

## Usage

```bash
wk-send-command \
    --database FullTextSearch \
    --sql 'select "Id", "Name", substring("Profile1", 0, 30) as "Profile" from "Students" limit 10'
```