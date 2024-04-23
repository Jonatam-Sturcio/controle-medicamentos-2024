using ControleMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleMedicamentos.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelaMedicamento tela = new();
            tela.AcessarMenu();
        }
    }
}
