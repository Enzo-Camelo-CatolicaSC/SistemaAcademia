using Academia;

public class TelaInstrutores
{
    public void exibir()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Menu de Instrutores ===");
            Console.WriteLine("[1] Cadastrar novo instrutor");
            Console.WriteLine("[2] Visualizar todos os instrutores");
            Console.WriteLine("[3] Visualizar próxima aula de um instrutor");
            Console.WriteLine("[4] Associar instrutor a uma modalidade");
            Console.WriteLine("[0] Voltar ao menu principal");
            Console.Write(">>> ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                opcao = -1; // Força a opção padrão no switch
            }

            switch (opcao)
            {
                case 0:
                    Console.WriteLine("Voltando...");
                    break;
                case 1:
                    cadastrar_instrutor();
                    break;
                case 2:
                    visualizar_instrutores();
                    break;
                case 3: // <-- NOVO CASE
                    visualizar_proxima_aula();
                    break;
                case 4: // <-- CASE ANTIGO RE-NUMERADO
                    associar_instrutor_modalidade();
                    break;
                default:
                    Console.WriteLine("Opção inválida! Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }

        } while (opcao != 0);
    }

    public void cadastrar_instrutor()
    {
        Console.Clear();
        Console.WriteLine("--- Cadastro de Novo Instrutor ---");

        try
        {
            Console.Write("Nome completo: ");
            string nome = Console.ReadLine();

            Console.Write("CPF (apenas números): ");
            string cpf = Console.ReadLine();

            Console.Write("Telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Salário: ");
            if (!float.TryParse(Console.ReadLine(), out float salario))
            {
                Console.WriteLine("\nSalário inválido! O cadastro foi cancelado.");
                Console.ReadKey();
                return;
            }

            Instrutor novo_instrutor = new Instrutor(nome, cpf, telefone, salario);
            DadosAcademia.instrutores.Add(novo_instrutor);

            Console.WriteLine("\nInstrutor cadastrado com sucesso!");
            Console.WriteLine($"ID gerado: {novo_instrutor.id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nOcorreu um erro ao cadastrar: {ex.Message}");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    public void visualizar_instrutores()
    {
        Console.Clear();
        Console.WriteLine("--- Lista de Instrutores Cadastrados ---");

        if (!DadosAcademia.instrutores.Any())
        {
            Console.WriteLine("Nenhum instrutor cadastrado no sistema.");
        }
        else
        {
            foreach (var instrutor in DadosAcademia.instrutores)
            {
                Console.WriteLine($"ID: {instrutor.id}");
                Console.WriteLine($"Nome: {instrutor.nome}");
                Console.WriteLine($"Salário: {instrutor.salario:C}");

                if (instrutor.modalidades.Any())
                {
                    Console.WriteLine($"Modalidades: {string.Join(", ", instrutor.modalidades)}");
                }
                else
                {
                    Console.WriteLine("Modalidades: Nenhuma associada.");
                }
                Console.WriteLine("---------------------------------");
            }
        }

        // A lógica de perguntar o ID foi removida daqui.
        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    // --- NOVO MÉTODO DEDICADO ---
    public void visualizar_proxima_aula()
    {
        Console.Clear();
        Console.WriteLine("--- Consultar Próxima Aula do Instrutor ---");

        if (!DadosAcademia.instrutores.Any())
        {
            Console.WriteLine("Nenhum instrutor cadastrado para consulta.");
            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }

        // Mostra a lista de instrutores para o usuário saber qual ID digitar
        Console.WriteLine("Instrutores disponíveis:");
        foreach (var instrutor in DadosAcademia.instrutores)
        {
            Console.WriteLine($"ID: {instrutor.id} | Nome: {instrutor.nome}");
        }
        Console.WriteLine("---------------------------------");

        Console.Write("\nDigite o ID do instrutor para consultar: ");
        if (int.TryParse(Console.ReadLine(), out int id_instrutor))
        {
            Instrutor? instrutor_selecionado = null;
            foreach (var instrutor in DadosAcademia.instrutores)
            {
                if (instrutor.id == id_instrutor)
                {
                    instrutor_selecionado = instrutor;
                    break;
                }
            }

            if (instrutor_selecionado != null)
            {
                // Chama o método do objeto instrutor
                string proxima_aula_info = instrutor_selecionado.aula_mais_proxima();

                Console.WriteLine($"\nVerificando agenda para: {instrutor_selecionado.nome}");
                Console.WriteLine(proxima_aula_info);
            }
            else
            {
                Console.WriteLine("Instrutor com o ID informado não foi encontrado.");
            }
        }
        else
        {
            Console.WriteLine("ID inválido.");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    public void associar_instrutor_modalidade()
    {
        Console.Clear();
        Console.WriteLine("--- Associar Instrutor a uma Modalidade ---");

        // Passo 1: Selecionar o instrutor
        Console.WriteLine("Instrutores disponíveis:");
        foreach (var instrutor in DadosAcademia.instrutores)
        {
            Console.WriteLine($"ID: {instrutor.id} | Nome: {instrutor.nome}");
        }
        Console.Write("\nDigite o ID do instrutor: ");
        if (!int.TryParse(Console.ReadLine(), out int id_instrutor))
        {
            Console.WriteLine("ID inválido.");
            Console.ReadKey();
            return;
        }

        Instrutor? instrutor_selecionado = DadosAcademia.instrutores.FirstOrDefault(i => i.id == id_instrutor);
        if (instrutor_selecionado == null)
        {
            Console.WriteLine("Instrutor não encontrado!");
            Console.ReadKey();
            return;
        }

        // Passo 2: Selecionar a modalidade
        Console.WriteLine("\nModalidades disponíveis:");
        foreach (var mod in DadosAcademia.modalidades)
        {
            Console.WriteLine($"- {mod.nome}");
        }
        Console.Write("\nDigite o nome da modalidade a ser associada: ");
        string nome_modalidade = Console.ReadLine();

        Modalidade? modalidade_selecionada = DadosAcademia.modalidades
            .FirstOrDefault(m => m.nome.Equals(nome_modalidade, StringComparison.OrdinalIgnoreCase));

        if (modalidade_selecionada == null)
        {
            Console.WriteLine("Modalidade não encontrada!");
            Console.ReadKey();
            return;
        }

        // Passo 3: Fazer a associação
        modalidade_selecionada.adicionar_instrutor(instrutor_selecionado);

        Console.WriteLine($"\nSucesso! O instrutor {instrutor_selecionado.nome} agora está apto a lecionar {modalidade_selecionada.nome}.");
        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}