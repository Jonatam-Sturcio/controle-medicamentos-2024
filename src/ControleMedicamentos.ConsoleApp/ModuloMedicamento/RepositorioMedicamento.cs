using System.Collections;

namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{
    internal class RepositorioMedicamento
    {
        private ArrayList medicamentos = new();

        public bool ListaEstaVazia()
        {
            if (medicamentos.Count == 0)
                return true;

            return false;
        }
        public void InserirMedicamento(Medicamento medicamento)
        {
            if (!MedicamentoExiste(medicamento.Nome))
            {
                medicamentos.Add(medicamento);
                Notificador.AvisoColorido("Medicamento cadastrado com sucesso!", ConsoleColor.Green);
                return;
            }

            Notificador.AvisoColorido("Medicamento já cadastrado, a quantidade será adicionada ao medicamento",
                    ConsoleColor.Blue);

            foreach (Medicamento medi in medicamentos)
            {
                if (medi.Nome == medicamento.Nome)
                {
                    medi.Quantidade += medicamento.Quantidade;
                }
            }

        }
        public void MostrarMedicamentos()
        {
            Console.WriteLine("{0, 15} | {1, 30} | {2, 10}", "Nome", "Descrição", "Quantidade");
            foreach (Medicamento medi in medicamentos)
            {
                medi.MostrarPaciente();
            }
        }
        public void EditarMedicamento(string nome, Medicamento novoMedicamento)
        {
            foreach (Medicamento medi in medicamentos)
            {
                if (medi.Nome == nome)
                {
                    medi.Nome = novoMedicamento.Nome;
                    medi.Descricao = novoMedicamento.Descricao;
                    medi.Quantidade = novoMedicamento.Quantidade;
                    Notificador.AvisoColorido("Medicamento editado com sucesso!", ConsoleColor.Green);
                    return;
                }
            }
            Notificador.AvisoColorido("Medicamento não encontrado!", ConsoleColor.Red);
        }
        public void RemoverMedicamento(string nome)
        {
            Medicamento removerMedicamento = null;

            foreach (Medicamento medi in medicamentos)
            {
                if (medi.Nome == nome)
                {
                    removerMedicamento = medi;
                    break;
                }
            }
            if (removerMedicamento != null)
            {
                medicamentos.Remove(removerMedicamento);
                Notificador.AvisoColorido("Medicamento removido com sucesso!", ConsoleColor.Green);
                return;
            }
            Notificador.AvisoColorido("Medicamento não encontrado!", ConsoleColor.Red);
        }
        public bool MedicamentoExiste(string nomeMedicamento)
        {
            foreach (Medicamento medi in medicamentos)
            {
                if (medi.Nome == nomeMedicamento)
                {
                    return true;
                }
            }
            return false;
        }
        public ArrayList MedicamentoBaixoEstoque()
        {
            ArrayList listaMedicamentosBaixoEstoque = new();
            foreach (Medicamento medi in medicamentos)
            {
                if (medi.Quantidade < 10)
                    listaMedicamentosBaixoEstoque.Add(medi);
            }
            return listaMedicamentosBaixoEstoque;
        }
    }
}
