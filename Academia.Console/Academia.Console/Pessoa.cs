namespace Academia
{
    public abstract class Pessoa
    {
        public int id;
        public string nome;
        public string cpf;
        public string telefone;

        public Pessoa(string nome, string cpf, string telefone)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome não pode ser vazio.");

            // Aqui você colocaria uma validação de CPF mais robusta
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
                throw new ArgumentException("CPF inválido.");

            this.nome = nome;
            this.cpf = cpf;
            this.telefone = telefone;
            this.id = gerar_id();
        }

        public int gerar_id()
        {
            // Lógica para gerar um ID único
            return new Random().Next(1000, 9999);
        }

        public bool cpf_e_valido()
        {
            // Lógica real de validação de CPF
            return true;
        }
    }
}