using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Generics.Robots
{
    public interface IRobotAI<T>
    {
        T GetCommand();
    }

    public class ShooterAI : IRobotAI<IMoveCommand>
    {
        int counter = 1;

        public IMoveCommand GetCommand()
        {
            return ShooterCommand.ForCounter(counter++);
        }
    }

    public class BuilderAI : IRobotAI<IMoveCommand>
    {
        int counter = 1;
        public IMoveCommand GetCommand()
        {
            return BuilderCommand.ForCounter(counter++);
        }
    }

    public interface IDevice<T>
    {
        string ExecuteCommand(T command);
    }

    public class Mover : IDevice<IMoveCommand>
    {
        public string ExecuteCommand(IMoveCommand cmd)
        {
            var command = cmd;
            if (command == null)
                throw new ArgumentException();
            return $"MOV {command.Destination.X}, {command.Destination.Y}";
        }
    }

    public class Robot
    {
        IRobotAI<IMoveCommand> ai;
        IDevice<IMoveCommand> device;

        public Robot(IRobotAI<IMoveCommand> ai, IDevice<IMoveCommand> executor)
        {
            this.ai = ai;
            this.device = executor;
        }

        public IEnumerable<string> Start(int steps)
        {
             for (int i=0;i<steps;i++)
             {
                 var command = ai.GetCommand();
                 if (command == null)
                     break;
                 yield return device.ExecuteCommand(command);
             }
        }

        public static Robot Create(IRobotAI<IMoveCommand> ai, IDevice<IMoveCommand> executor)
        {
            return new Robot(ai, executor);
        }
    }
}
