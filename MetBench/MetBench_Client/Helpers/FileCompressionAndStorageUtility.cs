using LiteDB;
using System;
using System.IO.Compression;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using Prism.Services.Dialogs;
using MetBench_BLL;
using MetBench_DAL;
using MetBench_Domain;
using System.Windows;
namespace MetBench_Client.Helpers
{
    public class FileCompressionAndStorageUtility
    {
        ApplicationRSerive applicationRSerive = new ApplicationRSerive();
        //解压文件的存储路径（默认系统的Temp路径）
        string SystemTemppath = Path.GetTempPath() + "\\MetBench";
        //存储路径下创建的存储文件夹名
        string folderunderpath = "MR_SUT";
        static byte[] CodeFile = null;
        //上传文件
        public void ProcessFileData(string filePath, string CodeName)
        {
            // 创建确认对话框并显示给用户
            var confirmResult = MessageBox.Show("确认上传文件吗？", "确认上传", MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);

            if (confirmResult == MessageBoxResult.Yes)
            {
                // 检查文件扩展名是否已经是 .zip
                if (Path.GetExtension(filePath).Equals(".zip", StringComparison.OrdinalIgnoreCase))
                {
                    // 存储压缩文件
                    //ProcessZipFileData(application, filePath, CodeName);
                    byte[] zipfiledata = ProcessZipFileData(filePath, CodeName);
                    CodeFile = zipfiledata;
                }
                else
                {
                    // 压缩并存储普通文件
                    byte[] filedata = ProcessNormalFileData(filePath, CodeName);
                    CodeFile = filedata;
                }
            }
            else
            {
                //用户取消上传，可以选择执行其他操作或不执行任何操作
            }
        }
        //撤回上传
        public void BacktoProcess(MetBench_Domain.Application application)
        {

            // 测回之前上传到据库的数据
            applicationRSerive.BackUpdateCode(application);
            // 弹出撤销成功的消息窗口
            MessageBox.Show("撤销上传成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        //存储普通文件
        public byte[] ProcessNormalFileData(string filePath, string CodeName)
        {
            ApplicationRSerive applicationRSerive = new ApplicationRSerive();
            // 压缩文件为 .zip文件
            string zipFilePath = CompressFile(filePath);
            // 将压缩文件数据读取为字节数组
            byte[] zipFileData = File.ReadAllBytes(zipFilePath);
            // 删除临时生成的压缩文件
            File.Delete(zipFilePath);
            // 弹出上传成功的消息窗口
            MessageBox.Show("文件上传成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            return zipFileData;
        }
        //存储压缩文件
        public byte[] ProcessZipFileData(string filePath, string CodeName)
        {
            ApplicationRSerive applicationRSerive = new ApplicationRSerive();
            // 将 ZIP 文件数据读取为字节数组
            byte[] zipFileData = File.ReadAllBytes(filePath);
            // 弹出上传成功的消息窗口
            MessageBox.Show("文件上传成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            return zipFileData;
        }
        // 压缩文件为 .zip
        static string CompressFile(string filePath)
        {
            string zipFilePath = Path.ChangeExtension(filePath, ".zip");

            // 检查目标路径是否已经存在同名文件
            if (File.Exists(zipFilePath))
            {
                // 如果存在，可以选择删除或重命名该文件，或者选择一个不同的目标路径
                // 这里选择在文件名后面添加一个数字来重命名
                int count = 1;
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(zipFilePath);
                string extension = Path.GetExtension(zipFilePath);
                do
                {
                    zipFilePath = Path.Combine(Path.GetDirectoryName(zipFilePath), $"{fileNameWithoutExtension}_{count}{extension}");
                    count++;
                }
                while (File.Exists(zipFilePath));
            }

            using (var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
            {
                zipArchive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
            }

            return zipFilePath;
        }
        //从数据库中获取选中行的压缩文件并解压至系统盘temp文件下新建的一个MR_SUT文件夹中
        public void ExtractAllZipFilesFromDatabase(MetBench_Domain.Application application)
        {
            ApplicationRSerive applicationRSerive = new ApplicationRSerive();
            // 创建缓存目录
            string cacheFolderPath = Path.Combine(SystemTemppath, folderunderpath);
            Directory.CreateDirectory(cacheFolderPath);

            // 获取选中的 application 的压缩文件
            byte[] zipFileData = applicationRSerive.GetZipCodeFileData(application);
            if (zipFileData != null)
            {
                // 创建临时文件路径以保存压缩文件数据
                string tempZipFilePath = Path.GetTempFileName();

                // 将压缩文件数据写入临时文件
                File.WriteAllBytes(tempZipFilePath, zipFileData);

                // 解压缩文件到缓存目录
                using (var zipArchive = ZipFile.OpenRead(tempZipFilePath))
                {
                    foreach (var entry in zipArchive.Entries)
                    {
                        string entryOutputFilePath = Path.Combine(cacheFolderPath, entry.FullName);
                        entry.ExtractToFile(entryOutputFilePath, true);
                        Console.WriteLine("文件成功解压并保存到路径：" + entryOutputFilePath);
                    }
                }

                // 删除临时生成的压缩文件
                File.Delete(tempZipFilePath);

                // 弹出上传成功的消息窗口
                MessageBox.Show("文件解压成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Console.WriteLine("没有代码文件记录！");
            }
        }
        //执行蜕变关系自动解压所关联的文件
        public void ExtractZipFilesFromDatabase(int applicationid)
        {
            ApplicationRSerive applicationRSerive = new ApplicationRSerive();
            // 创建缓存目录
            string cacheFolderPath = Path.Combine(SystemTemppath, folderunderpath);
            Directory.CreateDirectory(cacheFolderPath);

            // 获取选中的 application 的压缩文件
            byte[] zipFileData = applicationRSerive.GetZipCodeFileDataById(applicationid);
            if (zipFileData != null)
            {
                // 创建临时文件路径以保存压缩文件数据
                string tempZipFilePath = Path.GetTempFileName();

                // 将压缩文件数据写入临时文件
                File.WriteAllBytes(tempZipFilePath, zipFileData);

                // 解压缩文件到缓存目录
                using (var zipArchive = ZipFile.OpenRead(tempZipFilePath))
                {
                    foreach (var entry in zipArchive.Entries)
                    {
                        string entryOutputFilePath = Path.Combine(cacheFolderPath, entry.FullName);
                        entry.ExtractToFile(entryOutputFilePath, true);
                        Console.WriteLine("文件成功解压并保存到路径：" + entryOutputFilePath);
                    }
                }

                // 删除临时生成的压缩文件
                File.Delete(tempZipFilePath);
            }
            else
            {
                Console.WriteLine("没有被测程序的代码文件记录！");
            }
        }
        public byte[] GetFileData()
        {
            if (CodeFile != null)
            {
                return CodeFile;
            }
            else
            {
                return null;
            }
        }

    }

}