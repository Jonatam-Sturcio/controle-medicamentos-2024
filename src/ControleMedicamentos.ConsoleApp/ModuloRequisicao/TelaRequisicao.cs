using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;

namespace ControleMedicamentos.ConsoleApp.ModuloRequisicao
{
    internal class TelaRequisicao
    {
        public RepositorioRequisicao repositorio = new();
        public TelaPaciente telaPaciente = null;
        public TelaMedicamento telaMedicamento = null;
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
        private Requisicao ObterRequisicao()
        {
            Requisicao requisicao = new();
            int id_Paciente, id_Medicamento, qtdMedicamento;
            do
            {
                try
                {
                    id_Paciente = int.Parse(ReceberInformacao("Informe o id do paciente: "));
                    id_Medicamento = int.Parse(ReceberInformacao("Informe o id do medicamento: "));
                    qtdMedicamento = int.Parse(ReceberInformacao("Informe a quantidade a ser retirado do medicamento: "));
                }
                catch
                {
                    Notificador.AvisoColorido("Apenas números são permitidos! Tente novamente!", ConsoleColor.Red);
                    continue;
                }
                break;
            } while (true);

            requisicao.Paciente = (Paciente)telaPaciente.repositorio.SelecionarPorId(id_Paciente);
            requisicao.Medicamento = (Medicamento)telaMedicamento.repositorio.SelecionarPorId(id_Medicamento);
            requisicao.Quantidade = qtdMedicamento;
            requisicao.DataValidade = DateTime.Parse(ReceberInformacao("Informe a data de validade da requisição: "));
            return requisicao;
        }
        private int Menu()
        {
            char opcao;
            do
            {
                MenuPrincipal.Cabecalho();
                Console.WriteLine(" 1 - Cadastrar requisição");
                Console.WriteLine(" 2 - Visualizar requisições");
                Console.WriteLine(" 3 - Editar requisição");
                Console.WriteLine(" 4 - Excluir requisição");
                Console.WriteLine(" 0 - Sair\n");

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

            Requisicao requisicao = ObterRequisicao();

            repositorio.Cadastrar(requisicao);
            Notificador.AvisoColorido("Requisição cadastrada com sucesso!", ConsoleColor.Green);
        }
        public void Visualizar()
        {
            Entidade[] entidades = repositorio.SelecionarTodos();
            if (!repositorio.PossuiElementos())
            {
                Notificador.AvisoColorido("Não há nenhuma requisição registrada!", ConsoleColor.Red);
                return;
            }

            MenuPrincipal.Cabecalho();
            Console.WriteLine("{0, 5} | {1, 15} | {2, 15} | {3, 12} | {4, 18}",
                "ID", "Paciente", "Medicamento", "Quantidade", "Data de Validade");
            foreach (Requisicao requi in entidades)
            {
                if (requi == null)
                    continue;

                Console.WriteLine("{0, 5} | {1, 15} | {2, 15} | {3, 12} | {4, 18}",
                    requi.ID, requi.Paciente.Nome, requi.Medicamento.Nome, requi.Quantidade, requi.DataValidade);
            }

            Console.ReadKey();
        }
        public void Editar()
        {
            if (!repositorio.PossuiElementos())
            {
                Notificador.AvisoColorido("Não há nenhuma requisição registrada!", ConsoleColor.Red);
                return;
            }
            MenuPrincipal.Cabecalho();

            int idRequisicao = int.Parse(ReceberInformacao("Informe o id da requisição a ser editada: "));

            if (!repositorio.Existe(idRequisicao))
            {
                Notificador.AvisoColorido("Não existem nenhuma requisição com esse id!", ConsoleColor.Red);
                return;
            }

            Requisicao requisicao = ObterRequisicao();
            repositorio.Editar(idRequisicao, requisicao);
            Notificador.AvisoColorido("Requisição editado com sucesso!", ConsoleColor.Green);
        }
        public void Remover()
        {
            if (!repositorio.PossuiElementos())
            {
                Notificador.AvisoColorido("Não há nenhuma requisição registrada!", ConsoleColor.Red);
                return;
            }
            MenuPrincipal.Cabecalho();

            int idRequisicao = int.Parse(ReceberInformacao("Informe o id da requisição a ser removida: "));

            if (!repositorio.Existe(idRequisicao))
            {
                Notificador.AvisoColorido("Não existem nenhuma requisição com esse id!", ConsoleColor.Red);
            }
            repositorio.Excluir(idRequisicao);
            Notificador.AvisoColorido("requisição excluído com sucesso!", ConsoleColor.Green);
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
