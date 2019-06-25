using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace EncryptionMachine
{
    class Machine
    {
        public class Context
        {
            public Dictionary<string, object> variables = new Dictionary<string, object>();
            public object accumulator = null;
            public string log = string.Empty;
            public bool error = false;
        };

        Context context = new Context();
        private readonly Dictionary<string, MethodInfo> commandList = new Dictionary<string, MethodInfo>();

        struct CommandUnit
        {
            readonly public MethodInfo command;
            readonly public object[] arguments;

            public CommandUnit(MethodInfo command, object[] arguments)
            {
                this.command = command;
                this.arguments = arguments;
            }
        };
        private List<CommandUnit> compiledProgram = new List<CommandUnit>();


        public Machine()
        {
            MethodInfo[] methodsArray = typeof(MachineOperations).GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Instance);
            foreach (MethodInfo method in methodsArray)
            {
                commandList.Add(method.Name.ToLower(), method);
            }
        }

        public bool Compile(string code, out Context refContext)
        {
            using (StringReader reader = new StringReader(code))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] tmp = line.Split(' ');
                    if (tmp.Length == 0) continue;

                    string command = tmp[0];
                    object[] arguments = new object[tmp.Length];
                    arguments[0] = context;
                    for (int i = 1; i < tmp.Length; i++)
                    {
                        if (long.TryParse(tmp[i], out long argumentAsInt))
                        {
                            arguments[i] = argumentAsInt;
                        }
                        else
                        {
                            arguments[i] = tmp[i];
                        }
                    }
                    compiledProgram.Add(new CommandUnit(commandList[command], arguments));
                }
            }
            refContext = context;
            if (compiledProgram.Count == 0)
                return false;
            return true;
        }

        public string Run(Context preContext)
        {
            context.log = string.Empty;
            context.error = false;
            context = preContext;

            var stopwatch = new Stopwatch();
            Console.WriteLine($"{Environment.NewLine}New calculation is started...");
            
            foreach (CommandUnit currentCommand in compiledProgram)
            {
                stopwatch.Start();
                currentCommand.command.Invoke(null, currentCommand.arguments);
                stopwatch.Stop();

                StringBuilder outputArgs = new StringBuilder();
                foreach (var arg in currentCommand.arguments)
                {
                    if (arg is long || arg is string)
                        outputArgs.AppendFormat(" {0} ", arg);
                }
                if (context.accumulator is string || context.accumulator is long)
                    Console.WriteLine($"{currentCommand.command.Name} ({outputArgs}) -> {context.accumulator}");
                else if (context.accumulator is long[] array)
                {
                    StringBuilder outputArray = new StringBuilder();
                    Array.ForEach(array, (a) => outputArray.AppendFormat(" {0} ", a));
                    Console.WriteLine($"{currentCommand.command.Name} ({outputArgs}) -> [{outputArray}]");
                }
                if (context.error)
                {
                    Console.WriteLine($"Error in {currentCommand.command.Name} ({outputArgs}). Calculation aborted.");
                    break;
                }
            }
            
            Console.WriteLine($"Calculation finished in {stopwatch.Elapsed.TotalMilliseconds} ms");
            return context.log;
        }
    }
}