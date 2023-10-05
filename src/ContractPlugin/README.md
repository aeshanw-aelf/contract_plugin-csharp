# ContractPlugin

## Introduction

This is the actual protoc-plugin executable that is to be built & distributed to clients (i.e protoc users).

## Build

``
dotnet build
``

Though to build a single platform-specific binary executable you would need to use this command:
``
dotnet publish -c Release -r osx.13-arm64 --self-contained true
``
