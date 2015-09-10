using BotCake;
using BotBits;

namespace ArcadEE
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var config = new Configuration();
            if (!config.LoadFromFile("config.txt") || !config.LoadedRequired())
            {
                return;
            }

            CakeServices.Run(bot =>
            {
                var manager = new BotManager();

                ConnectionManager.Of(bot)
                    .EmailLogin(config.Email, config.Password)
                    .CreateJoinRoom(config.WorldId);

                return manager;
            });
        }
    }
}
