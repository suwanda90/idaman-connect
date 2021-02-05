using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ApplicationCore.Helpers.File.Model;

namespace ApplicationCore.Helpers.File
{
    public static class FileHelper
    {
        public static double ConvertBytesToMegabytes(this long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        public static double ConvertKilobytesToMegabytes(this long kilobytes)
        {
            return kilobytes / 1024f;
        }

        public static async Task<IList<string>> PostFileAsync(UploadFile model, string baseDir)
        {
            var result = new List<string>();

            if (model.File.Length > 0)
            {
                if (!Directory.Exists(baseDir + model.FkTransactionId))
                {
                    Directory.CreateDirectory(baseDir + model.FkTransactionId);
                }

                var fileExtension = Path.GetExtension(model.File.FileName);
                var fileName = Path.GetFileNameWithoutExtension(model.File.FileName).ToLower().Replace(" ", "_").Replace("-", "_") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                var filePath = Path.Combine(baseDir + model.FkTransactionId);
                var fileDocument = Path.Combine(filePath, fileName);
                var fileSize = model.File.Length;
                var mimeType = model.File.ContentType;

                using var stream = new FileStream(fileDocument, FileMode.Create);
                await model.File.CopyToAsync(stream);

                result = new List<string>
                {
                    fileName,
                    filePath,
                    mimeType,
                    fileExtension,
                    fileSize.ToString()
                };
            }

            return result;
        }

        public static async Task<IList<string>> PutFileAsync(UploadFile model, string oldFileName, string baseDir)
        {
            var result = new List<string>();

            if (model.File.Length > 0)
            {
                var fileExtension = Path.GetExtension(model.File.FileName);
                var fileName = Path.GetFileNameWithoutExtension(model.File.FileName).ToLower().Replace(" ", "_").Replace("-", "_") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                var filePath = Path.Combine(baseDir + model.FkTransactionId);
                var fileDocument = Path.Combine(filePath, fileName);
                var fileSize = model.File.Length;
                var mimeType = model.File.ContentType;

                using var stream = new FileStream(fileDocument, FileMode.Create);
                await model.File.CopyToAsync(stream);

                await DeleteFileAsync(filePath, oldFileName);

                result = new List<string>
                {
                    fileName,
                    filePath,
                    mimeType,
                    fileExtension,
                    fileSize.ToString()
                };
            }

            return result;
        }

        public static async Task DeleteFileAsync(string filePath, string fileName)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                if (System.IO.File.Exists(Path.Combine(filePath, fileName)))
                {
                    var directory = new DirectoryInfo(filePath);
                    if (directory.GetFiles().Length == 1)
                    {
                        Directory.Delete(filePath, true);
                    }
                    else
                    {
                        using FileStream stream = new FileStream(Path.Combine(filePath, fileName), FileMode.Truncate, FileAccess.Write, FileShare.Delete, 4096, true);
                        await stream.FlushAsync();
                        System.IO.File.Delete(Path.Combine(filePath, fileName));
                    }
                }
            }
        }
    }
}
