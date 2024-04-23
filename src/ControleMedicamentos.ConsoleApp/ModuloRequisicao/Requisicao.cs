using ControleMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;

namespace ControleMedicamentos.ConsoleApp.ModuloRequisicao
{
    internal class Requisicao
    {
        public Medicamento Medicamento { get; set; }
        public Paciente Paciente { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataValidade { get; set; }
    }
}
