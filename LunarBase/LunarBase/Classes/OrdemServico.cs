﻿using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Ordem de Serviço")]
    public class OrdemServico : ObjetoPadrao
    {
        private int id;
        private string status;
        private Pessoa cliente;
        private string numeroSerie;
        private string observacoes;
        private decimal valorProduto;
        private decimal valorDesconto;
        private decimal valorAcrescimo;
        private decimal valorServico;
        private decimal valorTotal;
        private DateTime dataAbertura;
        private DateTime dataEncerramento;
        private TipoObjeto tipoObjeto;
        private EmpresaFilial filial;
        private Nfe nfe;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Numero O.S")]
        public virtual string Status { get => status; set => status = value; }
        [Anotacao("Cliente")]
        public virtual Pessoa Cliente { get => cliente; set => cliente = value; }
        [Anotacao("Numero Serie")]
        public virtual string NumeroSerie { get => numeroSerie; set => numeroSerie = value; }
        [Anotacao("Observações")]
        public virtual string Observacoes { get => observacoes; set => observacoes = value; }
        [Anotacao("Valor Produtos")]
        public virtual decimal ValorProduto { get => valorProduto; set => valorProduto = value; }
        [Anotacao("Valor Desconto")]
        public virtual decimal ValorDesconto { get => valorDesconto; set => valorDesconto = value; }
        [Anotacao("Valor Acréscimo")]
        public virtual decimal ValorAcrescimo { get => valorAcrescimo; set => valorAcrescimo = value; }
        [Anotacao("Valor Serviço")]
        public virtual decimal ValorServico { get => valorServico; set => valorServico = value; }
        [Anotacao("Valor Total")]
        public virtual decimal ValorTotal { get => valorTotal; set => valorTotal = value; }
        [Anotacao("Data Abertura")]
        public virtual DateTime DataAbertura { get => dataAbertura; set => dataAbertura = value; }
        [Anotacao("Data Encerramento")]
        public virtual DateTime DataEncerramento { get => dataEncerramento; set => dataEncerramento = value; }
        [Anotacao("Tipo de Objeto")]
        public virtual TipoObjeto TipoObjeto { get => tipoObjeto; set => tipoObjeto = value; }
        [Anotacao("Filial")]
        public virtual EmpresaFilial Filial { get => filial; set => filial = value; }
        [Anotacao("NFe")]
        public virtual Nfe Nfe { get => nfe; set => nfe = value; }
    }
}
