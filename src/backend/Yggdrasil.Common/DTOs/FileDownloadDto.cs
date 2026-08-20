namespace Yggdrasil.Common.DTOs;

public class FileDownloadDto
{
    public byte[] FileContent { get; set; }
    public string ContentType { get; set; }
    public string FileName { get; set; }

    public FileDownloadDto(byte[] fileContent, string contentType, string fileName)
    {
        FileContent = fileContent;
        ContentType = contentType;
        FileName = fileName;
    }
}