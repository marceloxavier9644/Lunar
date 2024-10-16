using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Classes
{
    public class BoletoConfig : ObjetoPadrao
    {
        private int id;
        private string descricao;
        private string codigoBeneficiario;
        private decimal multa;
        private decimal juroMensal;
        private string numeroCarteira;
        private string identificacaoCliente;
        private string cooperativa;
        private string posto;
        private string usuario;
        private string senha;
        private string idToken;
        private string token;
        private Boolean contaPadrao;
        private string tipoBoleto;
        private ContaBancaria contaBancaria;
        private PlanoConta planoContaTarifa;
        private Boolean ambienteProducao;
        public virtual int Id { get => id; set => id = value; }
        public virtual string Descricao { get => descricao; set => descricao = value; }
        public virtual string CodigoBeneficiario { get => codigoBeneficiario; set => codigoBeneficiario = value; }
        public virtual decimal Multa { get => multa; set => multa = value; }
        public virtual decimal JuroMensal { get => juroMensal; set => juroMensal = value; }
        public virtual string NumeroCarteira { get => numeroCarteira; set => numeroCarteira = value; }
        public virtual string IdentificacaoCliente { get => identificacaoCliente; set => identificacaoCliente = value; }
        public virtual string Cooperativa { get => cooperativa; set => cooperativa = value; }
        public virtual string Posto { get => posto; set => posto = value; }
        public virtual string Usuario { get => usuario; set => usuario = value; }
        public virtual string Senha { get => senha; set => senha = value; }
        public virtual string IdToken { get => idToken; set => idToken = value; }
        public virtual string Token { get => token; set => token = value; }
        public virtual bool ContaPadrao { get => contaPadrao; set => contaPadrao = value; }
        public virtual string TipoBoleto { get => tipoBoleto; set => tipoBoleto = value; }
        public virtual ContaBancaria ContaBancaria { get => contaBancaria; set => contaBancaria = value; }
        public virtual PlanoConta PlanoContaTarifa { get => planoContaTarifa; set => planoContaTarifa = value; }
        public virtual bool AmbienteProducao { get => ambienteProducao; set => ambienteProducao = value; }
    }
}
