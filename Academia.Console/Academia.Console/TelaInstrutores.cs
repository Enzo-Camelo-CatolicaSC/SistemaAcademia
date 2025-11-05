public class TelaInstrutores
{
    public void Exibir()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Instrutores ===");
            Console.WriteLine("[1] Cadastrar instrutor");
            Console.WriteLine("[2] Visualizar todos os instrutores");
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
                    Console.WriteLine("Cadastrar aluno.");
                    break;
                case 2:
                    Console.WriteLine("Matricular aluno.");
                    break;
                case 3:
                    Console.WriteLine("Alterar dados de um aluno.");
                    break;
                case 4:
                    Console.WriteLine("Buscar aluno.");
                    break;
                case 5:
                    Console.WriteLine("Remover aluno selecionado.");
                    break;
                case 6:
                    Console.WriteLine("Visualizar todos os alunos.");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }

        } while (opcao != 0);
    }
}




