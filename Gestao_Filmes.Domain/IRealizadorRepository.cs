using System.Collections.Generic;

namespace Gestao_Filmes.Domain
{
    public interface IRealizadorRepository
    {
        void Adicionar(Realizador realizador);
        List<Realizador> Listar();
        Realizador ProcurarPorNome(string nome);
        void Remover(int id);
    }
}