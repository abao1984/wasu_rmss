using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
///FileUtil 的摘要说明
/// </summary>
public class FileUtil
{
    public static string GetFileNameWithoutExtension(string path)
    {
        return Path.GetFileNameWithoutExtension(path);
    }
    public static string GetFileName(string path)
    {
        return Path.GetFileName(path);
    }
    public static void MoveFile(string sourcePath, string targetPath)
    {
        FileInfo info = new FileInfo(sourcePath);
        info.CopyTo(targetPath, true);
        info.Delete();
    }
    public static void CopyFile(string sourcePath, string targetPath)
    {
        FileInfo info = new FileInfo(sourcePath);
        info.CopyTo(targetPath, true);
    }
    public static bool SaveBinaryStreamToFile(byte[] bytesFile, string filepath)
    {

        //保证存在指定的路径
        EnsureExistsPath(filepath);
        FileStream outputStream = null;
        BufferedStream bs = null;
        try
        {
            outputStream = new FileStream(filepath, FileMode.Create);
            bs = new BufferedStream(outputStream);
            bs.Write(bytesFile, 0, bytesFile.Length);
            bs.Flush();

            outputStream.Flush();
            outputStream.Flush();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (null != outputStream)
                outputStream.Close();
            if (null != bs)
                bs.Close();

        }
    }

    public static void EnsureExistsPath(string filepath)
    {
        string directory = Path.GetDirectoryName(filepath);
        if (!System.IO.Directory.Exists(directory))
        {
            System.IO.Directory.CreateDirectory(directory);
        }
    }

