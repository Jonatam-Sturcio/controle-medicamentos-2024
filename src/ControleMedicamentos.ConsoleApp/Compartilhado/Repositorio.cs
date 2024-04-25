namespace ControleMedicamentos.ConsoleApp.Compartilhado
{
    internal class Repositorio
    {

        protected Entidade[] registros = new Entidade[100];
        protected int id = 1;
        public bool PossuiElementos()
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] != null)
                    return true;
            }

            return false;
        }
        public void Cadastrar(Entidade novoRegistro)
        {
            novoRegistro.ID = id++;
            RegistrarItem(novoRegistro);
        }
        public Entidade[] SelecionarTodos()
        {
            return registros;
        }
        public Entidade SelecionarPorId(int id)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    continue;
                if (registros[i].ID == id)
                    return registros[i];
            }
            return null;
        }
        public bool Editar(int id, Entidade novaEntidade)
        {
            novaEntidade.ID = id;
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    continue;
                if (registros[i].ID == id)
                {
                    registros[i] = novaEntidade;
                    return true;
                }
            }
            return false;
        }
        public bool Excluir(int id)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    continue;
                if (registros[i].ID == id)
                {
                    registros[i] = null;
                    return true;
                }
            }
            return false;
        }
        public bool Existe(int id)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    continue;
                if (registros[i].ID == id)
                {
                    return true;
                }
            }
            return false;
        }
        protected void RegistrarItem(Entidade novoRegistro)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] != null)
                    continue;
                else
                {
                    registros[i] = novoRegistro;
                    break;
                }
            }
        }
    }
}
