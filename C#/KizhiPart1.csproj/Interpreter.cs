using System;
using System.Collections.Generic;
using System.IO;

namespace KizhiPart1
{
    public class Interpreter
    {
        private TextWriter _writer;
        private Dictionary<string, Action<string[]>> setCommands = new Dictionary<string, Action<string[]>>();
        private Dictionary<string, int> variables = new Dictionary<string, int>();

        public Interpreter(TextWriter writer)
        {
            _writer = writer;
            RegisterCommands();
        }

        public void ExecuteLine(string command)
        {
            setCommands[command.Split(' ')[0]](command.Split(' '));
        }

        private void RegisterCommands()
        {
            Action<string, Action> Execute = (variable, command) => {
                if (variables.ContainsKey(variable))
                    command();
                else
                    _writer.WriteLine("Переменная отсутствует в памяти");
            };

            setCommands.Add("set", (splitCommand) => variables[splitCommand[1]] = int.Parse(splitCommand[2]));

            setCommands.Add("sub", (splitCommand) => 
                Execute(splitCommand[1], () => variables[splitCommand[1]] -= int.Parse(splitCommand[2])));

            setCommands.Add("rem", (splitCommand) =>
                Execute(splitCommand[1], () => variables.Remove(splitCommand[1])));

            setCommands.Add("print", (splitCommand) =>
                Execute(splitCommand[1], () => _writer.WriteLine(variables[splitCommand[1]])));
        }
    }
}