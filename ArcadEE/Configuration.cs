using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BotBits;
using BotCake;

namespace ArcadEE
{
    public class Configuration
    {
        public bool LoadFromFile(string file)
        {
            try
            {
                var config = File.ReadAllLines(file);

                foreach (var line in config)
                {
                    if (!line.Contains(":"))
                        continue;

                    var setting = line.Split(new char[] { ':' }, 2);
                    LoadSetting(setting[0].ToLower(), setting[1]);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("ERROR: {0}", ex.Message);
                return false;
            }
            return true;
        }

        public bool LoadedRequired()
        {
            var missingConfig = from conf in requiredConfig
                                         where !loadedConfig.Contains(conf)
                                         select conf;

            if (missingConfig.Count() > 0)
            {
                Console.WriteLine("ERROR: Missing required configuration settings:");
                foreach (var missing in missingConfig)
                {
                    Console.WriteLine(missing);
                }

                return false;
            }
            return true;
        }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public string WorldId { get; private set; }

        private void LoadSetting(string name, string data)
        {
            switch (name)
            {
                case "email":
                    Email = data;
                    break;
                case "password":
                    Password = data;
                    break;
                case "worldid":
                    WorldId = data;
                    break;
                
                default:
                    Console.WriteLine("ERROR: Found unknow config: {0} - {1}", name, data);
                    break;
            }

            loadedConfig.Add(name);
        }

        private string[] requiredConfig = { "email", "password", "worldid" };
        private HashSet<String> loadedConfig = new HashSet<String>();
    }
}

