using Academia;

public static class DadosAcademia
{
    // Listas estáticas para armazenar todos os dados do sistema
    public static List<Aluno> alunos = new List<Aluno>();
    public static List<Instrutor> instrutores = new List<Instrutor>();
    public static List<Modalidade> modalidades = new List<Modalidade>();

    public static void inicializar()
    {
        if (modalidades.Count > 0) return;

        // Cria e adiciona algumas modalidades
        var musculacao = new Modalidade("Musculação", 99.90f);
        var yoga = new Modalidade("Yoga", 120.00f);
        var spinning = new Modalidade("Spinning", 110.50f);
        var pilates = new Modalidade("Pilates", 150.00f);

        modalidades.Add(musculacao);
        modalidades.Add(yoga);
        modalidades.Add(spinning);
        modalidades.Add(pilates);
    }
}