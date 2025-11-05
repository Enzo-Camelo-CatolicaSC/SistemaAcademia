namespace Academia
{
    public class Aluno : Pessoa
    {
        public Modalidade modalidade_preferida;
        public bool matriculado;

        public Aluno(string nome, string cpf, string telefone, Modalidade modalidade_preferida)
            : base(nome, cpf, telefone)
        {
            this.modalidade_preferida = modalidade_preferida;
            this.matriculado = false;
        }

        public void RealizarMatricula()
        {
            Matricula matricula = new Matricula(this, new List<Modalidade> { this.modalidade_preferida }, 1);
            this.matriculado = true;
            Console.WriteLine($"Aluno {this.nome} matriculado com sucesso!");
        }

        public bool EstaMatriculado()
        {
            return this.matriculado;
        }
    }
}