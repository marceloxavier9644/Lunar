using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Classes
{
    public class PessoaPerfil : ObjetoPadrao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<PessoaCaracteristica> Caracteristicas { get; set; }

        public PessoaPerfil(int id, string nome)
        {
            Id = id;
            Nome = nome;
            Caracteristicas = new List<PessoaCaracteristica>();
        }

        // Método para adicionar características ao perfil
        public void AdicionarCaracteristica(Caracteristica caracteristica, string valor)
        {
            PessoaCaracteristica pessoaCaracteristica = new PessoaCaracteristica
            {
                Caracteristica = caracteristica,
                Valor = valor
            };
            Caracteristicas.Add(pessoaCaracteristica);
        }
    }
}
