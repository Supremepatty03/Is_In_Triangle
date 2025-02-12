using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_c_
{
    public interface IFileReader
    {
        string[] ReadFromFile (string filePath);
    }
    public interface IFileWriter
    {
        void WriteDataToFile(string filePath, string[] data);
        void WriteResultToFile(string filePath, string result);
        int WhatToDoWithData(string filePath);

    }
    public class FileReader : IFileReader
    {
        public string[] ReadFromFile(string filePath)
        {

            if (!File.Exists(filePath)) { throw new FileNotFoundException("Файл не найден", filePath); }

            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length != 4)
            {
                throw new InvalidDataException($"Ожидалось 4 строки на вход, а получено {lines.Length}.");
            }
            return lines;
        }
    }
    public class FileWriter : IFileWriter
    {
        enum FileChoise
        {
            REWRITE = 1,
            ADD = 2,
            CANCEL = 3
        }

        void EnsureFileNDirectoryExists(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close(); // Создает файл и сразу закрывает поток, чтобы избежать блокировки
            }
        }
        public int WhatToDoWithData(string filePath)
        {

            AdditionalInfo.FileMenu(filePath);

            int choice = InputHandler.GetInput<int>("Введите ваш выбор: ");

            return choice;
        }

        public void ApplyingChoice(string filePath, int choice, string[] data)
        {
            if (choice == (int)FileChoise.REWRITE)
            {
                File.WriteAllLines(filePath, data);
            }
            else if (choice == (int)FileChoise.ADD)
            {
                File.AppendAllLines(filePath, data);
            }
            else if (choice == (int)FileChoise.CANCEL)
            {
                return;
            }

        }
        enum SaveOptions
        {
            ONLYINPUT = 1,
            RESULT = 2,
            NOSAVE = 3
        }
        public void SavingToFile(string[] data, string result)
        {
            AdditionalInfo.SaveToFile();
            bool flag = true;
            while (flag) { 
                int choice = InputHandler.GetInput<int>(" - ");
                string filepath;
                
                switch (choice)
                {
                    case 1:
                        Console.Write(" Введите путь сохранения файла: ");
                        filepath = Console.ReadLine();
                        WriteDataToFile(filepath, data);
                        flag = false;
                        break;
                    case 2:
                        Console.Write(" Введите путь сохранения файла: ");
                        filepath = Console.ReadLine();
                        WriteResultToFile(filepath, result);
                        flag = false;
                        break;
                    case 3:
                        Console.WriteLine("Данные не сохранены.");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        flag = false;
                        break;
                }
            }




        }
        public void WriteDataToFile(string filePath, string[] data)
        {

            EnsureFileNDirectoryExists(filePath);
            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
            {
                int choice = WhatToDoWithData(filePath);
                ApplyingChoice(filePath, choice, data);
            }
            File.WriteAllLines(filePath, data);
        }

        public void WriteResultToFile(string filePath, string result)
        {
            string[] converted = new string[] { result };
            EnsureFileNDirectoryExists(filePath);
            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
            {
                int choice = WhatToDoWithData(filePath);
                ApplyingChoice(filePath, choice, converted);
            }
            File.WriteAllText(filePath, result);
        }
    }
}
