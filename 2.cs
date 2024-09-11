using System;
using System.Collections.Generic;
using System.Xml;

class Personal
{
    public string Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Nickname { get; private set; }
    public decimal Salary { get; private set; }

    public Personal(string id, string firstName, string lastName, string nickname, decimal salary)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Nickname = nickname;
        Salary = salary;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"ID: {Id}, Прiзвище: {LastName}, Iм'я: {FirstName}, Нiк: {Nickname}, Зарплата: {Salary}");
    }
}

class Firma
{
    public List<Personal> PersonalList { get; private set; }

    public Firma()
    {
        PersonalList = new List<Personal>();
    }

    public void AddPersonal(Personal person)
    {
        PersonalList.Add(person);
    }

    public void ShowAllPersonal()
    {
        foreach (var person in PersonalList)
        {
            person.PrintInfo();
        }
    }
}

static class XmlToFirmaConverter
{
    public static Firma Convert(string xmlFilePath)
    {
        Firma firma = new Firma();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlFilePath);

        // Проходимо по кожному елементу "personal"
        foreach (XmlNode node in xmlDoc.DocumentElement.SelectNodes("personal"))
        {
            string id = node.Attributes["id"].Value;
            string firstName = node["firstname"].InnerText;
            string lastName = node["lastname"].InnerText;
            string nickname = node["nickname"].InnerText;
            decimal salary = decimal.Parse(node["salary"].InnerText);

            // Додаємо особу в список
            firma.AddPersonal(new Personal(id, firstName, lastName, nickname, salary));
        }

        return firma;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Шлях до XML-файлу
        string xmlFilePath = @"C:\firma.xml";

        // Перетворюємо XML на об'єкт класу Firma
        Firma firma = XmlToFirmaConverter.Convert(xmlFilePath);

        // Виводимо інформацію про всіх працівників
        firma.ShowAllPersonal();
    }
}