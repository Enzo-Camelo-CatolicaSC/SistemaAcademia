using Academia;

public class TelaModalidades
{
    public void exibir()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Menu de Modalidades ===");
            Console.WriteLine("[1] Cadastrar nova modalidade");
            Console.WriteLine("[2] Visualizar todas as modalidades");
            Console.WriteLine("[0] Voltar ao menu principal");
            Console.Write(">>> ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                opcao = -1;
            }

            switch (opcao)
            {
                case 0:
                    Console.WriteLine("Voltando...");
                    break;
                case 1:
                    cadastrar_modalidade();
                    break;
                case 2:
                    visualizar_modalidades();
                    break;
                default:
                    Console.WriteLine("Opção inválida! Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }

        } while (opcao != 0);
    }

    public void cadastrar_modalidade()
    {
        Console.Clear();
        Console.WriteLine("--- Cadastro de Nova Modalidade ---");

        try
        {
            Console.Write("Nome da modalidade: ");
            string nome = Console.ReadLine();

            // Verifica se uma modalidade com o mesmo nome já existe
            if (DadosAcademia.modalidades.Any(m => m.nome.Equals(nome, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("\nUma modalidade com este nome já existe. Cadastro cancelado.");
                Console.ReadKey();
                return;
            }

            Console.Write("Valor mensal: ");
            if (!float.TryParse(Console.ReadLine(), out float valor_mensal))
            {
                Console.WriteLine("\nValor inválido! O cadastro foi cancelado.");
                Console.ReadKey();
                return;
            }

            Modalidade nova_modalidade = new Modalidade(nome, valor_mensal);
            DadosAcademia.modalidades.Add(nova_modalidade);

            Console.WriteLine("\nModalidade cadastrada com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nOcorreu um erro ao cadastrar: {ex.Message}");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    public void visualizar_modalidades()
    {
        Console.Clear();
        Console.WriteLine("--- Lista de Modalidades Cadastradas ---");

        if (DadosAcademia.modalidades.Count == 0)
        {
            Console.WriteLine("Nenhuma modalidade cadastrada no sistema.");
        }
        else
        {
            foreach (var modalidade in DadosAcademia.modalidades)
            {
                Console.WriteLine($"Nome: {modalidade.nome}");
                Console.WriteLine($"Valor Mensal: {modalidade.valor_mensal:C}");

                // Mostra a lista de instrutores associados a esta modalidade
                if (modalidade.instrutores.Any())
                {
                    // Mapeia a lista de objetos Instrutor para uma lista de nomes
                    var nomes_instrutores = modalidade.instrutores.Select(i => i.nome);
                    Console.WriteLine($"Instrutores: {string.Join(", ", nomes_instrutores)}");
                }
                else
                {
                    Console.WriteLine("Instrutores: Nenhum instrutor associado.");
                }
                Console.WriteLine("--------------------------------------");
            }
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}