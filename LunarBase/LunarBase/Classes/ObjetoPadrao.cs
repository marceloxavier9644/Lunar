using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    public class ObjetoPadrao
    {
        private DateTime dataCadastro;
        private DateTime dataAlteracao;
        private DateTime dataExclusao;
        private String operadorCadastro;
        private String operadorAlteracao;
        private String operadorExclusao;
        private bool flagExcluido;

        [Anotacao("Data de Cadastro")]
        public virtual DateTime DataCadastro
        {
            get { return dataCadastro; }
            set { dataCadastro = value; }
        }

        [Anotacao("Data de Alteração")]
        public virtual DateTime DataAlteracao
        {
            get { return dataAlteracao; }
            set { dataAlteracao = value; }
        }

        [Anotacao("Data de Exclusão")]
        public virtual DateTime DataExclusao
        {
            get { return dataExclusao; }
            set { dataExclusao = value; }
        }

        [Anotacao("Operador do Cadastro")]
        public virtual String OperadorCadastro
        {
            get { return operadorCadastro; }
            set { operadorCadastro = value; }
        }

        [Anotacao("Operador da Alteração")]
        public virtual String OperadorAlteracao
        {
            get { return operadorAlteracao; }
            set { operadorAlteracao = value; }
        }

        [Anotacao("Operador da Exclusão")]
        public virtual String OperadorExclusao
        {
            get { return operadorExclusao; }
            set { operadorExclusao = value; }
        }

        [Anotacao("Excluído")]
        public virtual bool FlagExcluido
        {
            get { return flagExcluido; }
            set { flagExcluido = value; }
        }
    }
}
