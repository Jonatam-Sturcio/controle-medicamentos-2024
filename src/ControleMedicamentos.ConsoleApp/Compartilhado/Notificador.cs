namespace ControleMedicamentos.ConsoleApp.Compartilhado
{
    internal class Notificador
    {
        public static void AvisoColorido(string texto, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(texto);
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
