using Academia;

public class Aluno : Pessoa
{
    public Modalidade modalidade_preferida;
    public bool matriculado;

    public Aluno(string nome, string cpf, string telefone, Modalidade modalidade_preferida)
        : base(nome, cpf, telefone)
    {
        if (modalidade_preferida == null)
        {
            throw new ArgumentNullException(nameof(modalidade_preferida), "A modalidade preferida não pode ser nula.");
        }

        this.modalidade_preferida = modalidade_preferida;
        this.matriculado = false;
    }

    public void realizar_matricula()
    {
        this.matriculado = true;
    }

    public void cancelar_matricula()
    {
        this.matriculado = false;
    }

    public bool esta_matriculado()
    {
        return this.matriculado;
    }
}