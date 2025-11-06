using Academia; // Importante para ter acesso à classe Aluno

public class TelaAlunos
{
    public void exibir()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Menu de Alunos ===");
            Console.WriteLine("[1] Cadastrar novo aluno");
            Console.WriteLine("[2] Visualizar todos os alunos");
            Console.WriteLine("[0] Voltar ao menu principal");
            Console.Write(">>> ");

            // Tenta converter a entrada do usuário para um número
            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                opcao = -1; // Define um valor inválido para mostrar a mensagem de erro padrão
            }

            switch (opcao)
            {
                case 0:
                    Console.WriteLine("Voltando...");
                    break;
                case 1:
                    cadastrar_aluno();
                    break;
                case 2:
                    visualizar_alunos();
                    break;
                default:
                    Console.WriteLine("Opção inválida! Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }

        } while (opcao != 0);
    }

    public void cadastrar_aluno()
    {
        Console.Clear();
        Console.WriteLine("--- Cadastro de Novo Aluno ---");

        try
        {
            Console.Write("Nome completo: ");
            string nome = Console.ReadLine();

            Console.Write("CPF (apenas números): ");
            string cpf = Console.ReadLine();

            Console.Write("Telefone: ");
            string telefone = Console.ReadLine();

            Console.WriteLine("\nModalidades disponíveis:");
            foreach (var mod in DadosAcademia.modalidades)
            {
                Console.WriteLine($"- {mod.nome}");
            }

            Console.Write("\nDigite o nome exato da modalidade de preferência: ");
            string nome_modalidade_escolhida = Console.ReadLine();

            // Busca na lista de modalidades o objeto cujo nome corresponde ao que o usuário digitou
            Modalidade? modalidade_escolhida = DadosAcademia.modalidades.FirstOrDefault(m => m.nome.Equals(nome_modalidade_escolhida, StringComparison.OrdinalIgnoreCase));

            // Verifica se a modalidade foi encontrada
            if (modalidade_escolhida == null)
            {
                Console.WriteLine("\nModalidade não encontrada! O cadastro foi cancelado.");
                Console.WriteLine("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return; // Encerra o método
            }

            // Agora passamos o OBJETO modalidade_escolhida, e não mais uma string
            Aluno novo_aluno = new Aluno(nome, cpf, telefone, modalidade_escolhida);

            // Adiciona o aluno à lista central
            DadosAcademia.alunos.Add(novo_aluno);

            Console.WriteLine("\nAluno cadastrado com sucesso!");
            Console.WriteLine($"ID gerado: {novo_aluno.id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nOcorreu um erro: {ex.Message}");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    public void visualizar_alunos()
    {
        Console.Clear();
        Console.WriteLine("--- Lista de Alunos Cadastrados ---");

        if (DadosAcademia.alunos.Count == 0)
        {
            Console.WriteLine("Nenhum aluno cadastrado no sistema.");
        }
        else
        {
            foreach (var aluno in DadosAcademia.alunos)
            {
                Console.WriteLine($"ID: {aluno.id}");
                Console.WriteLine($"Nome: {aluno.nome}");
                // Agora acessamos a propriedade 'nome' do objeto modalidade
                Console.WriteLine($"Modalidade Preferida: {aluno.modalidade_preferida.nome}");
                Console.WriteLine($"Está Matriculado? {(aluno.esta_matriculado() ? "Sim" : "Não")}");
                Console.WriteLine("---------------------------------");
            }
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}