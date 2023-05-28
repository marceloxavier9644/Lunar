using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("TransferenciaEstoque")]
    public class TransferenciaEstoque : ObjetoPadrao
    {
        private int id;
        private DateTime data;
        private string descricao;
        private decimal valorTotal;
        private EmpresaFilial empresaOrigem;
        private EmpresaFilial empresaDestino;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Data")]
        public virtual DateTime Data { get => data; set => data = value; }
        [Anotacao("Descrição")]
        public virtual string Descricao { get => descricao; set => descricao = value; }
        [Anotacao("Valor Total")]
        public virtual decimal ValorTotal { get => valorTotal; set => valorTotal = value; }
        [Anotacao("Empresa Origem")]
        public virtual EmpresaFilial EmpresaOrigem { get => empresaOrigem; set => empresaOrigem = value; }
        [Anotacao("Empresa Destino")]
        public virtual EmpresaFilial EmpresaDestino { get => empresaDestino; set => empresaDestino = value; }


        public override string ToString()
        {
            return descricao;
        }
    }
}
