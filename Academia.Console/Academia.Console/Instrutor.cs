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

        public void AdicionarModalidade(string modalidade)
        {
            if (!modalidades.Contains(modalidade))
            {
                modalidades.Add(modalidade);
            }
        }

        public string AulaMaisProxima()
        {
            // Lógica para buscar a próxima aula no sistema
            return "Aula de Spinning - Amanhã às 18h00";
        }
    }
}