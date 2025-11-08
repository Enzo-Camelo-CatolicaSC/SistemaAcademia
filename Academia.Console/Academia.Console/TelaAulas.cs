using System;
using System.Collections.Generic;
using System.Linq;

namespace Academia
{
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

            if (!DadosAcademia.modalidades.Any() || !DadosAcademia.instrutores.Any())
            {
                Console.WriteLine("É preciso ter ao menos uma modalidade e um instrutor cadastrados para agendar uma aula.");
                Console.ReadKey();
                return;
            }

            try
            {
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

                Console.Write("Digite a data e hora da aula (formato dd/MM/yyyy HH:mm): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime horario))
                {
                    throw new Exception("Formato de data ou hora inválido.");
                }

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

    // Lista todas as aulas
    foreach (var aula in DadosAcademia.aulas.OrderBy(a => a.horario))
    {
        Console.WriteLine($"ID: {aula.id}");
        Console.WriteLine($"Modalidade: {aula.modalidade.nome}");
        Console.WriteLine($"Instrutor: {aula.instrutor.nome}");
        Console.WriteLine($"Horário: {aula.horario:dd/MM/yyyy HH:mm}");
        Console.WriteLine($"Duração: {aula.tempo_de_aula} minutos");
        Console.WriteLine($"Vagas: {aula.alunos.Count}/{aula.limite_vagas}");
        Console.WriteLine("---------------------------------");
    }

    // Pergunta se deseja buscar
    Console.Write("\nDeseja buscar uma aula pelo ID? (s/n): ");
    string opcao = Console.ReadLine().Trim().ToLower();

    if (opcao == "s")
    {
        Console.Write("\nDigite o ID da aula que deseja buscar: ");
        if (int.TryParse(Console.ReadLine(), out int idBusca))
        {
            var aulaEncontrada = DadosAcademia.aulas
                .FirstOrDefault(a => a.id == idBusca);

            Console.Clear();
            if (aulaEncontrada != null)
            {
                Console.WriteLine("--- Aula Encontrada ---");
                Console.WriteLine($"ID: {aulaEncontrada.id}");
                Console.WriteLine($"Modalidade: {aulaEncontrada.modalidade.nome}");
                Console.WriteLine($"Instrutor: {aulaEncontrada.instrutor.nome}");
                Console.WriteLine($"Horário: {aulaEncontrada.horario:dd/MM/yyyy HH:mm}");
                Console.WriteLine($"Duração: {aulaEncontrada.tempo_de_aula} minutos");
                Console.WriteLine($"Vagas: {aulaEncontrada.alunos.Count}/{aulaEncontrada.limite_vagas}");

                Console.WriteLine("\nO que deseja fazer?");
                Console.WriteLine("[1] Alterar dados da aula");
                Console.WriteLine("[2] Excluir aula");
                Console.WriteLine("[3] Voltar");
                Console.Write("\nEscolha uma opção: ");
                string escolha = Console.ReadLine().Trim();

                switch (escolha)
                {
                    case "1":
                        alterar_dados_aula(aulaEncontrada);
                        break;

                    case "2":
                        Console.Write("\nTem certeza que deseja excluir esta aula? (s/n): ");
                        string confirma = Console.ReadLine().Trim().ToLower();

                        if (confirma == "s")
                        {
                            DadosAcademia.aulas.Remove(aulaEncontrada);
                            Console.WriteLine("\nAula excluída com sucesso!");
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
                Console.WriteLine("Nenhuma aula encontrada com esse ID.");
            }
        }
        else
        {
            Console.WriteLine("ID inválido!");
        }
    }

    Console.WriteLine("\nPressione qualquer tecla para voltar...");
    Console.ReadKey();
}

public void alterar_dados_aula(Aula aula)
{
    Console.Clear();
    Console.WriteLine($"--- Alterar Dados da Aula: {aula.modalidade.nome} ---");

    try
    {
        Console.WriteLine("\nPressione Enter para manter o valor atual.");

        Console.Write($"\nData/Horário atual ({aula.horario:dd/MM/yyyy HH:mm}): ");
        string s = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(s) && DateTime.TryParse(s, out DateTime novoHorario))
            aula.horario = novoHorario;

        Console.Write($"Limite de vagas atual ({aula.limite_vagas}): ");
        s = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(s) && int.TryParse(s, out int novoLimite))
            aula.limite_vagas = novoLimite;

        Console.Write($"Duração atual ({aula.tempo_de_aula} minutos): ");
        s = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(s) && int.TryParse(s, out int novaDuracao))
            aula.tempo_de_aula = novaDuracao;

        Console.WriteLine("\nDados da aula atualizados com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nOcorreu um erro ao alterar os dados: {ex.Message}");
    }

    Console.WriteLine("\nPressione qualquer tecla para voltar...");
    Console.ReadKey();
}


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

