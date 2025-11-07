public class TelaFinanceiro
{
    public void exibir()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Menu Financeiro ===");
            Console.WriteLine("Selecione a opção que você deseja visualizar:");
            Console.WriteLine("[1] Débitos (Despesas)");
            Console.WriteLine("[2] Créditos (Receitas)");
            Console.WriteLine("[3] Resultados (Balanço)");
            Console.WriteLine("[0] Voltar");

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
                    visualizar_debitos();
                    break;
                case 2:
                    visualizar_creditos();
                    break;
                case 3:
                    visualizar_resultados();
                    break;
                default:
                    Console.WriteLine("Opção inválida! Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }

        } while (opcao != 0);
    }

    public void visualizar_debitos()
    {
        Console.Clear();
        Console.WriteLine("---=== Débitos ===---");

        if (!DadosAcademia.instrutores.Any())
        {
            Console.WriteLine("Nenhum instrutor cadastrado para calcular os débitos.");
        }
        else
        {
            float total_debitos = 0;
            foreach (var instrutor in DadosAcademia.instrutores)
            {
                Console.WriteLine($"Instrutor: {instrutor.nome}");
                Console.WriteLine($"Salário: {instrutor.salario:C}");
                Console.WriteLine("...");
                total_debitos += instrutor.salario;
            }

            Console.WriteLine("-----------------------");
            Console.WriteLine($"Total: {total_debitos:C}");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    public void visualizar_creditos()
    {
        Console.Clear();
        Console.WriteLine("---=== Créditos ===---");

        if (!DadosAcademia.matriculas.Any())
        {
            Console.WriteLine("Nenhuma matrícula realizada para calcular os créditos.");
        }
        else
        {
            float total_creditos = 0;
            foreach (var matricula in DadosAcademia.matriculas)
            {
                Console.WriteLine($"Aluno: {matricula.aluno.nome}");
                Console.WriteLine($"Matrícula: {matricula.valor_total:C}");
                Console.WriteLine("...");
                total_creditos += matricula.valor_total;
            }

            Console.WriteLine("-----------------------");
            Console.WriteLine($"Total: {total_creditos:C}");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    public void visualizar_resultados()
    {
        Console.Clear();
        Console.WriteLine("---=== Resultados ===---");

        // Calculando o total de débitos (salários dos instrutores)
        float total_debitos = 0;
        foreach (var instrutor in DadosAcademia.instrutores)
        {
            total_debitos += instrutor.salario;
        }

        // Calculando o total de créditos (valor das matrículas)
        float total_creditos = 0;
        foreach (var matricula in DadosAcademia.matriculas)
        {
            total_creditos += matricula.valor_total;
        }

        float resultado = total_creditos - total_debitos;

        Console.WriteLine($"Total débitos: {total_debitos:C}");
        Console.WriteLine($"Total créditos: {total_creditos:C}");
        Console.WriteLine("--------------------------");

        if (resultado >= 0)
        {
            Console.WriteLine($"Lucro: {resultado:C}");
        }
        else
        {
            Console.WriteLine($"Prejuízo: {(resultado):C}");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}