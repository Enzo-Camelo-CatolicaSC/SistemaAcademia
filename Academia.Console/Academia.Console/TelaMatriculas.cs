using Academia;

public class TelaMatriculas
{
    public void exibir()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Menu de Matrículas ===");
            Console.WriteLine("[1] Realizar nova matrícula");
            Console.WriteLine("[2] Visualizar todas as matrículas");
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
                    fazer_matricula();
                    break;
                case 2:
                    visualizar_matriculas();
                    break;
                default:
                    Console.WriteLine("Opção inválida! Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }

        } while (opcao != 0);
    }

    public void fazer_matricula()
    {
        Console.Clear();
        Console.WriteLine("--- Realizar Nova Matrícula ---");

        // Valida se existem alunos e modalidades para matricular
        if (!DadosAcademia.alunos.Any() || !DadosAcademia.modalidades.Any())
        {
            Console.WriteLine("É preciso ter ao menos um aluno e uma modalidade cadastrados para realizar uma matrícula.");
            Console.ReadKey();
            return;
        }

        try
        {
            // Passo 1: Selecionar o Aluno
            Console.WriteLine("Alunos disponíveis para matrícula:");
            foreach (var aluno in DadosAcademia.alunos.Where(a => !a.esta_matriculado()))
            {
                Console.WriteLine($"ID: {aluno.id} | Nome: {aluno.nome}");
            }
            Console.Write("\nDigite o ID do aluno a ser matriculado: ");
            if (!int.TryParse(Console.ReadLine(), out int id_aluno))
            {
                throw new Exception("ID de aluno inválido.");
            }

            Aluno? aluno_selecionado = DadosAcademia.alunos.FirstOrDefault(a => a.id == id_aluno);
            if (aluno_selecionado == null)
            {
                throw new Exception("Aluno não encontrado.");
            }
            if (aluno_selecionado.esta_matriculado())
            {
                throw new Exception("Este aluno já possui uma matrícula ativa.");
            }

            // Passo 2: Selecionar as Modalidades
            List<Modalidade> modalidades_selecionadas = new List<Modalidade>();
            string nome_modalidade;
            do
            {
                Console.Clear();
                Console.WriteLine($"Selecionando modalidades para: {aluno_selecionado.nome}");
                Console.WriteLine("Modalidades disponíveis:");
                foreach (var mod in DadosAcademia.modalidades)
                {
                    Console.WriteLine($"- {mod.nome}");
                }
                Console.WriteLine("\nModalidades já adicionadas: " + (modalidades_selecionadas.Any() ? string.Join(", ", modalidades_selecionadas.Select(m => m.nome)) : "Nenhuma"));
                Console.Write("Digite o nome da modalidade para adicionar (ou 'fim' para continuar): ");
                nome_modalidade = Console.ReadLine();

                if (!nome_modalidade.Equals("fim", StringComparison.OrdinalIgnoreCase))
                {
                    Modalidade? modalidade_para_adicionar = DadosAcademia.modalidades
                        .FirstOrDefault(m => m.nome.Equals(nome_modalidade, StringComparison.OrdinalIgnoreCase));

                    if (modalidade_para_adicionar != null && !modalidades_selecionadas.Contains(modalidade_para_adicionar))
                    {
                        modalidades_selecionadas.Add(modalidade_para_adicionar);
                        Console.WriteLine($"'{modalidade_para_adicionar.nome}' adicionada com sucesso!");
                        System.Threading.Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("Modalidade não encontrada ou já adicionada. Tente novamente.");
                        System.Threading.Thread.Sleep(1500);
                    }
                }
            } while (!nome_modalidade.Equals("fim", StringComparison.OrdinalIgnoreCase));

            if (!modalidades_selecionadas.Any())
            {
                throw new Exception("Nenhuma modalidade foi selecionada. Matrícula cancelada.");
            }

            // Passo 3: Duração do plano
            Console.Write("Digite a duração do plano em meses (ex: 6): ");
            if (!int.TryParse(Console.ReadLine(), out int duracao_meses) || duracao_meses <= 0)
            {
                throw new Exception("Duração inválida.");
            }

            // Passo 4: Criar a Matrícula
            Matricula nova_matricula = new Matricula(aluno_selecionado, modalidades_selecionadas, duracao_meses);
            DadosAcademia.matriculas.Add(nova_matricula);

            Console.WriteLine("\nMatrícula realizada com sucesso!");
            Console.WriteLine($"Valor total do plano: {nova_matricula.valor_total:C}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nOcorreu um erro: {ex.Message}");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    public void visualizar_matriculas()
    {
        Console.Clear();
        Console.WriteLine("--- Lista de Matrículas Realizadas ---");

        if (!DadosAcademia.matriculas.Any())
        {
            Console.WriteLine("Nenhuma matrícula foi realizada no sistema.");
        }
        else
        {
            foreach (var matricula in DadosAcademia.matriculas)
            {
                var nomes_modalidades = matricula.modalidades.Select(m => m.nome);

                Console.WriteLine($"ID da Matrícula: {matricula.id}");
                Console.WriteLine($"Aluno: {matricula.aluno.nome}");
                Console.WriteLine($"Modalidades: {string.Join(", ", nomes_modalidades)}");
                Console.WriteLine($"Valor do Plano: {matricula.valor_total:C}");
                Console.WriteLine($"Data da Matrícula: {matricula.data_matricula:dd/MM/yyyy}");
                Console.WriteLine($"Data de Vencimento: {matricula.data_vencimento:dd/MM/yyyy}");
                Console.WriteLine($"Status: {(matricula.esta_ativa ? "Ativa" : "Cancelada")}");
                Console.WriteLine("--------------------------------------");
            }
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}