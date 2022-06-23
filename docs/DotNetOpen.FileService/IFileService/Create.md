# IFileService.Create method (1 of 4)

Create a new File and Get the file from the FileSystem synchronously

```csharp
public IFileMetaData Create(string fileType, byte[] bytes, bool throwOnException = true)
```

| parameter | description |
| --- | --- |
| fileType | The type of the file |
| bytes | The contents of the file |
| throwOnException | Indicates whether an Exception should be thrown upon errors. Default:true |

## Return Value

IFileMetaData

## See Also

* interface [IFileMetaData](../IFileMetaData.md)
* interface [IFileService](../IFileService.md)
* namespace [DotNetOpen.FileService](../../DotNetOpen.FileService.Abstractions.md)

---

# IFileService.Create method (2 of 4)

Create a new File and Get the file from the FileSystem synchronously

```csharp
public IFileMetaData Create(string fileType, Stream stream, bool throwOnException = true)
```

| parameter | description |
| --- | --- |
| fileType | The type of the file |
| stream | The contents of the file |
| throwOnException | Indicates whether an Exception should be thrown upon errors. Default:true |

## Return Value

IFileMetaData

## See Also

* interface [IFileMetaData](../IFileMetaData.md)
* interface [IFileService](../IFileService.md)
* namespace [DotNetOpen.FileService](../../DotNetOpen.FileService.Abstractions.md)

---

# IFileService.Create method (3 of 4)

Create a new File and Get the file from the FileSystem synchronously

```csharp
public IFileMetaData Create(string fileName, string fileType, byte[] bytes, 
    bool throwOnException = true)
```

| parameter | description |
| --- | --- |
| fileName | Name of the file |
| fileType | The type of the file |
| bytes | The contents of the file |
| throwOnException | Indicates whether an Exception should be thrown upon errors. Default:true |

## Return Value

IFileMetaData

## See Also

* interface [IFileMetaData](../IFileMetaData.md)
* interface [IFileService](../IFileService.md)
* namespace [DotNetOpen.FileService](../../DotNetOpen.FileService.Abstractions.md)

---

# IFileService.Create method (4 of 4)

Create a new File and Get the file from the FileSystem synchronously

```csharp
public IFileMetaData Create(string fileName, string fileType, Stream stream, 
    bool throwOnException = true)
```

| parameter | description |
| --- | --- |
| fileName | Name of the file |
| fileType | The type of the file |
| stream | The contents of the file |
| throwOnException | Indicates whether an Exception should be thrown upon errors. Default:true |

## Return Value

IFileMetaData

## See Also

* interface [IFileMetaData](../IFileMetaData.md)
* interface [IFileService](../IFileService.md)
* namespace [DotNetOpen.FileService](../../DotNetOpen.FileService.Abstractions.md)

<!-- DO NOT EDIT: generated by xmldocmd for DotNetOpen.FileService.Abstractions.dll -->