using Academia;
using System.Linq;

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

        if (!DadosAcademia.alunos.Any() || !DadosAcademia.modalidades.Any())
        {
            Console.WriteLine("É preciso ter ao menos um aluno e uma modalidade cadastrados para realizar uma matrícula.");
            Console.ReadKey();
            return;
        }

        try
        {
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
                throw new Exception("Aluno não encontrado.");
            if (aluno_selecionado.esta_matriculado())
                throw new Exception("Este aluno já possui uma matrícula ativa.");

            List<Modalidade> modalidades_selecionadas = new List<Modalidade>();
            string nome_modalidade;
            do
            {
                Console.Clear();
                Console.WriteLine($"Selecionando modalidades para: {aluno_selecionado.nome}");
                Console.WriteLine("Modalidades disponíveis:");
                foreach (var mod in DadosAcademia.modalidades)
                    Console.WriteLine($"- {mod.nome}");

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
                throw new Exception("Nenhuma modalidade foi selecionada. Matrícula cancelada.");

            Console.Write("Digite a duração do plano em meses (ex: 6): ");
            if (!int.TryParse(Console.ReadLine(), out int duracao_meses) || duracao_meses <= 0)
                throw new Exception("Duração inválida.");

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

        // 🟢 Corrigido: busca por CPF, não por ID
        Console.Write("\nDeseja buscar uma matrícula pelo CPF? (s/n): ");
        string resposta = Console.ReadLine().ToLower();

        if (resposta == "s")
            buscar_matricula();
        else
        {
            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }

    public void buscar_matricula()
    {
        Console.Clear();
        Console.WriteLine("--- Buscar Matrícula por CPF ---");
        Console.Write("Digite o CPF do aluno: ");
        string cpf = Console.ReadLine();

        var aluno = DadosAcademia.alunos.FirstOrDefault(a => a.cpf == cpf);

        if (aluno == null)
        {
            Console.WriteLine("\nAluno não encontrado!");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }

        var matricula = DadosAcademia.matriculas.FirstOrDefault(m => m.aluno == aluno);

        if (matricula == null)
        {
            Console.WriteLine("\nNenhuma matrícula encontrada para este aluno.");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }

        Console.Clear();
        Console.WriteLine("--- Dados da Matrícula ---");
        Console.WriteLine($"ID: {matricula.id}");
        Console.WriteLine($"Aluno: {aluno.nome}");
        Console.WriteLine($"CPF: {aluno.cpf}");
        Console.WriteLine($"Modalidade: {aluno.modalidade_preferida.nome}");
        Console.WriteLine($"Data de Início: {matricula.data_matricula:dd/MM/yyyy}");
        Console.WriteLine($"Status: {(matricula.esta_ativa ? "Ativa" : "Cancelada")}");
        Console.WriteLine("---------------------------------");

        Console.WriteLine("\n[1] Alterar matrícula");
        Console.WriteLine("[2] Cancelar matrícula");
        Console.WriteLine("[0] Voltar");
        Console.Write(">>> ");
        string opcao = Console.ReadLine();

        switch (opcao)
        {
            case "1":
                alterar_matricula(matricula);
                break;
            case "2":
                cancelar_matricula(matricula);
                break;
            default:
                Console.WriteLine("\nVoltando ao menu...");
                Console.ReadKey();
                break;
        }
    }

    public void alterar_matricula(Matricula matricula)
    {
        Console.Clear();
        Console.WriteLine("--- Alterar Matrícula ---");

        try
        {
            Console.WriteLine($"Aluno atual: {matricula.aluno.nome}");
            Console.Write("Deseja alterar o aluno? (s/n): ");
            if (Console.ReadLine().ToLower() == "s")
            {
                Console.WriteLine("\nAlunos disponíveis:");
                foreach (var aluno in DadosAcademia.alunos)
                    Console.WriteLine($"ID: {aluno.id} | Nome: {aluno.nome}");
                Console.Write("Digite o ID do novo aluno: ");
                if (int.TryParse(Console.ReadLine(), out int idNovoAluno))
                {
                    var novoAluno = DadosAcademia.alunos.FirstOrDefault(a => a.id == idNovoAluno);
                    if (novoAluno != null)
                        matricula.aluno = novoAluno;
                    else
                        Console.WriteLine("Aluno não encontrado, mantendo o anterior.");
                }
            }

            Console.WriteLine("\nModalidades atuais: " + string.Join(", ", matricula.modalidades.Select(m => m.nome)));
            Console.Write("Deseja alterar as modalidades? (s/n): ");
            if (Console.ReadLine().ToLower() == "s")
            {
                List<Modalidade> novasModalidades = new();
                string nome;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Modalidades disponíveis:");
                    foreach (var mod in DadosAcademia.modalidades)
                        Console.WriteLine($"- {mod.nome}");

                    Console.WriteLine("\nModalidades já adicionadas: " + (novasModalidades.Any() ? string.Join(", ", novasModalidades.Select(m => m.nome)) : "Nenhuma"));
                    Console.Write("Digite o nome da modalidade para adicionar (ou 'fim' para continuar): ");
                    nome = Console.ReadLine();

                    if (!nome.Equals("fim", StringComparison.OrdinalIgnoreCase))
                    {
                        var modSelecionada = DadosAcademia.modalidades.FirstOrDefault(m => m.nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
                        if (modSelecionada != null && !novasModalidades.Contains(modSelecionada))
                            novasModalidades.Add(modSelecionada);
                        else
                            Console.WriteLine("Modalidade inválida ou já adicionada.");
                    }
                } while (!nome.Equals("fim", StringComparison.OrdinalIgnoreCase));

                if (novasModalidades.Any())
                    matricula.modalidades = novasModalidades;
            }

            Console.Write($"\nDuração atual: {(matricula.data_vencimento - matricula.data_matricula).Days / 30} meses. Nova duração (ou Enter p/ manter): ");
            string novaDuracaoStr = Console.ReadLine();
            if (int.TryParse(novaDuracaoStr, out int novaDuracao))
                matricula.data_vencimento = matricula.data_matricula.AddMonths(novaDuracao);

            Console.Write("\nDeseja alterar o status da matrícula? (Ativa/Cancelada): ");
            string novoStatus = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoStatus))
                matricula.esta_ativa = novoStatus.Equals("Ativa", StringComparison.OrdinalIgnoreCase);

            Console.WriteLine("\nMatrícula atualizada com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErro ao alterar matrícula: {ex.Message}");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }


    public void cancelar_matricula(Matricula matricula)
    {
        Console.Clear();
        Console.WriteLine("--- Cancelamento de Matrícula ---");
        Console.WriteLine($"ID: {matricula.id}");
        Console.WriteLine($"Aluno: {matricula.aluno.nome}");
        Console.WriteLine($"Modalidades: {string.Join(", ", matricula.modalidades.Select(m => m.nome))}");
        Console.WriteLine("---------------------------------");

        Console.Write("\nTem certeza que deseja cancelar esta matrícula? (s/n): ");
        string confirmacao = Console.ReadLine().ToLower();

        if (confirmacao == "s")
        {
            DadosAcademia.matriculas.Remove(matricula);
            Console.WriteLine("\nMatrícula cancelada com sucesso!");
        }
        else
        {
            Console.WriteLine("\nCancelamento cancelado.");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}
