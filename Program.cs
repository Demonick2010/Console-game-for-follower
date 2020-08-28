using System;
using System.IO;
using System.Text;
using static System.Console;

namespace ConsoleGame
{
    class Program
    {
        /// <summary>
        /// Точка входа в программу
        /// </summary>
        /// <param name="args">Необязательные аргументы</param>
        static void Main(string[] args)
        {
            // Устанавливаем кодировку консоли в UTF-8
            OutputEncoding = Encoding.UTF8;

            // Объявляем переменную для хранения имени персонажа, которое возвращается из методов меню
            string choise = string.Empty;

            // Инкапсулируем ряд методов вывода меню
            // Получая имя персонажа
            Menu.ShowMenu(out choise);
            
            // Создаём новый экземпляр класса актта, передавая в него, в качестве аргумента, выбранное имя персонажа
            Acts acts = new Acts(choise);

            // Инкапсулируем ряд методов актов и ответов
            acts.StartAct();
        }
    }
}
