namespace Academia
{
    public class Modalidade
    {
        public string nome;
        public float valor_mensal;
        public List<Instrutor> instrutores;

        public Modalidade(string nome, float valor_mensal)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome da modalidade não pode ser vazio.", nameof(nome));

            if (valor_mensal < 0)
                throw new ArgumentException("O valor mensal não pode ser negativo.", nameof(valor_mensal));

            this.nome = nome;
            this.valor_mensal = valor_mensal;
            this.instrutores = new List<Instrutor>();
        }

        public void AdicionarInstrutor(Instrutor instrutor)
        {
            if (instrutor == null)
            {
                Console.WriteLine("Erro: Não é possível adicionar um instrutor nulo.");
                return;
            }

            // Verifica se o instrutor já não está na lista para evitar duplicatas.
            if (!this.instrutores.Contains(instrutor))
            {
                this.instrutores.Add(instrutor);

                // Também é uma boa prática adicionar a modalidade na lista do instrutor (associação bidirecional)
                instrutor.AdicionarModalidade(this.nome);
            }
            else
            {
                Console.WriteLine($"O instrutor {instrutor.nome} já está associado à modalidade {this.nome}.");
            }
        }

        public void RemoverInstrutor(Instrutor instrutor)
        {
            if (instrutor != null && this.instrutores.Contains(instrutor))
            {
                this.instrutores.Remove(instrutor);
            }
        }

        public override string ToString()
        {
            return $"{this.nome} - {this.valor_mensal:C}/mês";
        }
    }
}