using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace dClient
{
    public class LoadMods
    {
        public Config config_;

        public LoadMods(Config config)
        {
            config_ = config;
            bool parsed = bool.Parse(config_.modLoad);
            if (parsed == true) { 
                Execute();
                Console.WriteLine("Mods loaded");
            }
            else
            {
                Console.WriteLine("Mods disabled");
            }
        }

        //Load all of the mods
        public void Execute()
        {
            string[] files = Directory.GetFiles(config_.modDir);
            foreach (string files_ in files)
            {
                var DLL = Assembly.LoadFile(files_);
                Console.WriteLine("Loaded Mod: " + files_);
                foreach (Type type in DLL.GetExportedTypes())
                {
                    dynamic c = Activator.CreateInstance(type);
                    //c.Output(@"Hello");
                } 
            }
        }
    }
}
