
using Academia;

while (true)
{
    Console.Clear();
    Console.WriteLine("\n=== Tela inicial ===\n");
    Console.WriteLine("Selecione a opção que você deseja administrar:\n");
    Console.WriteLine("[1] Matriculas");
    Console.WriteLine("[2] Alunos");
    Console.WriteLine("[3] Instrutores");
    Console.WriteLine("[4] Aulas");
    Console.WriteLine("[5] Modalidades");
    Console.WriteLine("[6] Financeiro");
    Console.WriteLine("[0] Sair");
    Console.Write(">>> ");
    string opcao = Console.ReadLine();

    switch (opcao)
    {
        case "0":
            Console.WriteLine("\nSaindo do sistema...");
            return;

        case "1":
            TelaMatriculas tela_matriculas = new TelaMatriculas();
            tela_matriculas.exibir();
            break;

        case "2":
            TelaAlunos tela_alunos = new TelaAlunos();
            tela_alunos.exibir();
            break;

        case "3":
            TelaInstrutores tela_instrutores = new TelaInstrutores();
            tela_instrutores.exibir();
            break;

        case "4":
            TelaAulas tela_aulas = new TelaAulas();
            tela_aulas.exibir();
            break;

        case "5":
            TelaModalidades tela_modalidades = new TelaModalidades();
            tela_modalidades.exibir();
            break;

        case "6":
            TelaFinanceiro tela_financeiro = new TelaFinanceiro();
            tela_financeiro.exibir();
            break;

        default:
            Console.WriteLine("\nOpção inválida. Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            break;
    }
}