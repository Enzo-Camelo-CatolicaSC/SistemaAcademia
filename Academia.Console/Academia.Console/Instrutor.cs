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
            var agora = DateTime.Now;

            // Esta variável vai guardar a aula mais próxima que encontrarmos.
            // Começa como nula.
            Aula? proxima_aula_encontrada = null;

            // 1. Percorrer toda a lista de aulas da academia
            foreach (var aula_atual in DadosAcademia.aulas)
            {
                // 2. Filtrar: A aula pertence a este instrutor E está no futuro?
                if (aula_atual.instrutor.id == this.id && aula_atual.horario > agora)
                {
                    // Se a aula_atual passa no filtro, ela é uma candidata a ser a "mais próxima".

                    // 3. Ordenar e Pegar a Primeira (Lógica do OrderBy e FirstOrDefault)
                    // Se ainda não encontramos nenhuma aula, a primeira que acharmos é a mais próxima até agora.
                    if (proxima_aula_encontrada == null)
                    {
                        proxima_aula_encontrada = aula_atual;
                    }
                    // Se já temos uma candidata, verificamos se a aula_atual é AINDA MAIS próxima.
                    else if (aula_atual.horario < proxima_aula_encontrada.horario)
                    {
                        // Se for, substituímos nossa candidata pela nova, que está mais perto no tempo.
                        proxima_aula_encontrada = aula_atual;
                    }
                }
            }

            // 4. Depois que o laço terminar, verificamos o resultado
            if (proxima_aula_encontrada != null)
            {
                // Se a variável não for mais nula, significa que encontramos a aula mais próxima.
                return $"Próxima aula: {proxima_aula_encontrada.modalidade.nome} em {proxima_aula_encontrada.horario:dd/MM/yyyy 'às' HH:mm}";
            }
            else
            {
                // Se a variável continuou nula, nenhuma aula foi encontrada.
                return "Nenhuma aula futura agendada para este instrutor.";
            }
        }
    }
}