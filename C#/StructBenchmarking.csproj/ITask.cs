using System.Text;

namespace StructBenchmarking
{
    public interface ITask
    {
        void Run();

       /* public void TestStrOne()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                str.Append('a');
            }
            str.ToString();
        } */
    }
}