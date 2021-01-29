## Send Command

[![Actions](https://github.com/wk-j/send-command/workflows/NuGet/badge.svg)](https://github.com/wk-j/send-command/actions)
[![NuGet](https://img.shields.io/nuget/v/wk.SendCommand.svg)](https://www.nuget.org/packages/wk.SendCommand)
[![NuGet Downloads](https://img.shields.io/nuget/dt/wk.SendCommand.svg)](https://www.nuget.org/packages/wk.SendCommand)

Send command to PostgreSQL server

## Installation

```bash
dotnet tool install -g wk.SendCommand
```

## Usage

```bash
wk-send-command         \
    --user postgres     \
    --password 1234     \
    --database postgres \
    --sql "select to_tsvector('english', 'This will also find related word such ') @@ to_tsquery('english', 'words')"
```