using System.Text;

namespace Academia
{
    public class Aula
    {
        public int id;
        public Modalidade modalidade;
        public DateTime horario;
        public Instrutor instrutor;
        public List<Aluno> alunos;
        public int limite_vagas;
        public int tempo_de_aula;

        public Aula(Modalidade modalidade, Instrutor instrutor, DateTime horario, int limite_vagas, int tempo_de_aula_minutos)
        {
            if (modalidade == null || instrutor == null)
            {
                throw new ArgumentNullException("Modalidade e instrutor são obrigatórios.");
            }

            if (!modalidade.instrutores.Contains(instrutor))
            {
                throw new InvalidOperationException($"O instrutor {instrutor.nome} não está qualificado para a modalidade {modalidade.nome}.");
            }

            if (limite_vagas <= 0)
            {
                throw new ArgumentException("O limite de vagas deve ser positivo.");
            }

            this.id = this.gerar_id();
            this.modalidade = modalidade;
            this.instrutor = instrutor;
            this.horario = horario;
            this.limite_vagas = limite_vagas;
            this.tempo_de_aula = tempo_de_aula_minutos;
            this.alunos = new List<Aluno>();
        }

        public int gerar_id()
        {
            return new Random().Next(1000, 9999);
        }

        public bool validar_vagas()
        {
            return this.alunos.Count < this.limite_vagas;
        }

        public bool adicionar_aluno(Aluno aluno_para_inscrever)
        {
            if (aluno_para_inscrever == null) return false;

            if (validar_vagas() && !this.alunos.Contains(aluno_para_inscrever))
            {
                this.alunos.Add(aluno_para_inscrever);
                Console.WriteLine($"{aluno_para_inscrever.nome} inscrito na aula de {this.modalidade.nome}.");
                return true;
            }
            else
            {
                if (!validar_vagas())
                {
                    Console.WriteLine($"Não há vagas para a aula de {this.modalidade.nome}.");
                }
                else
                {
                    Console.WriteLine($"{aluno_para_inscrever.nome} já está inscrito nesta aula.");
                }
                return false;
            }
        }

        public bool verificar_presenca(List<int> ids_alunos_presentes)
        {
            int alunos_encontrados = 0;
            foreach (int aluno_id in ids_alunos_presentes)
            {
                if (this.alunos.Any(a => a.id == aluno_id))
                {
                    alunos_encontrados++;
                }
            }

            Console.WriteLine($"Presença registrada para {alunos_encontrados} de {ids_alunos_presentes.Count} alunos.");
            return alunos_encontrados == ids_alunos_presentes.Count;
        }

        public string gerar_relatorio()
        {
            var relatorio = new StringBuilder();
            relatorio.AppendLine("--- Relatório da Aula ---");
            relatorio.AppendLine($"ID da Aula: {this.id}");
            relatorio.AppendLine($"Modalidade: {this.modalidade.nome}");
            relatorio.AppendLine($"Instrutor: {this.instrutor.nome}");
            relatorio.AppendLine($"Data e Hora: {this.horario:dd/MM/yyyy HH:mm}");
            relatorio.AppendLine($"Duração: {this.tempo_de_aula} minutos");
            relatorio.AppendLine($"Vagas: {this.alunos.Count} / {this.limite_vagas}");
            relatorio.AppendLine("-------------------------");

            return relatorio.ToString();
        }

        public string informar_alunos_inscritos()
        {
            if (!this.alunos.Any())
            {
                return "Nenhum aluno inscrito nesta aula.";
            }

            var lista_alunos = new StringBuilder();
            lista_alunos.AppendLine($"Alunos inscritos na aula de {this.modalidade.nome}:");
            foreach (var aluno in this.alunos)
            {
                lista_alunos.AppendLine($"- ID: {aluno.id}, Nome: {aluno.nome}");
            }

            return lista_alunos.ToString();
        }
    }
}