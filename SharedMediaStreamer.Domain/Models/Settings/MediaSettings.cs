namespace SharedMediaStreamer.Domain.Models.Settings
{
    public class MediaSettings
    {
        public string FolderPath { get; set; }
        public string FileName { get; set; }
        public short BufferSizeInMB { get; set; }
    }
}
