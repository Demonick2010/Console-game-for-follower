using ConsoleGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace ConsoleGame
{
    /// <summary>
    /// Класс выполнения всей логики актов
    /// </summary>
    public class Acts
    {
        // Создаём приватные переменные для хранения данных персонажа, актов и ответов
        private PersonModel _person;
        private List<ActModel> _acts = new List<ActModel>();
        private List<AnswerModel> _answers = new List<AnswerModel>();

        // Через конструктор присваиваем переменной персонажа имя, полученное из метода Main
        public Acts(string personName)
        {
            _person = new PersonModel(personName);
        }

        /// <summary>
        /// Главный метод выполнения логики актов
        /// </summary>
        public void StartAct()
        {
            WriteLine("\nЗагружается сценарий ...");

            // Считываем все данные конкретного персонажа с JSON файлов (акты и ответы)
            _acts = UniversalJSONWork<List<ActModel>>.Deserialize(_person.ActTextPath);
            _answers = UniversalJSONWork<List<AnswerModel>>.Deserialize(_person.ActAnswerPath);

            // Объявляем переменную успешного нахождения нужного ответа
            // Если ответ не найден, прерываем выполнение программы
            bool successful;

            // Зацикливаем выполнение
            while (true)
            {
                // Присваиваем временной переменной название текущего акта
                string tempActName = _person.UserAnswerActStepName;

                // Если массив актов больше 0, то выполняем следующие действия
                if (_acts.Count > 0)
                {
                    // Запускаем метод вывода текущего акта, передавая в качестве параметра - имя текущего акта
                    ShowText(tempActName);

                    // Запускаем метод вывода ответов текущего акта, передавая в качестве параметра - имя текущего акта 
                    // И получая после выполнения отчёт об успехе или неудачи
                    ShowAnswers(tempActName, out successful);

                    // Если ответы получены успешно
                    if (successful)
                    {
                        // Удаляем текущий акт из массива, передавая в качестве параметра - имя текущего акта
                        DeleteActFromList(tempActName);
                    }
                    // Если ответы не получены
                    else
                        // Выходим из цикла
                        break;
                }
                // Если актов в массиве нет, то
                else
                    // Выходим из цикла
                    break;
            }
        }

        /// <summary>
        /// Метод получения и отображения текущего акта
        /// </summary>
        /// <param name="stepName">Имя текущего акта</param>
        private void ShowText(string stepName)
        {
            // Очищаем консоль
            Clear();

            // Объявляем новую переменную для отображения текущего акта
            // И инициализируем её пустой строкой
            string show = string.Empty;

            // Открываем блок отлова ошибок
            // Этот блок нужен, чтобы из-за ошибки программа не прекращала работу
            try
            { 
                // Пробуем найти в массиве, с помощью методов LINQ, текст текущего акта по его назанию
                show = _acts.FirstOrDefault(x => x.ActStepName == stepName).ActText;
            }
            // Если найти не удалось (вернулось NULL)
            catch
            {
                // Меняем цвет текста консоли на красный
                ForegroundColor = ConsoleColor.Red;
                // Выводим сообщение в консоль
                WriteLine("Текст акта не найден!");
                // Сбрасываем цвет текста консоли на дефолтный
                ResetColor();
            }

            // Ещё раз проверяем, если переменная с текстом не пустая или не состоит из пробелов
            if (!string.IsNullOrWhiteSpace(show))
                // Выводим текст акта в консоль
                WriteLine(show);

            // Меняем цвет текста консоли на жёлтый
            ForegroundColor = ConsoleColor.Yellow;
            // Выводим сообщение в консоль
            WriteLine("\nДля продолжения нажмите ENTER ...\n");
            // Сбрасываем цвет текста консоли на дефолтный
            ResetColor();
            // Ждём нажатия любой клавиши
            ReadKey();
        }

        /// <summary>
        /// Метод получения и вывода ответов
        /// </summary>
        /// <param name="stepName">Имя текущего акта</param>
        /// <param name="successful">Возвращаемое значение о успешном (или нет) нахождении ответов для текущего акта</param>
        private void ShowAnswers(string stepName, out bool successful)
        {
            // Устанавливаем переменную о успехе в значение правды (успех)
            successful = true;
            // Объявляем переменную для хранения значения выбора пользователя
            int choise;
            // Объявляем массив для хранения полученных ответов текущего акта
            List<string> answers = new List<string>();

            // Открываем блок отлова ошибок
            try
            {
                // Пробуем найти в массиве, с помощью методов LINQ, ответы для текущего акта по его назанию
                answers = _answers.FirstOrDefault(x => x.ActStepName == stepName).Answers;
            }
            // Если не удалось найти, то
            catch
            {
                ForegroundColor = ConsoleColor.Red;
                // Выводим сообщение об ошибки в консоль
                WriteLine($"Ответы для акта '{stepName}' не найдены!");
                ResetColor();
                // Устанавливаем переменную об успехе в положение ложь (не найдено)
                successful = false;
                // Прерываем выполнение данного метода
                return;
            }

            // Проверяем ещё раз, что ответы действительно найдены
            if (answers != null)
            {
                // Зацикливаем последующий код на тот случай, если пользователь введёт недопустимое значение
                while (true)
                {
                    // Проходим циклом FOR по всем элементам массива ответов
                    for (int i = 0; i < answers.Count; i++)
                    {
                        // Выводим каждый элемент на экран
                        // Используем итератпр плюс 1 (i + 1) в кажестве порядкового номера ответа (i + 1, чтобы отчёт был с 1 а не с 0)
                        WriteLine($"{i + 1} - {answers[i]}");
                    }

                    ForegroundColor = ConsoleColor.Green;
                    // Выводим сообщение запроса ввода ответа в консоль
                    Write("\nВаш ответ: ");
                    ResetColor();

                    // Пробуем сконвертировать ввод пользователя в цифровой формат
                    bool success = int.TryParse(ReadLine(), out choise);

                    // Если конвертация успешна
                    if (success)
                    {
                        // Проверяем, не выходит ли ввод пользователя за диапазон ответов в массиве
                        if (choise > 0 && choise <= answers.Count)
                        {
                            // Если всё хорошо, то присваиваем название следующего акта в свойство персонажа
                            // В качестве ответа берём цифру выбора - 1, т.к. элементы массива начинаются с 0, а пользовательский ввод - с 1
                            _person.UserAnswerActStepName = answers[choise - 1];
                            // Прерываем текущий цикл
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Метод удаления прошедшего акта из массива
        /// </summary>
        /// <param name="actName">Имя прошедшего акта</param>
        private void DeleteActFromList(string actName)
        {
            // Так как акт уже прошёл, значит мы успешно нашли его ранее, следовательно - новая проверка на NULL 
            // И отлов ошибок не требуются
            // Находим прошедший акт в массиве
            var tempAct = _acts.Find(x => x.ActStepName == actName);
            // Удаляем его из массива
            _acts.Remove(tempAct);
        }
    }
}
