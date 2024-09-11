using System;
using System.Collections.Generic;
using System.Xml;

class ProgrammingLanguage
{
    public string Name { get; private set; }
    public int Appeared { get; private set; }
    public string Creator { get; private set; }

    public ProgrammingLanguage(string name, int appeared, string creator)
    {
        Name = name;
        Appeared = appeared;
        Creator = creator;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Мова: {Name}, Рiк появи: {Appeared}, Автор: {Creator}");
    }
}

class Languages
{
    public List<ProgrammingLanguage> LanguageList { get; private set; }

    public Languages()
    {
        LanguageList = new List<ProgrammingLanguage>();
    }

    public void AddLanguage(ProgrammingLanguage lang)
    {
        LanguageList.Add(lang);
    }

    public void ShowAllLanguages()
    {
        foreach (var lang in LanguageList)
        {
            lang.PrintInfo();
        }
    }
}

static class XmlToLanguagesConverter
{
    public static Languages Convert(string xmlFilePath)
    {
        Languages languages = new Languages();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlFilePath);

        // Проходимо по кожному елементу "lang"
        foreach (XmlNode node in xmlDoc.DocumentElement.SelectNodes("lang"))
        {
            string name = node.Attributes["name"].Value;
            int appeared = int.Parse(node["appeared"].InnerText);
            string creator = node["creator"].InnerText;

            // Додаємо мову в список
            languages.AddLanguage(new ProgrammingLanguage(name, appeared, creator));
        }

        return languages;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Шлях до XML-файлу
        string xmlFilePath = @"C:\languages.xml";

        // Перетворюємо XML на об'єкт класу Languages
        Languages languages = XmlToLanguagesConverter.Convert(xmlFilePath);

        // Виводимо інформацію про всі мови
        languages.ShowAllLanguages();
    }
}