using System.Collections.Generic;

namespace ConsoleGame.Models
{
    /// <summary>
    /// Модель для хранения данных ответа
    /// </summary>
    public class AnswerModel
    {
        public string ActStepName { get; set; }
        public List<string> Answers { get; set; }
    }
}
