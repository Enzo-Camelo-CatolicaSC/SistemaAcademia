using Academia;
using System;
using System.Linq;

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

            // Verifica se já existe
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

                if (modalidade.instrutores.Any())
                {
                    var nomes_instrutores = modalidade.instrutores.Select(i => i.nome);
                    Console.WriteLine($"Instrutores: {string.Join(", ", nomes_instrutores)}");
                }
                else
                {
                    Console.WriteLine("Instrutores: Nenhum instrutor associado.");
                }
                Console.WriteLine("--------------------------------------");
            }

            Console.Write("\nDeseja buscar uma modalidade específica? (S/N): ");
            string opcaoBusca = Console.ReadLine().Trim().ToUpper();

            if (opcaoBusca == "S")
            {
                buscar_modalidade();
            }
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    private void buscar_modalidade()
    {
        Console.Clear();
        Console.WriteLine("--- Buscar Modalidade ---");
        Console.Write("Digite o nome da modalidade: ");
        string nomeBusca = Console.ReadLine();

        Modalidade modalidadeEncontrada = DadosAcademia.modalidades
            .FirstOrDefault(m => m.nome.Equals(nomeBusca, StringComparison.OrdinalIgnoreCase));

        if (modalidadeEncontrada == null)
        {
            Console.WriteLine("\nModalidade não encontrada!");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }

        Console.Clear();
        Console.WriteLine("--- Dados da Modalidade ---");
        Console.WriteLine($"Nome: {modalidadeEncontrada.nome}");
        Console.WriteLine($"Valor Mensal: {modalidadeEncontrada.valor_mensal:C}");

        if (modalidadeEncontrada.instrutores.Any())
        {
            var nomes_instrutores = modalidadeEncontrada.instrutores.Select(i => i.nome);
            Console.WriteLine($"Instrutores: {string.Join(", ", nomes_instrutores)}");
        }
        else
        {
            Console.WriteLine("Instrutores: Nenhum instrutor associado.");
        }

        Console.WriteLine("\n[1] Alterar dados da modalidade");
        Console.WriteLine("[2] Excluir modalidade");
        Console.WriteLine("[0] Voltar");
        Console.Write(">>> ");
        string opcao = Console.ReadLine();

        switch (opcao)
        {
            case "1":
                alterar_modalidade(modalidadeEncontrada);
                break;
            case "2":
                excluir_modalidade(modalidadeEncontrada);
                break;
            default:
                Console.WriteLine("Voltando...");
                break;
        }
    }

    private void alterar_modalidade(Modalidade modalidade)
    {
        Console.Clear();
        Console.WriteLine("--- Alterar Dados da Modalidade ---");

        Console.Write($"Novo nome (atual: {modalidade.nome}): ");
        string novo_nome = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(novo_nome))
        {
            modalidade.nome = novo_nome;
        }

        Console.Write($"Novo valor mensal (atual: {modalidade.valor_mensal:C}): ");
        string novo_valor_str = Console.ReadLine();
        if (float.TryParse(novo_valor_str, out float novo_valor))
        {
            modalidade.valor_mensal = novo_valor;
        }

        Console.WriteLine("\nDados da modalidade atualizados com sucesso!");
        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    private void excluir_modalidade(Modalidade modalidade)
    {
        Console.Write("\nTem certeza que deseja excluir esta modalidade? (S/N): ");
        string confirm = Console.ReadLine().Trim().ToUpper();

        if (confirm == "S")
        {
            DadosAcademia.modalidades.Remove(modalidade);
            Console.WriteLine("Modalidade excluída com sucesso!");
        }
        else
        {
            Console.WriteLine("Exclusão cancelada.");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}
