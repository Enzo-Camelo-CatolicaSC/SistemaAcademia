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

        if (!DadosAcademia.modalidades.Any())
        {
            Console.WriteLine("\nAVISO: Nenhuma modalidade foi cadastrada no sistema.");
            Console.WriteLine("Por favor, acesse o menu de modalidades e cadastre ao menos uma antes de registrar um aluno.");

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }

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

            Modalidade? modalidade_escolhida = DadosAcademia.modalidades
                .FirstOrDefault(m => m.nome.Equals(nome_modalidade_escolhida, StringComparison.OrdinalIgnoreCase));

            if (modalidade_escolhida == null)
            {
                Console.WriteLine("\nModalidade não encontrada! O cadastro foi cancelado.");
                Console.WriteLine("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Aluno novo_aluno = new Aluno(nome, cpf, telefone, modalidade_escolhida);
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
                Console.WriteLine($"Modalidade Preferida: {aluno.modalidade_preferida.nome}");
                Console.WriteLine($"Está matriculado?: {(aluno.esta_matriculado()? "Sim" : "Não")}");
                Console.WriteLine("---------------------------------");
            }
        }

        Console.Write("\nDeseja buscar um aluno pelo CPF? (s/n): ");
        string opcao = Console.ReadLine().Trim().ToLower();

        if (opcao == "s")
        {
            Console.Write("\nDigite o CPF do aluno que deseja buscar: ");
            string cpfBusca = Console.ReadLine().Trim();

            var alunoEncontrado = DadosAcademia.alunos
                .Find(a => a.cpf.Equals(cpfBusca, StringComparison.OrdinalIgnoreCase));

            Console.Clear();
            if (alunoEncontrado != null)
            {
                Console.WriteLine("--- Aluno Encontrado ---");
                Console.WriteLine($"ID: {alunoEncontrado.id}");
                Console.WriteLine($"Nome: {alunoEncontrado.nome}");
                Console.WriteLine($"CPF: {alunoEncontrado.cpf}");
                Console.WriteLine($"Telefone: {alunoEncontrado.telefone}");
                Console.WriteLine($"Modalidade Preferida: {alunoEncontrado.modalidade_preferida.nome}");
                Console.WriteLine($"Está matriculado?: {(alunoEncontrado.esta_matriculado()? "Sim" : "Não")}");

                Console.WriteLine("\nO que deseja fazer?");
                Console.WriteLine("[1] Alterar dados do aluno");
                Console.WriteLine("[2] Excluir aluno");
                Console.WriteLine("[3] Voltar");
                Console.Write("\nEscolha uma opção: ");
                string escolha = Console.ReadLine().Trim();

                switch (escolha)
                {
                    case "1":
                        alterar_dados_aluno(alunoEncontrado);
                        break;

                    case "2":
                        Console.Write("\nTem certeza que deseja excluir este aluno? (s/n): ");
                        string confirma = Console.ReadLine().Trim().ToLower();

                        if (confirma == "s")
                        {
                            DadosAcademia.alunos.Remove(alunoEncontrado);
                            Console.WriteLine("\nAluno excluído com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("\nOperação cancelada.");
                        }
                        break;

                    default:
                        Console.WriteLine("\nVoltando ao menu...");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Nenhum aluno encontrado com esse CPF.");
            }
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
public void alterar_dados_aluno(Aluno aluno)
    {
        Console.Clear();
        Console.WriteLine($"--- Alterar Dados do Aluno: {aluno.nome} ---");

        try
        {
            Console.WriteLine("\nPressione Enter para manter o valor atual.");

            Console.Write($"\nNome atual ({aluno.nome}): ");
            string novoNome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoNome))
                aluno.nome = novoNome;

            Console.Write($"CPF atual ({aluno.cpf}): ");
            string novoCpf = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoCpf))
                aluno.cpf = novoCpf;

            Console.Write($"Telefone atual ({aluno.telefone}): ");
            string novoTelefone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoTelefone))
                aluno.telefone = novoTelefone;

            Console.WriteLine("\nModalidades disponíveis:");
            foreach (var mod in DadosAcademia.modalidades)
            {
                Console.WriteLine($"- {mod.nome}");
            }

            Console.Write($"\nModalidade atual ({aluno.modalidade_preferida.nome}): ");
            string novaModalidadeNome = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(novaModalidadeNome))
            {
                Modalidade? novaModalidade = DadosAcademia.modalidades
                    .FirstOrDefault(m => m.nome.Equals(novaModalidadeNome, StringComparison.OrdinalIgnoreCase));

                if (novaModalidade != null)
                    aluno.modalidade_preferida = novaModalidade;
                else
                    Console.WriteLine("Modalidade não encontrada. Mantendo a atual.");
            };

            Console.WriteLine("\nDados do aluno atualizados com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nOcorreu um erro ao alterar os dados: {ex.Message}");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}