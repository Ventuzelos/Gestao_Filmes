using System.Collections.Generic;

namespace Gestao_Filmes.Domain
{
    public interface ICategoriaRepository
    {
        void Adicionar(Categoria categoria);
        List<Categoria> Listar();
        Categoria ProcurarPorNome(string nome);
        void Remover(int id);
    }
}