using System;
using Automation.Utilities;
using OpenQA.Selenium;
using NUnit.Framework;


namespace Funda_Assignment
{

    public static class SystemVariables
    {
        public static string ChromeDriverPath = @"ChromeDriver\";
        public static string URL = "https://www.funda.nl/";
        
    }

    public static class StaticMethods
    {
        public static string getCorrectPath(string localFolderPath)
        {


            string CurrentDir = AppDomain.CurrentDomain.BaseDirectory.ToString();

            int CurrentDirCount = CurrentDir.Length;

            int indexToRemove = CurrentDirCount - 10;
            string currentDir = Environment.CurrentDirectory;

            string output = AppDomain.CurrentDomain.BaseDirectory.Remove(indexToRemove, 10);

            string CorrectDirectory = output + @"ChromeDriver\";

            return CorrectDirectory;

        }
    }
}