                if (aula_selecionada.alunos.Any())
                {
                    Console.WriteLine("Alunos já inscritos nesta aula:");
                    foreach (var aluno_inscrito in aula_selecionada.alunos)
                    {
                        Console.WriteLine($"- ID: {aluno_inscrito.id}, Nome: {aluno_inscrito.nome}");
                    }
                }

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

        // ===========================================================
        // MÉTODOS NOVOS: Buscar, Alterar e Excluir Aula
        // ===========================================================
        private void BuscarAulaPorId()
        {
            Console.Clear();
            Console.WriteLine("=== BUSCAR AULA PELO ID ===");

            Console.Write("Digite o ID da aula: ");
            if (int.TryParse(Console.ReadLine(), out int idBusca))
            {
                var aulaEncontrada = DadosAcademia.aulas.FirstOrDefault(a => a.id == idBusca);

                if (aulaEncontrada != null)
                {
                    Console.Clear();
                    Console.WriteLine("=== DADOS DA AULA ===");
                    Console.WriteLine($"ID: {aulaEncontrada.id}");
                    Console.WriteLine($"Modalidade: {aulaEncontrada.modalidade?.nome ?? "N/A"}");
                    Console.WriteLine($"Instrutor: {aulaEncontrada.instrutor?.nome ?? "N/A"}");
                    Console.WriteLine($"Horário: {aulaEncontrada.horario:dd/MM/yyyy HH:mm}");
                    Console.WriteLine($"Duração: {aulaEncontrada.tempo_de_aula} min");
                    Console.WriteLine($"Vagas: {aulaEncontrada.alunos.Count}/{aulaEncontrada.limite_vagas}");
                    Console.WriteLine();
                    Console.WriteLine("[1] Alterar Dados da Aula");
                    Console.WriteLine("[2] Excluir Aula");
                    Console.WriteLine("[0] Voltar");
                    Console.Write("Escolha uma opção: ");
                    string opcao = Console.ReadLine();

                    switch (opcao)
                    {
                        case "1":
                            AlterarDadosAula(aulaEncontrada);
                            break;
                        case "2":
                            ExcluirAula(aulaEncontrada);
                            break;
                        default:
                            Console.WriteLine("Voltando...");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Aula não encontrada!");
                }
            }
            else
            {
                Console.WriteLine("ID inválido!");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private void AlterarDadosAula(Aula aula)
        {
            Console.Clear();
            Console.WriteLine("=== ALTERAR DADOS DA AULA ===");

            Console.Write($"Nova data/hora ({aula.horario:dd/MM/yyyy HH:mm}) ou ENTER: ");
            string s = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(s) && DateTime.TryParse(s, out DateTime novoHorario))
                aula.horario = novoHorario;

            Console.Write($"Novo limite ({aula.limite_vagas}) ou ENTER: ");
            s = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(s) && int.TryParse(s, out int novoLimite))
                aula.limite_vagas = novoLimite;

            Console.Write($"Nova duração ({aula.tempo_de_aula}) ou ENTER: ");
            s = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(s) && int.TryParse(s, out int novaDuracao))
                aula.tempo_de_aula = novaDuracao;

            Console.WriteLine("\nAula atualizada com sucesso!");
            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private void ExcluirAula(Aula aula)
        {
            Console.Write("\nTem certeza que deseja excluir esta aula? (S/N): ");
            string confirm = Console.ReadLine().Trim().ToUpper();

            if (confirm == "S")
            {
                DadosAcademia.aulas.Remove(aula);
                Console.WriteLine("Aula excluída com sucesso!");
            }
            else
            {
                Console.WriteLine("Exclusão cancelada.");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }
}
