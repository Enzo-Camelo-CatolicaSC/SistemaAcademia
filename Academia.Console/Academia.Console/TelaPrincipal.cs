public class TelaPrincipal
{
    public void Exibir()
    {
        int opcao = 0;

        do
        {
            Console.Clear();
            Console.WriteLine("=== Bem-vindo ===");
            Console.WriteLine("Selecione a opção que você deseja administrar:");
            Console.WriteLine("[1] Academia");
            Console.WriteLine("[2] Alunos");
            Console.WriteLine("[3] Instrutores");
            Console.WriteLine("[4] Aulas");
            Console.WriteLine("[0] Sair");
            Console.Write("\nOpção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
                opcao = -1;

            switch (opcao)
            {
                case 0:
                    Console.WriteLine("Saindo do sistema...");
                    break;
                case 1:
                    new TelaAcademia().Exibir();
                    break;
                case 2:
                    new TelaAlunos().Exibir();
                    break;
                case 3:
                    new TelaInstrutores().Exibir();
                    break;
                    
                default:
                    Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }

        } while (opcao != 0);
    }
}
        