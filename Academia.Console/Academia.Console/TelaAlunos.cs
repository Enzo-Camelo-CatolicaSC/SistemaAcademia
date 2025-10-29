public class TelaAlunos
{
    public void Exibir()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Alunos ===");
            Console.WriteLine("[1] Cadastrar aluno");
            Console.WriteLine("[2] Matricular aluno");
            Console.WriteLine("[3] Alterar dados de um aluno");
            Console.WriteLine("[4] Buscar aluno");
            Console.WriteLine("[5] Remover aluno");
            Console.WriteLine("[6] Visualizar todos os alunos");
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