    /// <summary>
    /// 根据上传文件获取文件的二进制表达形式
    /// </summary>
    /// <param name="filePosted"></param>
    /// <returns></returns>
    public static byte[] GetPostedFileToBytes(HttpPostedFile filePosted)
    {
        // If no file was uploaded then you probably want to inform the user to select a file
        // before clicking the upload button.
        if (filePosted != null)
        {
            try
            {
                return GetFileBinaryFormatBase(filePosted.InputStream);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != filePosted.InputStream)
                    filePosted.InputStream.Close();
            }
        }
        return null;
    }

    /// <summary>
    /// 保存文件到系统路径
    /// </summary>
    /// <param name="filePosted"></param>
    /// <returns></returns>
    public static void SavePostedFileToSystem(HttpPostedFile filePosted, string pathname)
    {
        try
        {
            EnsureExitsDirectory(Path.GetDirectoryName(pathname));
            filePosted.SaveAs(pathname);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void SavePostedFileToSystem(HttpPostedFile filePosted, string targetDirectory, string filename)
    {
        SavePostedFileToSystem(filePosted, targetDirectory + filename);
    }
    public static void SavePostedFileToSystem(HttpPostedFile filePosted, string targetDirectory, string withoutExtfilename, string Ext)
    {
        SavePostedFileToSystem(filePosted, targetDirectory + withoutExtfilename + EnsureExtHasDot(Ext));
    }


    /// <summary>
    /// 把二进制留数据保存到指定的路进的文件中
    /// </summary>
    /// <param name="data">文件二进制流数据</param>
    /// <param name="filePath">文件路径,可能是相对的也可能是绝对的</param>
    /// <returns>返回文件创建成功后的决定路径</returns>
    public static string SaveBinaryStreamFile(byte[] bytesFile, string filePath)
    {
        return SaveBinaryStreamFile(bytesFile, Path.GetDirectoryName(filePath), Path.GetFileName(filePath));
    }
    public static string SaveBinaryStreamFile(byte[] bytesFile, string targetDirectory, string fileName)
    {
        if (bytesFile == null || bytesFile.Length == 0 || fileName == null || fileName.Trim() == string.Empty)
        {
            return string.Empty;
        }
        //保证存在指定的路径
        EnsureExitsDirectory(targetDirectory);
        FileStream outputStream = null;
        string filepath;
        try
        {
            if (targetDirectory == null || targetDirectory.Trim() == string.Empty)
            {
                filepath = fileName;
            }
            else
            {
                filepath = targetDirectory + fileName;
            }
            outputStream = new FileStream(filepath, FileMode.Create);
            outputStream.Write(bytesFile, 0, bytesFile.Length);
            outputStream.Flush();
            return Path.GetFullPath(filepath);
        }
        catch//(Exception ex)
        {
            return string.Empty;
        }
        finally
        {
            if (null != outputStream)
                outputStream.Close();
        }
    }
    public static string SaveBinaryStreamFile(byte[] bytesFile, string targetDirectory, string withoutExtfilename, string Ext)
    {
        return SaveBinaryStreamFile(bytesFile, targetDirectory + withoutExtfilename + EnsureExtHasDot(Ext));
    }

    /// <summary>
    /// 保证存在指定的路径
    /// </summary>
    /// //可能发生一系列的异常
    /// <param name="targetDirectory"></param>
    /// <paramref name="IOException"><seealso cref=" System.IO.IOException"/>由 path 指定的目录是只读的或不为空</paramref>
    /// <paramref name="UnauthorizedAccessException"><seealso cref=" System.IO.UnauthorizedAccessException"/>调用方没有所要求的权限</paramref>
    /// <paramref name="ArgumentException"><seealso cref=" System.IO.ArgumentException"/>是一个零长度字符串，仅包含空白或者包含一个或多个由 InvalidPathChars 定义的无效字符</paramref>
    /// <paramref name="ArgumentNullException"><seealso cref=" System.IO.ArgumentNullException"/>path 为空引用</paramref>
    /// <paramref name="PathTooLongException"><seealso cref=" System.IO.PathTooLongException"/>指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。 </paramref>
    /// <paramref name="DirectoryNotFoundException"><seealso cref=" System.IO.DirectoryNotFoundException"/>指定的路径无效，比如在未映射的驱动器上。</paramref>
    /// <paramref name="NotSupportedException"><seealso cref=" System.IO.NotSupportedException"/>试图仅通过冒号 (:) 字符创建目录</paramref>
    private static void EnsureExitsDirectory(string targetDirectory)
    {
        if (!Directory.Exists(targetDirectory))
        {
            Directory.CreateDirectory(targetDirectory);
        }

    }

    /// <summary>
    /// 获取指定文件名的二进制流byte[]表现形式
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns>如果文件存在并且可读,返回二进制流,否则返回null</returns>
    public static byte[] GetFileBinaryFormat(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }
        FileStream fs = null;
        try
        {
            fs = new FileStream(filePath, FileMode.Open);
            return GetFileBinaryFormatBase(fs);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (null != fs)
                fs.Close();
        }
    }
    public static byte[] GetFileBinaryFormat(string sourceDirectory, string filename)
    {
        return GetFileBinaryFormat(sourceDirectory + filename);
    }

    public static byte[] GetFileBinaryFormat(string sourceDirectory, string withoutExtfilename, string Ext)
    {
        return GetFileBinaryFormat(sourceDirectory + withoutExtfilename + EnsureExtHasDot(Ext));
    }

    /// <summary>
    /// 基本的文件流转化成byte[]
    /// </summary>
    /// <param name="inputStream">基本文件流</param>
    /// <returns>二进制的byte[]表现形式</returns>
    public static byte[] GetFileBinaryFormatBase(Stream inputStream)
    {
        BufferedStream bs = null;
        byte[] filebytes = null;
        try
        {
            bs = new BufferedStream(inputStream);

            if (bs.CanRead)
            {
                filebytes = new byte[bs.Length];
                bs.Read(filebytes, 0, (int)bs.Length);
            }
        }
        catch { throw; }
        finally { if (bs != null) bs.Close(); }
        return filebytes;

    }


    private static string EnsureExtHasDot(string ext)
    {
        ext = Convert.ToString(ext).Trim();
        if (ext.IndexOf(".", 0) != 0)
        {
            ext = "." + ext;
        }
        return ext;
    }
}
