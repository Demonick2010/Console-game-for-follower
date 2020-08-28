using System.IO;

namespace ConsoleGame.Models
{
    /// <summary>
    /// Модель персонажа
    /// </summary>
    public class PersonModel
    {
        // Через конструктор получаем выбранное игроком имя персонажа
        public PersonModel(string personName)
        {
            // Присваиваем полученное имя в свойство модели для хранения
            PersonName = personName;

            // Назначаем пути к JSON файлам актов и ответов
            string dialogFolderPath = $"Dialogs\\{personName}\\ActTexts\\Act.json";
            string answerFolderPath = $"Dialogs\\{personName}\\Answers\\ActAnswer.json";

            // Получаем обсалютный путь к корневому каталогу программы
            string rootPath = Directory.GetCurrentDirectory();

            // Комбинируем пути
            ActTextPath = Path.Combine(rootPath, dialogFolderPath);
            ActAnswerPath = Path.Combine(rootPath, answerFolderPath);
        }

        public string PersonName { get; set; }

        // Назначаем значение "по умолчанию" для стартового акта
        public string UserAnswerActStepName { get; set; } = "Start";
        public string ActTextPath { get; set; }
        public string ActAnswerPath { get; set; }
    }
}
