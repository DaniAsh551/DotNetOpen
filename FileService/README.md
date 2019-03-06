# DotNetOpen.FileService
A library to manage files in an organized, efficient and simple manner.

# Classes
  ## FileService
<p>The default <a href="https://github.com/DaniAsh551/DotNetOpen/blob/master/FileService/DotNetOpen.FileService.Abstractions/Interfaces/IFileService.cs">IFileService</a> implementation which is provided. Requires an instance of <a href="https://github.com/DaniAsh551/DotNetOpen/blob/master/FileService/DotNetOpen.FileService.Abstractions/Interfaces/IFileServiceConfig.cs">IFileServiceConfig</a> in order to work.</p>

  ## FileServiceConfig
<p>The default <a href="https://github.com/DaniAsh551/DotNetOpen/blob/master/FileService/DotNetOpen.FileService.Abstractions/Interfaces/IFileServiceConfig.cs">IFileServiceConfig</a> implementation provided.

  ## FileMetaData
  <p>The default <a href="https://github.com/DaniAsh551/DotNetOpen/blob/master/FileService/DotNetOpen.FileService.Abstractions/Interfaces/IFileMetaData.cs">IFileMetaData</a> implementation provided.

# Enums
  ## FileSizeUnit
  <p>This Enum is not actually physically included in this package, but is rather inherited from the <a href="https://github.com/DaniAsh551/DotNetOpen/tree/master/FileService/DotNetOpen.FileService.Abstractions">DotNetOpen.FileService.Abstractions</a> and defines the units of size used throughout the whole package.

# HOWTO
<p>In order to use the functionalities provided within the library, you must create or load an instance of IFileServiceConfig</p>

 
<pre><code class='language-cs'>
  var allowedExtensions = new string[] {"zip", "mp3", "png", "jpg", "gif"};
  var config = new FileServiceConfig(
    stream => "application/octet-stream", 
    28,
    FileSizeUnit.MB,
    Path.Combine(Environment.CurrentDirectory, "Files"),
    allowedExtensions);
  var fileService = new FileService(config);
</code></pre>

  <p>Now, imagining that you have a zip file in a stream named 'stream' in order to save a stream into a file</p>
<pre><code class='language-cs'>
  var fileMetaData = await fileService.CreateAsync("thefile.zip", "zip", stream);
</code></pre>

  <p>This gives you a variable of type 'IFileMetaData' which contains the basic information of the file which we just saved.</p>
  <p>Now imagining that you want to get the data of the newly created file into a stream named 'strm'</p>
<pre><code class='language-cs'>
  var metaData = fileService.GetFile("thefile.zip", "zip");
</code></pre>
