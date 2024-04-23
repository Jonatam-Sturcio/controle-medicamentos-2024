using System.Collections;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    internal class RepositorioPaciente
    {
        private ArrayList pacientes = new ArrayList();

        public bool ListaEstaVazia()
        {
            if (pacientes.Count == 0)
                return true;

            return false;
        }
        public void InserirPaciente(Paciente paciente)
        {
            pacientes.Add(paciente);
            Notificador.AvisoColorido("Paciente cadastrado com sucesso!", ConsoleColor.Green);
        }
        public void MostrarPacientes()
        {
            Console.WriteLine("{0, 20} | {1, 15} | {2, 25} | {3, 15}", "Nome", "CPF", "Endereço", "Cartão SUS");
            foreach (Paciente paci in pacientes)
            {
                paci.MostrarPaciente();
            }
        }
        public void EditarPaciente(string nome, Paciente novoPaciente)
        {
            foreach (Paciente paci in pacientes)
            {
                if (paci.Nome == nome)
                {
                    paci.Nome = novoPaciente.Nome;
                    paci.CPF = novoPaciente.CPF;
                    paci.Endereco = novoPaciente.Endereco;
                    paci.CartaoSus = novoPaciente.CartaoSus;
                    Notificador.AvisoColorido("Paciente editado com sucesso!", ConsoleColor.Green);
                    return;
                }
            }
            Notificador.AvisoColorido("Paciente não encontrado!", ConsoleColor.Red);
        }
        public void RemoverPaciente(string nome)
        {
            Paciente removerPaciente = null;

            foreach (Paciente paci in pacientes)
            {
                if (paci.Nome == nome)
                {
                    removerPaciente = paci;
                    break;
                }
            }
            if (removerPaciente != null)
            {
                pacientes.Remove(removerPaciente);
                Notificador.AvisoColorido("Paciente removido com sucesso!", ConsoleColor.Green);
                return;
            }
            Notificador.AvisoColorido("Paciente não encontrado!", ConsoleColor.Red);
        }
        public bool PacienteExiste(string nomePaciente)
        {
            foreach (Paciente paci in pacientes)
            {
                if (paci.Nome == nomePaciente)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
