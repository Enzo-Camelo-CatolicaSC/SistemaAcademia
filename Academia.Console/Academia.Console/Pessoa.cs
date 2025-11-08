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

            if (!validar_cpf(cpf))
                throw new ArgumentException("O CPF fornecido é inválido.");

            this.nome = nome;
            this.cpf = cpf;
            this.telefone = telefone;
            this.id = gerar_id();
        }

        public int gerar_id()
        {
            return new Random().Next(1000, 9999);
        }

        public void alterar_cpf(string novo_cpf)
        {
            if (!validar_cpf(novo_cpf))
            {
                throw new ArgumentException("O novo CPF fornecido é inválido.");
            }
            this.cpf = novo_cpf.Trim().Replace(".", "").Replace("-", "");
        }

        public bool validar_cpf(string cpf)
        {
            if (this.cpf.Length != 11)
                return false;
            return true;
        }
    }
}