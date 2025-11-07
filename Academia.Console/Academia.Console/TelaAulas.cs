using Academia;

public class TelaAulas
{
    public void exibir()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Menu de Aulas ===");
            Console.WriteLine("[1] Agendar nova aula");
            Console.WriteLine("[2] Visualizar todas as aulas");
            Console.WriteLine("[0] Voltar");
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
                    agendar_aula();
                    break;
                case 2:
                    visualizar_aulas();
                    break;
                default:
                    Console.WriteLine("Opção inválida! Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        } while (opcao != 0);
    }

    public void agendar_aula()
    {
        Console.Clear();
        Console.WriteLine("--- Agendamento de Nova Aula ---");

        // Validação inicial
        if (!DadosAcademia.modalidades.Any() || !DadosAcademia.instrutores.Any())
        {
            Console.WriteLine("É preciso ter ao menos uma modalidade e um instrutor cadastrados para agendar uma aula.");
            Console.ReadKey();
            return;
        }

        try
        {
            // Passo 1: Selecionar a Modalidade
            Console.WriteLine("Modalidades disponíveis:");
            foreach (var mod in DadosAcademia.modalidades)
            {
                Console.WriteLine($"- {mod.nome}");
            }
            Console.Write("\nDigite o nome da modalidade para a aula: ");
            string nome_modalidade = Console.ReadLine();
            Modalidade? modalidade_selecionada = DadosAcademia.modalidades
                .FirstOrDefault(m => m.nome.Equals(nome_modalidade, StringComparison.OrdinalIgnoreCase));

            if (modalidade_selecionada == null) throw new Exception("Modalidade não encontrada.");

            // Passo 2: Selecionar um Instrutor qualificado
            var instrutores_qualificados = modalidade_selecionada.instrutores;
            if (!instrutores_qualificados.Any())
            {
                throw new Exception($"Não há instrutores qualificados para a modalidade '{modalidade_selecionada.nome}'.");
            }

            Console.WriteLine("\nInstrutores qualificados para esta modalidade:");
            foreach (var instrutor in instrutores_qualificados)
            {
                Console.WriteLine($"ID: {instrutor.id} | Nome: {instrutor.nome}");
            }
            Console.Write("\nDigite o ID do instrutor para a aula: ");
            if (!int.TryParse(Console.ReadLine(), out int id_instrutor)) throw new Exception("ID de instrutor inválido.");

            Instrutor? instrutor_selecionado = instrutores_qualificados.FirstOrDefault(i => i.id == id_instrutor);
            if (instrutor_selecionado == null) throw new Exception("Instrutor não encontrado ou não qualificado para esta modalidade.");

            // Passo 3: Definir data e hora
            Console.Write("Digite a data e hora da aula (formato dd/MM/yyyy HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime horario))
            {
                throw new Exception("Formato de data ou hora inválido.");
            }

            // Passo 4: Definir limite de vagas e duração
            Console.Write("Digite o limite de vagas para a aula: ");
            if (!int.TryParse(Console.ReadLine(), out int limite_vagas) || limite_vagas <= 0)
            {
                throw new Exception("Limite de vagas inválido.");
            }

            Console.Write("Digite a duração da aula em minutos (ex: 60): ");
            if (!int.TryParse(Console.ReadLine(), out int duracao_minutos) || duracao_minutos <= 0)
            {
                throw new Exception("Duração inválida.");
            }

            // Passo 5: Criar a Aula
            Aula nova_aula = new Aula(modalidade_selecionada, instrutor_selecionado, horario, limite_vagas, duracao_minutos);
            DadosAcademia.aulas.Add(nova_aula);

            Console.WriteLine("\nAula agendada com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nOcorreu um erro: {ex.Message}");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    public void visualizar_aulas()
    {
        Console.Clear();
        Console.WriteLine("--- Lista de Aulas Agendadas ---");

        if (!DadosAcademia.aulas.Any())
        {
            Console.WriteLine("Nenhuma aula agendada no sistema.");
            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }

        foreach (var aula in DadosAcademia.aulas.OrderBy(a => a.horario))
        {
            Console.WriteLine($"ID: {aula.id} | Horário: {aula.horario:dd/MM HH:mm} | Modalidade: {aula.modalidade.nome} | Vagas: {aula.alunos.Count}/{aula.limite_vagas}");
        }
        Console.WriteLine("--------------------------------------");

        Console.Write("\nDigite o ID da aula para gerenciar as vagas (ou [0] para voltar): ");
        if (int.TryParse(Console.ReadLine(), out int id_aula_escolhida) && id_aula_escolhida != 0)
        {
            Aula? aula_encontrada = DadosAcademia.aulas.FirstOrDefault(a => a.id == id_aula_escolhida);

            if (aula_encontrada != null)
            {
                // Chama o novo método para gerenciar a aula específica
                gerenciar_vagas_aula(aula_encontrada);
            }
            else
            {
                Console.WriteLine("Aula com o ID informado não foi encontrada.");
                Console.ReadKey();
            }
        }
    }

    // --- NOVO MÉTODO PARA GERENCIAR VAGAS ---
    public void gerenciar_vagas_aula(Aula aula_selecionada)
    {
        int id_aluno_para_adicionar;
        do
        {
            Console.Clear();
            Console.WriteLine($"--- Gerenciando Vagas da Aula de {aula_selecionada.modalidade.nome} ---");
            Console.WriteLine($"Horário: {aula_selecionada.horario:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Instrutor: {aula_selecionada.instrutor.nome}");
            Console.WriteLine($"Vagas preenchidas: {aula_selecionada.alunos.Count} de {aula_selecionada.limite_vagas}\n");

            // Lista os alunos já inscritos na aula
            if (aula_selecionada.alunos.Any())
            {
                Console.WriteLine("Alunos já inscritos nesta aula:");
                foreach (var aluno_inscrito in aula_selecionada.alunos)
                {
                    Console.WriteLine($"- ID: {aluno_inscrito.id}, Nome: {aluno_inscrito.nome}");
                }
            }

            // Lista os alunos matriculados que AINDA NÃO estão nesta aula
            var alunos_disponiveis = DadosAcademia.alunos
                .Where(aluno => aluno.esta_matriculado() && !aula_selecionada.alunos.Contains(aluno))
                .ToList();

            Console.WriteLine("\nAlunos matriculados disponíveis para adicionar:");
            if (alunos_disponiveis.Any())
            {
                foreach (var aluno_disponivel in alunos_disponiveis)
                {
                    Console.WriteLine($"- ID: {aluno_disponivel.id}, Nome: {aluno_disponivel.nome}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum outro aluno disponível para inscrição nesta aula.");
            }

            Console.WriteLine("\n----------------------------------------------------");
            Console.Write("Digite o ID do aluno para adicionar à aula (ou [0] para concluir): ");

            if (!int.TryParse(Console.ReadLine(), out id_aluno_para_adicionar))
            {
                Console.WriteLine("ID inválido. Tente novamente.");
                System.Threading.Thread.Sleep(1000);
                continue;
            }

            if (id_aluno_para_adicionar != 0)
            {
                Aluno? aluno_encontrado = DadosAcademia.alunos.FirstOrDefault(a => a.id == id_aluno_para_adicionar);

                if (aluno_encontrado != null)
                {
                    bool sucesso = aula_selecionada.adicionar_aluno(aluno_encontrado);

                    if (!sucesso)
                    {
                        Console.WriteLine("Não foi possível adicionar o aluno (aula lotada ou aluno já inscrito).");
                        System.Threading.Thread.Sleep(1500);
                    }
                }
                else
                {
                    Console.WriteLine("Aluno com o ID informado não foi encontrado.");
                    System.Threading.Thread.Sleep(1500);
                }
            }
        } while (id_aluno_para_adicionar != 0);
    }
}