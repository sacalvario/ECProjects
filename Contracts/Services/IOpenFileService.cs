﻿
namespace ProjectManager.Contracts.Services
{
    public interface IOpenFileService
    {
        string FileName { get; set; }
        string Path { get; set; }
        bool OpenFileDialog();
        bool SaveFileDialog(string filename);
        bool SaveFileExportDialog();
    }
}
