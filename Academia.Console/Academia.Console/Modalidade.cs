using Academia;

public class Modalidade
{
    public string nome { get; set; }
    public float valor_mensal { get; set; }
    public List<Instrutor> instrutores { get; private set; }

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

    public void adicionar_instrutor(Instrutor instrutor_para_adicionar)
    {
        if (instrutor_para_adicionar == null)
        {
            return;
        }
        if (!this.instrutores.Contains(instrutor_para_adicionar))
        {
            this.instrutores.Add(instrutor_para_adicionar);
            instrutor_para_adicionar.adicionar_modalidade(this.nome);
        }
    }

    public void remover_instrutor(Instrutor instrutor_para_remover)
    {
        if (instrutor_para_remover != null && this.instrutores.Contains(instrutor_para_remover))
        {
            this.instrutores.Remove(instrutor_para_remover);
        }
    }

    public override string ToString()
    {
        return $"{this.nome} - {this.valor_mensal:C}/mês";
    }
}