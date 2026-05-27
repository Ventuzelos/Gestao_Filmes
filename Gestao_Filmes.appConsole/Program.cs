using Gestao_Filmes.Business;
using Gestao_Filmes.Data;
using Gestao_Filmes.Domain;

var repository = new FilmeRepositoryMemoria();
var filmeService = new FilmeService(repository);

int opcao;

do
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("         GESTÃO DE FILMES        ");
    Console.WriteLine("---------------------------------");

    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("1 - Adicionar filme");
    Console.WriteLine("2 - Listar filmes");
    Console.WriteLine("3 - Procurar filme");
    Console.WriteLine("4 - Atualizar filme");
    Console.WriteLine("5 - Remover filme");
    Console.WriteLine("0 - Sair");

    Console.ResetColor();

    Console.WriteLine("---------------------------------");
    Console.Write("");
    Console.Write("Escolha uma opção: ");
 

    int.TryParse(Console.ReadLine(), out opcao);

    try
    {
        switch (opcao)
        {
            case 1:
                AdicionarFilme();
                break;

            case 2:
                ListarFilmes();
                break;

            case 3:
                ProcurarFilme();
                break;

            case 4:
                AtualizarFilme();
                break;

            case 5:
                RemoverFilme();
                break;

            case 0:
                Console.WriteLine("A sair...");
                break;

            default:
                Console.WriteLine("Opção inválida. Escolha novamente");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }

    Console.WriteLine();
    Console.WriteLine("Prima qualquer tecla para continuar...");
    Console.ReadKey();

} while (opcao != 0);


void AdicionarFilme()
{
    Console.Write("Título: ");
    string titulo = Console.ReadLine();

    Console.Write("Ano: ");
    int ano = int.Parse(Console.ReadLine());

    Console.Write("Língua: ");
    string lingua = Console.ReadLine();

    Console.Write("Classificação (0 a 5): ");

    if (!decimal.TryParse(Console.ReadLine(), out decimal classificacao))
    {
        Console.WriteLine("Classificação inválida.");
        return;
    }

    Filme filme = new Filme
    {
        Titulo = titulo,
        Ano = ano,
        Lingua = lingua,
        Classificacao = classificacao
    };

    filmeService.AdicionarFilme(filme);

    Console.WriteLine("Filme adicionado com sucesso.");
}

void ListarFilmes()
{
    var filmes = filmeService.ListarFilmes();

    if (filmes.Count == 0)
    {
        Console.WriteLine("Não existem filmes registados.");
        return;
    }

    foreach (var filme in filmes)
    {
        Console.WriteLine($"{filme.Id} - {filme.Titulo} ({filme.Ano}) | {filme.Lingua} | {filme.Classificacao}/5");
    }
}

void ProcurarFilme()
{
    Console.Write("Título a procurar: ");
    string titulo = Console.ReadLine();

    var filme = filmeService.ProcurarFilme(titulo);

    if (filme == null)
    {
        Console.WriteLine("Filme não encontrado.");
        return;
    }

    Console.WriteLine($"{filme.Id} - {filme.Titulo} ({filme.Ano}) | {filme.Lingua} | {filme.Classificacao}/5");
}

void AtualizarFilme()
{
    Console.Write("Título do filme a atualizar: ");
    string titulo = Console.ReadLine();

    var filme = filmeService.ProcurarFilme(titulo);

    if (filme == null)
    {
        Console.WriteLine("Filme não encontrado.");
        return;
    }

    Console.Write("Nova classificação (0 a 5): ");

    if (!decimal.TryParse(Console.ReadLine(), out decimal classificacao))
    {
        Console.WriteLine("Classificação inválida.");
        return;
    }

    filme.Classificacao = classificacao;

    filmeService.AtualizarFilme(filme);

    Console.WriteLine("Filme atualizado com sucesso.");
}

void RemoverFilme()
{
    Console.Write("Título a remover: ");
    string titulo = Console.ReadLine();

    filmeService.RemoverFilme(titulo);

    Console.WriteLine("Filme removido com sucesso.");
}