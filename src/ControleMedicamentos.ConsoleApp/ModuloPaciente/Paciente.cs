namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    internal class Paciente
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string CartaoSus { get; set; }

        public void MostrarPaciente()
        {
            Console.WriteLine("{0, 20} | {1, 15} | {2, 25} | {3, 15}",
                this.Nome, this.CPF, this.Endereco, this.CartaoSus);
        }
    }
}
