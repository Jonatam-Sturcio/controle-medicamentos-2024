namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{
    internal class Medicamento
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public int QtdRetirado { get; set; }
        public void MostrarPaciente()
        {
            Console.WriteLine("{0, 15} | {1, 30} | {2, 10}",
                this.Nome, this.Descricao, this.Quantidade);
        }

    }
}
