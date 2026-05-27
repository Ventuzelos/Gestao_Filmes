using System.Collections.Generic;

namespace Gestao_Filmes.Domain
{
    public interface IFilmeRepository
    {
        public void Adicionar(Filme filme);
        public List<Filme> Listar();
        public Filme? ProcurarPorTitulo(string titulo);
        public void Atualizar(Filme filme);
        public void Remover(Filme filme);

    }
}