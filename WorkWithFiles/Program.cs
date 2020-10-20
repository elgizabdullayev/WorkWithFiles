
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace WorkWithFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\WorkWithFiles\data.txt";
            string newpath = @"D:\WorkWithFiles\newdata.txt";
            ArrayList data = new ArrayList();
            List<Client> clients = new List<Client>();
            List<string> maindata = new List<string>();
            List<string> newdata = new List<string>();
            string temp;
            try
            {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    while ((temp = sr.ReadLine()) != null)
                    {
                        data.Add(temp);
                    }
                }
                foreach (string d in data)
                {
                    string[] DataList = d.Split("; ", StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < DataList.Length; i++)
                    {
                        maindata.Add(DataList[i]);
                    }
                }
                int count2 = 0;
                for (int i = 0; i < maindata.Count / 3; i++)
                {
                    clients.Add(new Client());
                }
                for (int count = 0; count < clients.Count; count++)
                {
                    clients[count].id = maindata[count2];
                    clients[count].passportnumber = maindata[count2 + 1];
                    clients[count].payment = maindata[count2 + 2];
                    count2 += 3;
                }

            }
            catch
            {
                Console.WriteLine("Формат данных неверен.");
            }

            bool Exit = false;
            do
            {
                Console.WriteLine("Меню:\n1.Показать считанные данные.\n2.Изменить данные.\n3.Выход.\nВведите номер пункта меню для перехода.");
                int select = InputInfo.InputNavigate1();
                switch (select)
                {
                    case 1:
                        {

                            foreach (Client c in clients)
                            {
                                c.ShowData();
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Какую строчку вы хотите изменить?");
                            int num = InputInfo.InputInt();
                            if (num < clients.Count)
                            {
                                bool NewEnter = false;
                                do
                                {
                                    Console.WriteLine("Что вы хотите изменить: \n1.ID \n2.Passport number\n3.Payment\nВыберите номер перед пунктом меню для перехода.");
                                    int num2 = InputInfo.InputNavigate1();
                                    switch (num2)
                                    {
                                        case 1:
                                            {
                                                Console.WriteLine("Введите новый ID.");
                                                string ins = Console.ReadLine();
                                                clients[num-1].id = ins;
                                                Console.WriteLine("Хотите изменить что-то еще? 1. Да; 2. Нет.");
                                                int select1 = InputInfo.InputNavigate2();
                                                if(select1 == 2)
                                                {
                                                    NewEnter = true;
                                                }
                                                break;
                                            }
                                        case 2:
                                            {
                                                Console.WriteLine("Введите новый Passport Number.");
                                                string ins = Console.ReadLine();
                                                clients[num-1].passportnumber = ins;
                                                Console.WriteLine("Хотите изменить что-то еще? 1. Да; 2. Нет.");
                                                int select1 = InputInfo.InputNavigate2();
                                                if (select1 == 2)
                                                {
                                                    NewEnter = true;
                                                }
                                                break;
                                            }
                                        case 3:
                                            {
                                                Console.WriteLine("Введите новый Payment.");
                                                string ins = Console.ReadLine();
                                                clients[num-1].payment = ins;
                                                Console.WriteLine("Хотите изменить что-то еще? 1. Да; 2. Нет.");
                                                int select1 = InputInfo.InputNavigate2();
                                                if (select1 == 2)
                                                {
                                                    NewEnter = true;
                                                }
                                                break;
                                            }
                                    }
                                }
                                while (NewEnter == false);
                                using (StreamWriter sw = File.CreateText(newpath))
                                {
                                    foreach (Client c in clients)
                                    {
                                        sw.WriteLine($"{c.id}; {c.passportnumber};{c.payment}");
                                    }
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                            Exit = true;
                            break;
                        }
                }
            }
            while (Exit == false);
            
        }

        class Client
        {
            public string id { get; set; }
            public string passportnumber { get; set; }
            public string payment { get; set; }
            public Client()
            {

            }
            public void ShowData()
            {
                Console.WriteLine($"ID: {id}, passport number: {passportnumber}, payment: {payment}.");
            }
        }
        class InputInfo
        {
            public static string InputName()
            {
                string name;
                bool isRight = false;
                do
                {
                    name = Console.ReadLine();
                    int count = 0;
                    for (int i = 0; i < name.Length; i++)
                    {
                        for (int j = 0; j <= 9; j++)
                        {
                            if (name[i] == j.ToString()[0])
                            {
                                isRight = false;
                                count += 1;
                                break;
                            }
                            if (count == 0)
                            {
                                isRight = true;
                            }
                        }
                    }
                    if (isRight == false)
                    {
                        Console.WriteLine("Введите имя, состоящее из букв. Попробуйте снова.");
                    }
                }
                while (isRight == false);
                return name;
            }
            public static int InputInt()
            {
                int number;
                bool isRight = false;
                do
                {
                    if (Int32.TryParse(Console.ReadLine(), out number))
                    {
                        isRight = true;
                    }
                    else
                    {
                        Console.WriteLine("Введено неверное значение. Попробуйте снова.");
                    }
                }
                while (isRight == false);
                return number;
            }
            public static int InputNavigate1()
            {
                int number;
                bool isRight = false;
                do
                {
                    if (Int32.TryParse(Console.ReadLine(), out number))
                    {
                        if (number > 0 && number <= 4)
                        {
                            isRight = true;
                        }
                        else
                        {
                            Console.WriteLine("Введите одну из цифр: 1, 2, 3, 4.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Введено неверное значение. Попробуйте снова.");
                    }
                }
                while (isRight == false);
                return number;

            }
            public static int InputNavigate2()
            {
                int number;
                bool isRight = false;
                do
                {
                    if (Int32.TryParse(Console.ReadLine(), out number))
                    {
                        if (number > 0 && number <= 2)
                        {
                            isRight = true;
                        }
                        else
                        {
                            Console.WriteLine("Введите: 1 или 2");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Введено неверное значение. Попробуйте снова.");
                    }
                }
                while (isRight == false);
                return number;
            }

        }
    }
}
