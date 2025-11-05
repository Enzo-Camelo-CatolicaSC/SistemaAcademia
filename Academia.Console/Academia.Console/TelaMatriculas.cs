public class TelaMatriculas
{
    public void Exibir()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Matriculas ===");
            Console.WriteLine("[1] Fazer matricula");
            Console.WriteLine("[2] Visualizar todas as matriculas");
            Console.WriteLine("[0] Voltar");
            Console.Write(">>> ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                continue;
            }

            switch (opcao)
            {
                case 0:
                    Console.WriteLine("Voltando ao menu principal...");
                    break;

                case 1:
                    Console.WriteLine("Fazer matricula.");
                    break;

                case 2:
                    Console.WriteLine("Visualizar todas as matriculas.");
                    break;

                default:
                    Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }

        } while (opcao != 0);
    }
}




