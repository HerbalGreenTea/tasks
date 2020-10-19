using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Stack
    {
        char[] stck; // массив содержащий стек 
        int tos; // индекс вершины стека
        public Stack(int size) // построй пустой класс Stack для реализации стека заданного размера // конструктор класса!!!
        {
            stck = new char[size]; // распределение памяи для стека
            tos = 0;
        }
        public void Push(char ch) // поместить символы в стек 
        {
            if (tos == stck.Length)
            {
                Console.WriteLine("- Стек заполнен. ");
                return;
            }
            stck[tos] = ch;
            tos++;
        }
        public char Pop() // извлечь символ и стека
        {
            if (tos == 0)
            {
                Console.WriteLine("Стек пуст.");
                return (char)0;
            }
            tos--;
            return stck[tos];
        }
        public bool IsFull() // возвращать true если стек заполнен
        {
            return tos == stck.Length;
        }
        public bool IsEmpty() // возвращать true если стек пуст 
        {
            return tos == 0;
        }
        public int Capacity() // возвратить общую емкость стека
        {
            return stck.Length;
        }
        public int GetNum() // возвратить количество объетов находящихся в данный момент в стеке
        {
            return tos;
        }
    }

    class StaskDemo
    {
        static void Main()
        {
            Stack stk_1 = new Stack(10);
            Stack stk_2 = new Stack(10);
            Stack stk_3 = new Stack(10);
            int i = 0; char ch;

            Console.WriteLine("Заполнение первого стека... ");
 
            for (i = 0; !stk_1.IsFull(); i++)
                stk_1.Push((char)('A' + i));
            if (stk_1.IsFull()) Console.WriteLine("Стек заполнен");

            Console.WriteLine("Извлечение первого стека... ");

            while (!stk_1.IsEmpty())
                Console.Write(stk_1.Pop() + "_");
            Console.WriteLine();
            if (stk_1.IsEmpty()) Console.WriteLine("Стек пуст");

            Console.WriteLine("добавление допонительных значений... ");

            for (i = 0; !stk_1.IsFull(); i++)
                stk_1.Push((char)('A' + i));
            if (stk_1.IsFull()) Console.WriteLine("Стек заполнен");

            Console.WriteLine("Перемещение значений из stk_1 в stk_2... ");

            while (!stk_2.IsFull())
            {
                ch = stk_1.Pop(); stk_2.Push(ch);
            }
            Console.WriteLine("Извлечение значений из второго стека... ");

            while (!stk_2.IsEmpty())
                Console.Write(stk_2.Pop() + "_");
            Console.WriteLine();

            Console.WriteLine("Поместить 5 значений в третий стек... ");

            for (i = 0; i < 5; i++)
                stk_3.Push((char)('A' + i));

            Console.WriteLine("общий объем стека: " + stk_3.Capacity());
            Console.WriteLine("объетов на данный момент в стеке: " + stk_3.GetNum());

           // Console.WriteLine("общий объем стека {1}\n количество объетов в стеке на данный момент составляет {2}", stk_3.Capacity(), stk_3.GetNum());

            Console.ReadKey();
        }
    }
}
