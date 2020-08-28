using Newtonsoft.Json;
using System.IO;

namespace ConsoleGame
{
    /// <summary>
    /// Универсальный класс сериализации и десериализации данных JSON с помощью библиотеки NEWTONSOFT.JSON
    /// </summary>
    /// <typeparam name="TModel">При вызове методов мы передаём нужный нам класс, сразу не назначаем тип модели</typeparam>
    public static class UniversalJSONWork<TModel> where TModel : class
    {
        /// <summary>
        /// Метод сериализации модели в JSON файл и сохранение этого файла
        /// </summary>
        /// <param name="path">Полный путь к файлу сохранения</param>
        /// <param name="serializeValuesList">Модель для сериализации и сохранения</param>
        public static void Serialize(string path, TModel serializeValuesList)
        {
            // Создаём экземпляр класса сериалайзера
            JsonSerializer serializer = new JsonSerializer();

            // Создаём поток для записи и передаём ему абсолютный путь к файлу сохранения
            using (StreamWriter sw = new StreamWriter(path))
            // Создаём записывающий поток
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                // Сериализуем модель в JSON и сохраняем результат в файл
                serializer.Serialize(writer, serializeValuesList);
                // На всякий случай, закрываем записывающий поток
                writer.Close();
            }
        }

        /// <summary>
        /// Метод десериализации данных из файла JSON
        /// </summary>
        /// <param name="path">Абсолютный путь к файлу с JSON данными</param>
        /// <returns>Модель с данными</returns>
        public static TModel Deserialize(string path)
        {
            // Создаём экземпляр класса сериалайзера
            JsonSerializer serializer = new JsonSerializer();
            // Объявляем модель данных
            TModel model;

            // Открываем поток, который считывает данные из файла
            using (var sr = new StreamReader(path))
            // Открываем поток, который читает JSON данные
            using (var reader = new JsonTextReader(sr))
            {
                // Десериализуем данные из файла JSON и сохраняем их в модель
                model = serializer.Deserialize<TModel>(reader);
                // На всякий случай, освобождаем поток чтения вручную (не обязательно)
                reader.Close();
            }
            // Возвращаем заполненную модель
            return model;
        }
    }
}
