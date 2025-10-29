public class TelaAcademia
{
    public void Exibir()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Academia ===");
            Console.WriteLine("[1] Configurações da academia");
            Console.WriteLine("[2] Gerenciar Modalidades");
            Console.WriteLine("[3] Financeiro");
            Console.WriteLine("[4] Gerar relatório geral");
            Console.WriteLine("[0] Voltar");
            Console.Write("\nOpção: ");

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
                    Console.WriteLine("Configurações da academia.");
                    break;
                case 2:
                    Console.WriteLine("Gerenciar modalidades.");
                    break;
                case 3:
                    Console.WriteLine("Financeiro.");
                    break;
                case 4:
                    Console.WriteLine("Gerar relatório geral.");
                    break;
            
                default:
                    Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }

        } while (opcao != 0);
    }
}




