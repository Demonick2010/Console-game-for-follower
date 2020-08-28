﻿using System;
using static System.Console;

namespace ConsoleGame
{
    /// <summary>
    /// Класс вывода меню и обработки ввода игрока
    /// </summary>
    public static class Menu
    {
        /// <summary>
        /// Инкапсулируем логику меню и выполняем её
        /// </summary>
        /// <param name="person">Возвращает латинское имя персонажа</param>
        public static void ShowMenu(out string person)
        {
            // Объявляем временную переменную для хранения и возврата из метода имя персонажа
            // Начальное значение устанавливаем, как пустая строка
            person = string.Empty;

            // Вызываем приватный метод вывода стартового меню и получаем его результат
            // В виде числового значения выбора пользователя
            var choise = StartMenu();

            // Выбираем действие, исходя из выбора игрока
            // Этот блок можно легко дополнять и изменять под любые нужды без серьёзных правок кода
            switch (choise)
            {
                // Если выбор игрока был 1, то вызываем метод вывода меню выбора персонажа
                // Он возвращает латинское имя выбранного персонажа
                case 1: person = ChoisePerson();  break;
                // Если выбор игрока был 2, то выходим из программы
                case 2: Environment.Exit(0); break;
            }
        }


        /// <summary>
        /// Метод вывода начального меню
        /// </summary>
        /// <returns>Возвращает правильный выбор</returns>
        private static int StartMenu()
        {
            // В случаи неправильного выбора - зацикливаем вывод меню
            while(true)
            {
                // Локальная переменная для хранения и возврата выбора
                int choise;
                // Выводим сообщение пользователю
                WriteLine("Добро пожаловат!\nВыберете действие:\n1 - Старт\n2 - Выход\n");
                // Конвертируем ввод пользователя в INT и получаем результат, успешно ли
                bool seccess = int.TryParse(ReadLine(), out choise);

                // Если успешно
                if (seccess)
                {
                    // То проверяем сам ввод, корректен ли он
                    if (choise == 1 || choise == 2)
                        // Если да, возвращаем значение
                        return choise;
                }
                // Если нет, то очищаем консоль и начинаем сначала
                Clear();
            }
        }

        /// <summary>
        /// Метод вывода меню выбора персонажа
        /// </summary>
        /// <returns>Возвращает правильный выбор</returns>
        private static string ChoisePerson()
        {
            // Очищаем консоль
            Clear();

            // Зацикливаем программу на случай некорректного ввода игрока
            // Чтобы он имел возможность исправить ошибку и ввести корректное значение
            while (true)
            {
                // Объявляем переменную для хранения выбора игрока
                int choise;
                // Выводим инструкцию для игрока на консоль
                WriteLine("Выберите персонажа:\n1 - Иван\n2 - Вадим\n");

                // Пробуем сконвертировать ввод пользователя в цифровой формат
                bool success = int.TryParse(ReadLine(), out choise);

                // Если конвертация прошла успешно
                if (success)
                {
                    // Возвращаем имя персонажа латинскими буквами
                    // !НИКОГДА не стоит называть файлы JSON (и не только) кириллицей!
                    switch (choise)
                    {
                        case 1: return "Ivan";
                        case 2: return "Vadim";
                        default: {
                                    WriteLine("Неверный выбор, попробуйте ещё раз!"); 
                                    WriteLine("Для продолжения нажмите любую кнопку ...");
                                    ReadKey();
                                 } break;
                    }
                }
                // Очищаем консоль
                Clear();
            }
        }
    }
}