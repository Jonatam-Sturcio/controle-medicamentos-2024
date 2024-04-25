using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    internal class TelaPaciente
    {
        public RepositorioPaciente repositorio = new();
        private string ReceberInformacao(string textoApresentado)
        {
            string informacao = "";
            do
            {
                MenuPrincipal.Cabecalho();
                Console.WriteLine(textoApresentado);

                informacao = Console.ReadLine();
                if (informacao.Length != 0)
                    break;

                Notificador.AvisoColorido("É necessário digitar o que foi solicitado!", ConsoleColor.Red);

            } while (true);
            return informacao;
        }
        private Paciente ObterPaciente()
        {
            Paciente paciente = new();
            paciente.Nome = ReceberInformacao("Informe o nome do paciente: ");
            paciente.CPF = ReceberInformacao("Informe o cpf do paciente: ");
            paciente.Endereco = ReceberInformacao("Informe o endereço do paciente: ");
            paciente.CartaoSus = ReceberInformacao("Informe o Cartão SUS do paciente: ");
            return paciente;
        }
        private int Menu()
        {
            char opcao;
            do
            {
                MenuPrincipal.Cabecalho();
                Console.WriteLine(" 1 - Cadastrar paciente");
                Console.WriteLine(" 2 - Visualizar pacientes");
                Console.WriteLine(" 3 - Editar paciente");
                Console.WriteLine(" 4 - Excluir paciente");
                Console.WriteLine(" 0 - Sair\n");

                Console.WriteLine("Informe a opção desejada: ");
                opcao = Console.ReadLine()[0];

                if (char.IsNumber(opcao))
                    break;

                Notificador.AvisoColorido("Apenas números sao permitidos! Tente novamente!", ConsoleColor.Red);

            } while (true);

            return Convert.ToInt32(opcao + "");
        }
        public void Inserir()
        {
            MenuPrincipal.Cabecalho();

            Paciente paciente = ObterPaciente();

            repositorio.Cadastrar(paciente);
            Notificador.AvisoColorido("Paciente cadastrado com sucesso!", ConsoleColor.Green);
        }
        public void Visualizar()
        {
            Entidade[] entidades = repositorio.SelecionarTodos();
            if (!repositorio.PossuiElementos())
            {
                Notificador.AvisoColorido("Não há nenhum paciente registrado!", ConsoleColor.Red);
                return;
            }

            MenuPrincipal.Cabecalho();
            Console.WriteLine("{0, 5} | {1, 20} | {2, 15} | {3, 25} | {4,15}",
                "ID", "Nome", "CPF", "Endereço", "Cartão SUS");
            foreach (Paciente paci in entidades)
            {
                if (paci == null)
                    continue;

                Console.WriteLine("{0, 5} | {1, 20} | {2, 15} | {3, 25} | {4,15}",
                    paci.ID, paci.Nome, paci.CPF, paci.Endereco, paci.CartaoSus);
            }

            Console.ReadKey();
        }
        public void Editar()
        {
            if (!repositorio.PossuiElementos())
            {
                Notificador.AvisoColorido("Não há nenhum paciente registrado!", ConsoleColor.Red);
                return;
            }
            MenuPrincipal.Cabecalho();

            int idPaciente = int.Parse(ReceberInformacao("Informe o id do paciente a ser editado: "));

            if (!repositorio.Existe(idPaciente))
            {
                Notificador.AvisoColorido("Não existem nenhum paciente com esse id!", ConsoleColor.Red);
                return;
            }

            Paciente paciente = ObterPaciente();
            repositorio.Editar(idPaciente, paciente);
            Notificador.AvisoColorido("Paciente editado com sucesso!", ConsoleColor.Green);
        }
        public void Remover()
        {
            if (!repositorio.PossuiElementos())
            {
                Notificador.AvisoColorido("Não há nenhum paciente registrado!", ConsoleColor.Red);
                return;
            }
            MenuPrincipal.Cabecalho();

            int idPaciente = int.Parse(ReceberInformacao("Informe o id do paciente a ser removido: "));

            if (!repositorio.Existe(idPaciente))
            {
                Notificador.AvisoColorido("Não existem nenhum paciente com esse id!", ConsoleColor.Red);
            }
            repositorio.Excluir(idPaciente);
            Notificador.AvisoColorido("Paciente excluído com sucesso!", ConsoleColor.Green);
        }
        public void AcessarMenu()
        {
            while (true)
            {
                int menu = Menu();

                if (menu == 0)
                    break;

                if (menu != 1 && menu != 2 && menu != 3 && menu != 4)
                {
                    Notificador.AvisoColorido("Opção Inválida! Tente novamente!", ConsoleColor.Red);
                    continue;
                }
                if (menu == 1)
                    Inserir();
                else if (menu == 2)
                    Visualizar();
                else if (menu == 3)
                    Editar();
                else
                    Remover();
            }
        }
    }
}
