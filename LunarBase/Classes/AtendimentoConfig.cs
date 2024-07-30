using LunarBase.Anotations;

namespace LunarBase.Classes
{
    [Serializable]
    [Anotacao("Config")]
    public class AtendimentoConfig : ObjetoPadrao
    {
        private int id;
        private int mesaInicial;
        private int mesaFinal;
        private string ipServidor;
        private string portaApi;

        [Anotacao("ID")]
        public virtual int Id { get => id; set => id = value; }
        [Anotacao("Mesa Inicial")]
        public virtual int MesaInicial { get => mesaInicial; set => mesaInicial = value; }
        [Anotacao("Mesa Final")]
        public virtual int MesaFinal { get => mesaFinal; set => mesaFinal = value; }
        [Anotacao("IP Servidor")]
        public virtual string IpServidor { get => ipServidor; set => ipServidor = value; }
        [Anotacao("Porta API")]
        public virtual string PortaApi { get => portaApi; set => portaApi = value; }
    }
}
