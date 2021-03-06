# FileService.Update method (1 of 2)

Update an already existing File and Get the file from the FileSystem synchronously

```csharp
public IFileMetaData Update(string fileName, string fileType, byte[] bytes, 
    bool throwOnException = true, bool throwOnNotFound = true)
```

| parameter | description |
| --- | --- |
| fileName | Name of the file |
| fileType | The Type of the file |
| bytes | Data to be written into the file |
| throwOnException | Indicates whether an Exception should be thrown upon errors. Default:true |
| throwOnNotFound | Indicates whether an Exeption should be thrown if the file is not found.Default:true |

## Return Value

IFileMetaData

## See Also

* class [FileService](../FileService.md)
* namespace [DotNetOpen.FileService](../../DotNetOpen.FileService.md)

---

# FileService.Update method (2 of 2)

Update an already existing File and Get the file from the FileSystem synchronously

```csharp
public IFileMetaData Update(string fileName, string fileType, Stream stream, 
    bool throwOnException = true, bool throwOnNotFound = true)
```

| parameter | description |
| --- | --- |
| fileName | Name of the file |
| fileType | The Type of the file |
| stream | Data to be written into the file |
| throwOnException | Indicates whether an Exception should be thrown upon errors. Default:true |
| throwOnNotFound | Indicates whether an Exeption should be thrown if the file is not found.Default:true |

## Return Value

IFileMetaData

## See Also

* class [FileService](../FileService.md)
* namespace [DotNetOpen.FileService](../../DotNetOpen.FileService.md)

<!-- DO NOT EDIT: generated by xmldocmd for DotNetOpen.FileService.dll -->
