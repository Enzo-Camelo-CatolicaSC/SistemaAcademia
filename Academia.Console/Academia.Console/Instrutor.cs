namespace Academia
{
    public class Instrutor : Pessoa
    {
        public List<string> modalidades;
        public float salario;
        public Instrutor(string nome, string cpf, string telefone, float salario)
            : base(nome, cpf, telefone) // Chama o construtor da classe base (Pessoa)
        {
            this.salario = salario;
            this.modalidades = new List<string>(); // Sempre inicialize listas!
        }

        public void adicionar_modalidade(string nome_modalidade)
        {
            if (!string.IsNullOrWhiteSpace(nome_modalidade) && !this.modalidades.Contains(nome_modalidade))
            {
                this.modalidades.Add(nome_modalidade);
            }
        }

        public string aula_mais_proxima()
        {
            // Lógica para buscar a próxima aula no sistema
            return "Aula de Spinning - Amanhã às 18h00";
        }
    }
}