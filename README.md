## Send Command

[![Build Status](https://dev.azure.com/wk-j/send-command/_apis/build/status/wk-j.send-command?branchName=master)](https://dev.azure.com/wk-j/send-command/_build/latest?definitionId=23&branchName=master)
[![NuGet](https://img.shields.io/nuget/v/wk.SendCommand.svg)](https://www.nuget.org/packages/wk.SendCommand)

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