public class TelaModalidades
{
    public void Exibir()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Modalidades ===");
            Console.WriteLine("[1] Cadastrar modalidade");
            Console.WriteLine("[2] Visualizar todas as modalidades");
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
                    Console.WriteLine("Cadastrar modalidade.");
                    break;

                case 2:
                    Console.WriteLine("Visualizar todas as modalidades.");
                    break;

                default:
                    Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }

        } while (opcao != 0);
    }
}




