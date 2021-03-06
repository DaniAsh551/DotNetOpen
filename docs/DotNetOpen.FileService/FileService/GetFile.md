# FileService.GetFile method

Get an already existing file on the FileSystem

```csharp
public IFileMetaData GetFile(string fileName, string fileType, bool throwOnException = true, 
    bool throwOnNotFound = true)
```

| parameter | description |
| --- | --- |
| fileName | Name of the file |
| fileType | The type of the file |
| throwOnException | Indicates whether an Exception should be thrown upon errors. Default:true |
| throwOnNotFound | Indicates whether an Exeption should be thrown if the file is not found. Default:true |

## Return Value

IFileMetaData

## See Also

* class [FileService](../FileService.md)
* namespace [DotNetOpen.FileService](../../DotNetOpen.FileService.md)

<!-- DO NOT EDIT: generated by xmldocmd for DotNetOpen.FileService.dll -->
