namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    internal class TelaPaciente
    {
        private RepositorioPaciente repositorio = new();
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
            if (repositorio.PacienteExiste(paciente.Nome))
            {
                Notificador.AvisoColorido("Já existe um chamado com esse nome!", ConsoleColor.Red);
                return;
            }

            repositorio.InserirPaciente(paciente);
        }
        public void Visualizar()
        {
            if (repositorio.ListaEstaVazia())
            {
                Notificador.AvisoColorido("Não há nenhum paciente registrado!", ConsoleColor.Red);
                return;
            }

            MenuPrincipal.Cabecalho();

            repositorio.MostrarPacientes();
            Console.ReadKey();
        }
        public void Editar()
        {
            if (repositorio.ListaEstaVazia())
            {
                Notificador.AvisoColorido("Não há nenhum paciente registrado!", ConsoleColor.Red);
                return;
            }
            MenuPrincipal.Cabecalho();

            string nomePaciente = ReceberInformacao("Informe o nome do paciente a ser editado: ");

            if (!repositorio.PacienteExiste(nomePaciente))
            {
                Notificador.AvisoColorido("Não existem nenhum paciênte com esse nome!", ConsoleColor.Red);
                return;
            }

            Paciente paciente = ObterPaciente();
            repositorio.EditarPaciente(nomePaciente, paciente);
        }
        public void Remover()
        {
            if (repositorio.ListaEstaVazia())
            {
                Notificador.AvisoColorido("Não há nenhum paciente registrado!", ConsoleColor.Red);
                return;
            }
            MenuPrincipal.Cabecalho();

            string nomePaciente = ReceberInformacao("Informe o nome do paciente a ser removido: ");

            if (!repositorio.PacienteExiste(nomePaciente))
            {
                Notificador.AvisoColorido("Não existem nenhum paciente com esse nome!", ConsoleColor.Red);
            }
            repositorio.RemoverPaciente(nomePaciente);
        }
        public void AcessarMenuPaciente()
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
