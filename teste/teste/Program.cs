using System; // Inclua esta linha no início do seu arquivo classJogador.cs

class Jogador
{
    public int Energia { get; private set; }
    public bool Vivo { get; private set; }

    public Jogador(int energiaInicial, bool vivoInicial)
    {
        Energia = energiaInicial;
        Vivo = vivoInicial;
    }

    public override string ToString()
    {
        return $"Jogador (Energia: {Energia}, Vivo: {Vivo})";
    }
}

class Teste
{
    static void Main()
    {
        Jogador j1 = new Jogador(60, true);

        Console.WriteLine(j1); // Agora o Console.WriteLine() será reconhecido
    }
}