using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;
using ControleMedicamentos.ConsoleApp.ModuloRequisicao;

namespace ControleMedicamentos.ConsoleApp
{
    internal class MenuPrincipal
    {
        TelaPaciente telaPaciente = new();
        TelaMedicamento telaMedicamento = new();
        TelaRequisicao telaRequisicao = new();
        private int MostrarMenu()
        {
            char opcao;
            do
            {
                Cabecalho();
                Console.WriteLine(" 1 - Medicamentos");
                Console.WriteLine(" 2 - Pacientes");
                Console.WriteLine(" 3 - Requisições");
                Console.WriteLine(" 0 - Sair\n");

                Console.WriteLine("Informe a opção desejada: ");
                opcao = Console.ReadLine()[0];

                if (char.IsNumber(opcao))
                    break;

                Notificador.AvisoColorido("Apenas números sao permitidos! Tente novamente!", ConsoleColor.Red);

            } while (true);

            return Convert.ToInt32(opcao + "");
        }
        private void ChamarSubmenus()
        {
            while (true)
            {
                int menuPrincipal = MostrarMenu();

                if (menuPrincipal == 0)
                    break;

                if (menuPrincipal != 1 && menuPrincipal != 2 && menuPrincipal != 3)
                {
                    Notificador.AvisoColorido("Opção Inválida! Tente novamente!", ConsoleColor.Red);
                    continue;
                }

                if (menuPrincipal == 1)
                    telaMedicamento.AcessarMenu();
                if (menuPrincipal == 2)
                    telaPaciente.AcessarMenu();
                if (menuPrincipal == 3)
                    telaRequisicao.AcessarMenu();
            }
        }
        public void IniciarPrograma()
        {
            telaRequisicao.telaPaciente = telaPaciente;
            telaRequisicao.telaMedicamento = telaMedicamento;
            ChamarSubmenus();
        }
        public static void Cabecalho()
        {
            Console.Clear();
            Console.WriteLine("Posto de Saúde\n");
        }
    }
}
