﻿using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Ordem Serviço - Serviços")]
    public class OrdemServicoServico : ObjetoPadrao
    {
        private int id;
        private string descricaoServico;
        private decimal valorUnitario;
        private decimal desconto;
        private decimal acrescimo;
        private decimal valorTotal;
        private double quantidade;
        private Servico servico;
        private OrdemServico ordemServico;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Descrição")]
        public virtual string DescricaoServico { get => descricaoServico; set => descricaoServico = value; }
        [Anotacao("Valor Unitário")]
        public virtual decimal ValorUnitario { get => valorUnitario; set => valorUnitario = value; }
        [Anotacao("Desconto")]
        public virtual decimal Desconto { get => desconto; set => desconto = value; }
        [Anotacao("Acrescimo")]
        public virtual decimal Acrescimo { get => acrescimo; set => acrescimo = value; }
        [Anotacao("Valor Total")]
        public virtual decimal ValorTotal { get => valorTotal; set => valorTotal = value; }
        [Anotacao("Quantidade")]
        public virtual double Quantidade { get => quantidade; set => quantidade = value; }
        [Anotacao("Serviço")]
        public virtual Servico Servico { get => servico; set => servico = value; }
        [Anotacao("Ordem de Serviço")]
        public virtual OrdemServico OrdemServico { get => ordemServico; set => ordemServico = value; }

        public override string ToString()
        {
            return descricaoServico;
        }
    }
}
