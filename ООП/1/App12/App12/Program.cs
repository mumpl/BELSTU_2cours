using System;
using System.IO;
using System.IO.Compression;

public class AESLog
{
    private readonly string logFilePath;
    public AESLog(string logFilePath)
    {
        this.logFilePath = logFilePath;
        if (!File.Exists(logFilePath))
        {
            File.Create(logFilePath).Close();
        }
    }
    public void WriteLog(string action, string details)
    {
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {action} | {details}";
            writer.WriteLine(logEntry);
        }
    }
    public void ReadLogs()
    {
        if (File.Exists(logFilePath))
        {
            Console.WriteLine("--- Логи ---");
            string[] logs = File.ReadAllLines(logFilePath); 
            foreach(var log in logs)
            {
                Console.WriteLine(log);
            }
        }
        else
        {
            Console.WriteLine("Файл лога отсутствует.");
        }
    }
    public void SearchLogs(string keyword)
    {
        if(File.Exists(logFilePath))
        {
            Console.WriteLine($"--- Результаты поиска по ключевому слову \"{keyword}\" ---");
            string[] logs = File.ReadAllLines(logFilePath);
            bool found = false; 
            foreach (var log in logs)
            {
                if (log.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(log);
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("Ничего не найдено.");
            }
        }
        else
        {
            Console.WriteLine("Файл лога отсутствует.");
        }
    }
    public void SearchLogsByDate(DateTime date)
    {
        if (File.Exists(logFilePath))
        {
            Console.WriteLine($"--- Логи за {date:yyyy-MM-dd} ---");
            string[] logs = File.ReadAllLines(logFilePath);
            bool found = false;
            foreach (var log in logs)
            {
                if (log.StartsWith(date.ToString("yyyy-MM-dd")))
                {
                    Console.WriteLine(log);
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("Записей за указанный день не найдено.");
            }
        }
        else
        {
            Console.WriteLine("Файл лога отсутствует.");
        }
    }
    public int CountLogs()
    {
        if (File.Exists(logFilePath))
        {
            string[] logs = File.ReadAllLines(logFilePath);
            return logs.Length;
        }
        return 0;
    }
    public void RetainLogsForCurrentHour()
    {
        if (File.Exists(logFilePath))
        {
            string[] logs = File.ReadAllLines(logFilePath);
            string currentHour = DateTime.Now.ToString("yyyy-MM-dd HH:");
            var filteredLogs = new List<string>();

            foreach (var log in logs)
            {
                if (log.StartsWith(currentHour))
                {
                    filteredLogs.Add(log);
                }
            }
            File.WriteAllLines(logFilePath, filteredLogs);
            Console.WriteLine("Логи обновлены: оставлены записи только за текущий час.");
        }
        else
        {
            Console.WriteLine("Файл лога отсутствует.");
        }
    }
}
public class AESDiskInfo
{
    public void ShowFreeSpace()
    {
        foreach(DriveInfo drive in DriveInfo.GetDrives())
        {
            if(drive.IsReady)
            {
                Console.WriteLine($"Диск: {drive.Name}; свободное место на диске: {drive.TotalFreeSpace /(1024*1024*1024)}Gb");
            }
        }
    }
    public void ShowFileSystemInfo()
    {
        foreach(DriveInfo drive in DriveInfo.GetDrives())
        {
            if(drive.IsReady)
            {
                Console.WriteLine($"Диск: {drive.Name}; Файловая система: {drive.DriveFormat}");
            }
        }
    }
    public void ShowDiskDetails()
    {
        foreach(DriveInfo drive in DriveInfo.GetDrives())
        {
            if(drive.IsReady)
            {
                Console.WriteLine($"Диск: {drive.Name}");
                Console.WriteLine($"Объём: {drive.TotalSize / (1024*1024*1024)}Gb");
                Console.WriteLine($"Доступный объём: {drive.AvailableFreeSpace / (1024*1024*1024)}Gb");
                Console.WriteLine($"Метка тома: {drive.VolumeLabel}");
            }
        }
    }
}
public class AESFileInfo
{
    public void ShowFileInfo(string filePath)
    {
        if (File.Exists(filePath))
        {
            FileInfo fileInfo = new FileInfo(filePath);
            Console.WriteLine($"Полный путь: {fileInfo.FullName}");
            Console.WriteLine($"Размер: {fileInfo.Length / 1024} Kb");
            Console.WriteLine($"Расширение: {fileInfo.Extension}");
            Console.WriteLine($"Имя файла: {fileInfo.Name}");
            Console.WriteLine($"Дата создания: {fileInfo.CreationTime}");
            Console.WriteLine($"Дата последнего изменения: {fileInfo.LastWriteTime}");
        }
        else
        {
            Console.WriteLine("Файл не найден!");
        }
    }
}
public class AESDirInfo
{
    public void ShowDirectoryInfo(string directoryPath)
    {
        if(!Directory.Exists(directoryPath))
        {
            DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);
            Console.WriteLine($"Директория: {dirInfo.FullName}");
            Console.WriteLine($"Количество файлов: {dirInfo.GetFiles().Length}");
            Console.WriteLine($"Время создания: {dirInfo.CreationTime}");
            Console.WriteLine($"Количество поддиректорий: {dirInfo.GetDirectories().Length}");
            Console.WriteLine($"Список родительский директорий: ");
            DirectoryInfo parent = dirInfo.Parent;
            while (parent != null)
            {
                Console.WriteLine($" - {parent.FullName}");
                parent = parent.Parent;
            }
        }
        else
        {
            Console.WriteLine("Директория не найдена!");
        }
    }
}
public class AESFileManager
{
    public void InspectDiskAndSaveInfo(string drivePath, string inspectDirName)
    {
        try
        {
            if (!Directory.Exists(inspectDirName))
            {
                Directory.CreateDirectory(inspectDirName);
            }
            string infoFilePath = Path.Combine(inspectDirName, "aesdirinfo.txt");
            using (StreamWriter writer = new StreamWriter(infoFilePath))
            {
                DirectoryInfo driveInfo = new DirectoryInfo(drivePath);
                writer.WriteLine($"Директория: {driveInfo.FullName}");
                writer.WriteLine($"Список файлов:");
                foreach (var file in driveInfo.GetFiles())
                {
                    writer.WriteLine($" - {file.Name}");
                }
                writer.WriteLine($"Список папок:");
                foreach (var dir in driveInfo.GetDirectories())
                {
                    writer.WriteLine($" - {dir.Name}");
                }
            }
            string copiedFilePath = Path.Combine(inspectDirName, "aesdirinfo_copy.txt"); //определяет путь
            try
            {
                File.Copy(infoFilePath, copiedFilePath);
                File.Delete(infoFilePath);
                Console.WriteLine($"Информация сохранена и файл переименован в: {copiedFilePath}");
            }
            catch(IOException ex)
            {
                Console.WriteLine($"Ошибка при копировании файла: {ex.Message}");
            }
            catch(UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Ошибка доступа: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }  
    }
    public void CopyFilesWithextension(string sourceDir, string targetDir, string extension)
    {
        if(!Directory.Exists(targetDir))
        {
            Directory.CreateDirectory(targetDir);
        }
        var files = Directory.GetFiles(sourceDir, $"*{extension}");
        foreach (var file in files)
        {
            string fileName = Path.GetFileName(file);
            string targetPath = Path.Combine(targetDir, fileName);
            File.Copy(file, targetPath, true);
        }
        Console.WriteLine($"Файлы с расширением {extension} скопированы в {targetDir}");
    }
    public void MoveDirectory(string sourceDir, string targetDir)
    {
        string newTargetDir = Path.Combine(targetDir, Path.GetFileName(sourceDir));
        if (Directory.Exists(newTargetDir))
        {
            Directory.Delete(newTargetDir, true);
        }
        Directory.Move(sourceDir, newTargetDir);    
        Console.WriteLine($"Директория {sourceDir} перемещена в {newTargetDir}");
    }
    public void CreateAndExtractArchive(string sourceDir, string archivePath, string extractDir)
    {
        try
        {
            try
            {
                if (File.Exists(archivePath))
                {
                    File.Delete(archivePath);
                }
                ZipFile.CreateFromDirectory(sourceDir, archivePath);
                Console.WriteLine($"Архив {archivePath} создан.");
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Ошибка: Исходная директория {sourceDir} не найдена. {ex.Message}");
                return;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Ошибка доступа: {ex.Message}");
                return;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода-вывода: {ex.Message}");
                return;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ошибка при создании архива: {ex.Message}");
                return;
            }
            try
            {
                if (Directory.Exists(extractDir))
                {
                    Directory.Delete(extractDir, true);
                }
                ZipFile.ExtractToDirectory(archivePath, extractDir);
                Console.WriteLine($"Архив извлечен в {extractDir}");
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Ошибка: Целевая директория {extractDir} не найдена. {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Ошибка доступа при извлечении: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода-вывода при извлечении: {ex.Message}");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        string logFilepath = "aeslogfile.txt";
        AESLog logger = new AESLog(logFilepath);

        logger.WriteLog("Создание файла", @"Путь: D:\test.txt");
        logger.WriteLog("Удаление файла", @"Путь: D:\old.txt");
        logger.WriteLog("Создание директории", @"Путь: D:\AESInspect");
        logger.WriteLog("Копирование файлов", @"Исходный путь: D:\SourceFolder, Целевой путь: D:\AESFiles");
        logger.WriteLog("Создание архива", @"Путь архива: D:\AESInspect\AESFiles.zip");

        Console.WriteLine("Чтение всех логов:");
        logger.ReadLogs();

        Console.WriteLine("Введите ключевое слово для поиска в логах: ");
        string keyword = Console.ReadLine();
        logger.SearchLogs(keyword);

        Console.WriteLine("Введите дату для поиска записей (в формате yyyy-MM-dd):");
        string dateInput = Console.ReadLine();
        if (DateTime.TryParse(dateInput, out DateTime date))
        {
            logger.SearchLogsByDate(date);
        }
        else
        {
            Console.WriteLine("Некорректный формат даты.");
        }

        Console.WriteLine("Общее количество записей в логе:");
        int count = logger.CountLogs();
        Console.WriteLine($"Количество записей: {count}");

        Console.WriteLine("Оставляем только записи за текущий час...");
        logger.RetainLogsForCurrentHour();

        Console.WriteLine("Логи после обновления:");
        logger.ReadLogs();

        Console.WriteLine("--- Информация о дисках ---");
        AESDiskInfo diskInfo = new AESDiskInfo();
        diskInfo.ShowFreeSpace();
        diskInfo.ShowFileSystemInfo();
        diskInfo.ShowDiskDetails();

        Console.WriteLine("--- Информация о файле ---");
        AESFileInfo fileInfo = new AESFileInfo();
        Console.WriteLine("Введите путь к файлу: ");
        string filePath = Console.ReadLine();
        fileInfo.ShowFileInfo(filePath);

        Console.WriteLine("--- Информация о директории ---");
        AESDirInfo dirInfo = new AESDirInfo();
        Console.WriteLine("Введите путь к директории: ");
        string dirPath = Console.ReadLine();    
        dirInfo.ShowDirectoryInfo(dirPath);

        Console.WriteLine("--- Управление файлами ---");
        AESFileManager fileManager = new AESFileManager();

        string drivePath = @"D:\";
        string inspectDir = @"D:\AESInspect";
        fileManager.InspectDiskAndSaveInfo(drivePath, inspectDir);

        string sourseDir = @"D:\SourceFolder";
        string aesFilesDir = @"D:\AESFiles";
        Console.Write("Введите расширение файлов (например, .txt): ");
        string extension = Console.ReadLine();
        fileManager.CopyFilesWithextension(sourseDir, aesFilesDir, extension);

        fileManager.MoveDirectory(aesFilesDir, inspectDir);

        string archivePath = @"D:\AESInspect\AESFiles.zip";
        string extractDir = @"D:\ExtractedFiles";
        fileManager.CreateAndExtractArchive(aesFilesDir, archivePath, extractDir);
    }
}
