using Gestao_Filmes.Business;
using Gestao_Filmes.Data;
using Gestao_Filmes.Domain;


var repository = new FilmeRepositoryMemoria();

var categoriaRepository = new CategoriaSQLiteRepository();
var realizadorRepository = new RealizadorSQLiteRepository();

//var filmeService = new FilmeService(repository); -- da erro tive que substituir por:

var filmeService = new FilmeService(
    repository,
    categoriaRepository,
    realizadorRepository);

var categoriaService = new CategoriaService(categoriaRepository);

var realizadorService = new RealizadorService(realizadorRepository);



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
    Console.WriteLine("6 - Adicionar categoria");
    Console.WriteLine("7 - Listar categorias");
    Console.WriteLine("8 - Remover categoria");
    Console.WriteLine("9 - Adicionar realizador");
    Console.WriteLine("10 - Listar realizadores");
    Console.WriteLine("11 - Remover realizador");
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

            case 6:
                AdicionarCategoria();
                break;

            case 7:
                ListarCategorias();
                break;

            case 8:
                RemoverCategoria();
                break;

            case 9:
                AdicionarRealizador();
                break;

            case 10:
                ListarRealizadores();
                break;

            case 11:
                RemoverRealizador();
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

    //-------------Adicionado para o utilizador acrescentar a Cat e Realizador, pois senão dá erro(a cat é obrigatoria) 
    Console.Write("Categoria: ");
    string nomeCategoria = Console.ReadLine();
    //- tinha u problema que se tivesse um erro de escrita, o programa dava erro e tinha que escrever tudo de novo,
    //adicionado um while pois se erra na cat ou realizador tenho a opção de escrever de novo ou 
    Categoria categoria = categoriaService.ProcurarCategoria(nomeCategoria);

    while (categoria == null)
    {
        Console.WriteLine("Categoria não existe.");
        Console.Write("Escreva outra categoria ou 0 para cancelar: ");
        nomeCategoria = Console.ReadLine();

        if (nomeCategoria == "0")
        {
            Console.WriteLine("Operação cancelada.");
            return;
        }

        categoria = categoriaService.ProcurarCategoria(nomeCategoria);
    }

    Console.Write("Realizador: ");
    string nomeRealizador = Console.ReadLine();
    Realizador realizador = realizadorService.ProcurarRealizador(nomeRealizador);

    while (realizador == null)
    {
        Console.WriteLine("Realizador não existe.");
        Console.Write("Escreva outro realizador ou 0 para cancelar: ");
        nomeRealizador = Console.ReadLine();

        if (nomeRealizador == "0")
        {
            Console.WriteLine("Operação cancelada.");
            return;
        }

        realizador = realizadorService.ProcurarRealizador(nomeRealizador);
    }

    Filme filme = new Filme
    {
        Titulo = titulo,
        Ano = ano,
        Lingua = lingua,
        Classificacao = classificacao,
        Categoria = categoria,
        Realizador = realizador
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
        //Console.WriteLine($"{filme.Id} - {filme.Titulo} ({filme.Ano}) | {filme.Lingua} | {filme.Classificacao}/5");
        Console.WriteLine($"{filme.Id} - {filme.Titulo} ({filme.Ano}) | {filme.Lingua} | {filme.Classificacao}/5 | Categoria: {filme.Categoria.Nome} | Realizador: {filme.Realizador.Nome}");
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

    //Console.WriteLine($"{filme.Id} - {filme.Titulo} ({filme.Ano}) | {filme.Lingua} | {filme.Classificacao}/5");
    Console.WriteLine($"{filme.Id} - {filme.Titulo} ({filme.Ano}) | {filme.Lingua} | {filme.Classificacao}/5 | Categoria: {filme.Categoria.Nome} | Realizador: {filme.Realizador.Nome}");
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

void AdicionarCategoria()
{
    Console.Write("Nome da categoria: ");
    string nome = Console.ReadLine();

    categoriaService.AdicionarCategoria(nome);

    Console.WriteLine("Categoria adicionada com sucesso.");
}

void ListarCategorias()
{
    var categorias = categoriaService.ListarCategorias();

    if (categorias.Count == 0)
    {
        Console.WriteLine("Não existem categorias registadas.");
        return;
    }

    foreach (var categoria in categorias)
    {
        Console.WriteLine($"{categoria.Id} - {categoria.Nome}");
    }
}

void RemoverCategoria()
{
    Console.Write("Id da categoria a remover: ");
    int id = int.Parse(Console.ReadLine());

    categoriaService.RemoverCategoria(id);

    Console.WriteLine("Categoria removida com sucesso.");
}

void AdicionarRealizador()
{
    Console.Write("Nome do realizador: ");
    string nome = Console.ReadLine();

    Console.Write("País do realizador: ");
    string pais = Console.ReadLine();

    realizadorService.AdicionarRealizador(nome, pais);

    Console.WriteLine("Realizador adicionado com sucesso.");
}

void ListarRealizadores()
{
    var realizadores = realizadorService.ListarRealizadores();

    if (realizadores.Count == 0)
    {
        Console.WriteLine("Não existem realizadores registados.");
        return;
    }

    foreach (var realizador in realizadores)
    {
        Console.WriteLine($"{realizador.Id} - {realizador.Nome} | {realizador.Pais}");
    }
}

void RemoverRealizador()
{
    Console.Write("Id do realizador a remover: ");
    int id = int.Parse(Console.ReadLine());

    realizadorService.RemoverRealizador(id);

    Console.WriteLine("Realizador removido com sucesso.");
}

void RemoverFilme()
{
    Console.Write("Título a remover: ");
    string titulo = Console.ReadLine();

    filmeService.RemoverFilme(titulo);

    Console.WriteLine("Filme removido com sucesso.");
}