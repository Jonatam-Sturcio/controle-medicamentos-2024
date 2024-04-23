namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{
    internal class TelaMedicamento
    {
        private RepositorioMedicamento repositorio = new();
        private void PoucoMedicamento()
        {
            if (repositorio.MedicamentoBaixoEstoque().Count < 1)
                return;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Os seguinte medicamentos estão com estoque baixo: ");
            foreach (Medicamento medi in repositorio.MedicamentoBaixoEstoque())
            {
                Console.WriteLine($"Nome: {medi.Nome}\nQuantidade Atual: {medi.Quantidade}");
            }
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

            Medicamento medicamento = ObterMedicamento();

            repositorio.InserirMedicamento(medicamento);
        }
        public void Visualizar()
        {
            if (repositorio.ListaEstaVazia())
            {
                Notificador.AvisoColorido("Não há nenhum medicamento registrado!", ConsoleColor.Red);
                return;
            }

            MenuPrincipal.Cabecalho();

            repositorio.MostrarMedicamentos();
            Console.ReadKey();
        }
        public void Editar()
        {
            if (repositorio.ListaEstaVazia())
            {
                Notificador.AvisoColorido("Não há nenhum medicamento registrado!", ConsoleColor.Red);
                return;
            }
            MenuPrincipal.Cabecalho();

            string nomeMedicamento = ReceberInformacao("Informe o nome do medicamento a ser editado: ");

            if (!repositorio.MedicamentoExiste(nomeMedicamento))
            {
                Notificador.AvisoColorido("Não existem nenhum medicamento com esse nome!", ConsoleColor.Red);
                return;
            }

            Medicamento medicamento = ObterMedicamento();
            repositorio.EditarMedicamento(nomeMedicamento, medicamento);
        }
        public void Remover()
        {
            if (repositorio.ListaEstaVazia())
            {
                Notificador.AvisoColorido("Não há nenhum medicamento registrado!", ConsoleColor.Red);
                return;
            }
            MenuPrincipal.Cabecalho();

            string nomeMedicamento = ReceberInformacao("Informe o nome do medicamento a ser removido: ");

            if (!repositorio.MedicamentoExiste(nomeMedicamento))
            {
                Notificador.AvisoColorido("Não existem nenhum medicamento com esse nome!", ConsoleColor.Red);
            }
            repositorio.RemoverMedicamento(nomeMedicamento);
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
