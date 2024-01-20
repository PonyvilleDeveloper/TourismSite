using Webtech;

/*Точка входа в программу*/

namespace Tourism {
    class Program { //Базовый класс программы
        public static void Main() {
            var api = new REST_API($"{ApiImpl.WorkDir}apiconfig.json", typeof(ApiImpl)); //Инициализация API
            Server? backend = new(api); //Инициализация сервера
            backend.Work = true;

            ApiImpl.logger.Info("MAIN", "Server started...");
            Console.ReadKey();
        }
    }
}