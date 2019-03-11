using System;

using eldotnet.Data;
using eldotnet.Report;

namespace eldotnet.Asm
{
    public class Runtime
    {
        public static string[] Source {get; set;}

    /// <summary>
    /// Init of vCPU that calls loading code and static analysis to be executed upon loaded code
    /// </summary>
    /// <param name="sourcepath"></param>
        public static void Init(string sourcepath)
        {
            Log.Out.LogRuntime("Loading source from " + sourcepath);
            Source = Parser.LoadCode(sourcepath);
        }

        /// <summary>
        /// Main vCPU function
        /// Hardcoded instruction handling not really an ideal state but will rewrite it later on ...maybe
        /// </summary>
        public static void Run(bool stepping)
        {
            foreach (string line in Source)
            {

                if(stepping)
                {
                    Log.Out.LogRuntime("Stepping has been enabled\n Pres any key to progres one instruction ahead");
                    Console.ReadLine();
                }

                string[] parts = line.Split(' ');

                switch(parts[0].ToLower())
                {
                    case "add":
                        if(Registers.IsRegister(parts[2]))
                            Execution.Add<Register>(Registers.NameToRegister(parts[1]), Registers.NameToRegister(parts[2]));
                        else
                            Execution.Add<short>(Registers.NameToRegister(parts[1]), Convert.ToInt16(parts[2]));
                    break; 
                    
                    case "mov":
                        if(Registers.IsRegister(parts[2]))
                            Execution.Mov<Register>(Registers.NameToRegister(parts[1]), Registers.NameToRegister(parts[2]));
                        else
                            Execution.Mov<short>(Registers.NameToRegister(parts[1]), Convert.ToInt16(parts[2]));
                    break;

                    case "dec":
                        if(Registers.IsRegister(parts[2]))
                            Execution.Dec<Register>(Registers.NameToRegister(parts[1]), Registers.NameToRegister(parts[2]));
                        else
                            Execution.Dec<short>(Registers.NameToRegister(parts[1]), Convert.ToInt16(parts[2]));
                    break;
                }
            }
            
        }
    }
}