using System.IO;
using System;
using System.Collections.Generic;

namespace KizhiPart2
{
    public class Item
    {
        public Action<string[]> function;
        public string[] data;
    }

    public class Interpreter
    {
        private TextWriter _writer;
        private Dictionary<string, Action<string[]>> setCommands = new Dictionary<string, Action<string[]>>();
        private Dictionary<string, int> variables = new Dictionary<string, int>();
        private Dictionary<string, Item[]> setFunctions = new Dictionary<string, Item[]>();

        public Interpreter(TextWriter writer)
        {
            _writer = writer;
            RegisterCommands();
        }

        public void ExecuteLine(string command)
        {
            if (command == "set code" || command == "end set code")
                return;



            //setCommands[command.Split(' ')[0]](command.Split(' '));
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

        private void RegisterFunction(string command)
        {
            var splitCommand = command.Split(' ');

            for (int i = 0; i < splitCommand.Length; i++)
            {
                if (splitCommand[i] == "def")
                {
                    var nameFunc = splitCommand[i + 1];
                    List<Item> functions = new List<Item>();
                    i += 5;
                    List<string> query = new List<string>();
                    while(splitCommand[i] != "")
                    {
                        query.Add(splitCommand[i]);
                        i++;
                    }
                    string[] com = query.ToArray();
                    functions.Add(new Item { function = setCommands[com[0]], data = com});



                    //functions.Add(setCommands[query[0]]());
                }
            }
        }
    }
}