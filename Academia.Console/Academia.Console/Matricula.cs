namespace Academia
{
    public class Matricula
    {
        public int id;
        public bool esta_ativa;
        public Aluno aluno;
        public DateTime data_matricula;
        public int meses_de_duracao;
        public DateTime data_vencimento;
        public List<Modalidade> modalidades;
        public float valor_total;

        public Matricula(Aluno aluno, List<Modalidade> modalidades, int meses_de_duracao)
        {
            if (aluno == null)
                throw new ArgumentNullException(nameof(aluno), "O aluno não pode ser nulo.");

            if (modalidades == null || !modalidades.Any())
                throw new ArgumentException("A matrícula deve conter pelo menos uma modalidade.", nameof(modalidades));

            if (meses_de_duracao <= 0)
                throw new ArgumentException("A duração deve ser de pelo menos 1 mês.", nameof(meses_de_duracao));

            this.id = GerarID();
            this.aluno = aluno;
            this.modalidades = modalidades;
            this.meses_de_duracao = meses_de_duracao;

            this.data_matricula = DateTime.Now;
            this.data_vencimento = DateTime.Now.AddMonths(meses_de_duracao);
            this.esta_ativa = true;

            this.aluno.RealizarMatricula();

            CalcularValor();
        }

        public void CalcularValor()
        {
            float valor_mensal_total = modalidades.Sum(mod => mod.valor_mensal);
            this.valor_total = valor_mensal_total * this.meses_de_duracao;
        }

        public void RenovarMatricula(int novos_meses_de_duracao)
        {
            this.meses_de_duracao = novos_meses_de_duracao;
            this.data_matricula = DateTime.Now;
            this.data_vencimento = DateTime.Now.AddMonths(novos_meses_de_duracao);

            CalcularValor();

            Console.WriteLine($"Matrícula {this.id} renovada por {novos_meses_de_duracao} meses. Novo vencimento: {this.data_vencimento.ToShortDateString()}");
        }

        public void CancelarMatricula()
        {
            this.esta_ativa = false;
            Console.WriteLine($"Matrícula {this.id} do aluno {this.aluno.nome} foi cancelada.");
        }

        public int GerarID()
        {
            return new Random().Next(1000, 9999);
        }
    }
}