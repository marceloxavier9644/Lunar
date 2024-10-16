using LunarBase.Anotations;
using System;
using System.Collections.Generic;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Perfil")]
    public class PessoaPerfil : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private Pessoa pessoa;
        private PessoaCaracteristica pessoaCaracteristica;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Pessoa")]
        public virtual Pessoa Pessoa { get => pessoa; set => pessoa = value; }
        [Anotacao("Caracteristica")]
        public virtual PessoaCaracteristica PessoaCaracteristica { get => pessoaCaracteristica; set => pessoaCaracteristica = value; }
    }
}
