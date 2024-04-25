using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{
    internal class TelaMedicamento
    {
        public RepositorioMedicamento repositorio = new();
        private void Avisos()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (repositorio.PossuiBaixoEstoque())
                Console.WriteLine("Há medicamento com baixo estoque!");
            if (repositorio.PossuiSemEstoque())
                Console.WriteLine("Há medicamento sem estoque!");
            Console.ResetColor();
        }
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
        private Medicamento ObterMedicamento()
        {
            Medicamento medicamento = new();
            medicamento.Nome = ReceberInformacao("Informe o nome do medicamento: ");
            medicamento.Descricao = ReceberInformacao("Informe a descrição do medicamento: ");
            do
            {
                try
                {
                    medicamento.Quantidade = int.Parse(ReceberInformacao("Informe a quantidade do medicamento: "));
                }
                catch
                {
                    Notificador.AvisoColorido("Apenas números são permitidos! Tente novamente!", ConsoleColor.Red);
                    continue;
                }
                break;
            } while (true);

            return medicamento;
        }
        private int Menu()
        {
            char opcao;
            do
            {
                MenuPrincipal.Cabecalho();
                Console.WriteLine(" 1 - Cadastrar medicamento");
                Console.WriteLine(" 2 - Visualizar medicamentos");
                Console.WriteLine(" 3 - Editar medicamento");
                Console.WriteLine(" 4 - Excluir medicamento");
                Console.WriteLine(" 5 - Solicitar medicamento");
                Console.WriteLine(" 6 - Visualizar medicamentos mais retirados");
                Console.WriteLine(" 0 - Sair\n");
                Avisos();
                Console.WriteLine("\nInforme a opção desejada: ");
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

            Medicamento medicamento = ObterMedicamento();

            repositorio.Cadastrar(medicamento);
            Notificador.AvisoColorido("Medicamento cadastrado com sucesso!", ConsoleColor.Green);
        }
        public void Visualizar()
        {
            Entidade[] entidades = repositorio.SelecionarTodos();
            if (!repositorio.PossuiElementos())
            {
                Notificador.AvisoColorido("Não há nenhum medicamento registrado!", ConsoleColor.Red);
                return;
            }

            MenuPrincipal.Cabecalho();
            Console.WriteLine("{0, 5} | {1, 15} | {2, 25} | {3, 10}", "ID", "Nome", "Descrição", "Quantidade");
            foreach (Medicamento medi in entidades)
            {
                if (medi == null)
                    continue;

                Console.WriteLine("{0, 5} | {1, 15} | {2, 25} | {3, 10}",
                    medi.ID, medi.Nome, medi.Descricao, medi.Quantidade);
            }

            Console.ReadKey();
        }
        public void Editar()
        {
            if (!repositorio.PossuiElementos())
            {
                Notificador.AvisoColorido("Não há nenhum medicamento registrado!", ConsoleColor.Red);
                return;
            }
            MenuPrincipal.Cabecalho();

            int idMedicamento = int.Parse(ReceberInformacao("Informe o id do medicamento a ser editado: "));

            if (!repositorio.Existe(idMedicamento))
            {
                Notificador.AvisoColorido("Não existem nenhum medicamento com esse id!", ConsoleColor.Red);
                return;
            }

            Medicamento medicamento = ObterMedicamento();
            repositorio.Editar(idMedicamento, medicamento);
            Notificador.AvisoColorido("Medicamento editado com sucesso!", ConsoleColor.Green);
        }
        public void Remover()
        {
            if (!repositorio.PossuiElementos())
            {
                Notificador.AvisoColorido("Não há nenhum medicamento registrado!", ConsoleColor.Red);
                return;
            }
            MenuPrincipal.Cabecalho();

            int idMedicamento = int.Parse(ReceberInformacao("Informe o id do medicamento a ser removido: "));

            if (!repositorio.Existe(idMedicamento))
            {
                Notificador.AvisoColorido("Não existem nenhum medicamento com esse id!", ConsoleColor.Red);
            }
            repositorio.Excluir(idMedicamento);
            Notificador.AvisoColorido("Medicamento excluído com sucesso!", ConsoleColor.Green);
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
