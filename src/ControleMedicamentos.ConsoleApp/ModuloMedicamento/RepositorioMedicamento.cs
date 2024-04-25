using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{
    internal class RepositorioMedicamento : Repositorio
    {
        public Medicamento[] MedicamentoBaixoEstoque()
        {
            Entidade[] registros = SelecionarTodos();
            Medicamento[] listaBaixoEstoque = new Medicamento[100];
            int posicao = 0;
            foreach (Medicamento medi in registros)
            {
                if (medi == null)
                    continue;
                if (medi.Quantidade < 10 && medi.Quantidade > 0)
                {
                    listaBaixoEstoque[posicao] = medi;
                    posicao++;
                }
            }
            return listaBaixoEstoque;
        }
        public Medicamento[] MedicamentoSemEstoque()
        {
            Entidade[] registros = SelecionarTodos();
            Medicamento[] listaSemEstoque = new Medicamento[100];
            int posicao = 0;
            foreach (Medicamento medi in registros)
            {
                if (medi == null)
                    continue;
                if (medi.Quantidade == 0)
                {
                    listaSemEstoque[posicao] = medi;
                    posicao++;
                }
            }
            return listaSemEstoque;
        }
        public bool PossuiBaixoEstoque()
        {
            Medicamento[] baixoEstoque = MedicamentoBaixoEstoque();
            for (int i = 0; i < baixoEstoque.Length; i++)
            {
                if (baixoEstoque[i] != null)
                    return true;
            }
            return false;
        }
        public bool PossuiSemEstoque()
        {
            Medicamento[] semEstoque = MedicamentoSemEstoque();
            for (int i = 0; i < semEstoque.Length; i++)
            {
                if (semEstoque[i] != null)
                    return true;
            }
            return false;
        }
    }
}
