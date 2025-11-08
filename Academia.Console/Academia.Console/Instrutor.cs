using System;
using System.Collections.Generic;

namespace Academia
{
    public class Instrutor : Pessoa
    {
        public List<string> modalidades;
        public float salario;

        public Instrutor(string nome, string cpf, string telefone, float salario)
            : base(nome, cpf, telefone)
        {
            this.salario = salario;
            this.modalidades = new List<string>();
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
            List<Aula> todasAulas = DadosAcademia.aulas != null
                ? DadosAcademia.aulas
                : new List<Aula>();

            Aula? proxima_aula_encontrada = null;

            foreach (Aula aula_atual in todasAulas)
            {
                if (aula_atual.instrutor != null &&
                    aula_atual.instrutor.id == this.id &&
                    aula_atual.horario > agora)
                {
                    if (proxima_aula_encontrada == null ||
                        aula_atual.horario < proxima_aula_encontrada.horario)
                    {
                        proxima_aula_encontrada = aula_atual;
                    }
                }
            }

            if (proxima_aula_encontrada != null)
            {
                return $"Próxima aula: {proxima_aula_encontrada.modalidade.nome} em {proxima_aula_encontrada.horario:dd/MM/yyyy 'às' HH:mm}";
            }
            else
            {
                return "Nenhuma aula futura agendada para este instrutor.";
            }
        }
    }
}
