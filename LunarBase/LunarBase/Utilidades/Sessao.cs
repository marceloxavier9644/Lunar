using LunarBase.Classes;
using NHibernate.Stat;

namespace LunarBase.Utilidades
{
    public class Sessao
    {
        public static string _conexaoMySQL;

        public static EmpresaFilial empresaFilialLogada;
        public static Usuario usuarioLogado;

        public static decimal vendasRecebimento_ValorRecebido;
        public static Cidade cidadeSelecionada;
        public static PessoaReferenciaPessoal pessoaReferenciaPessoal;
        public static PessoaReferenciaComercial pessoaReferenciaComercial;
        public static PessoaPropriedade pessoaPropriedade;
        public static PessoaTelefone pessoaTelefone;
        public static Endereco endereco;
        public static ProdutoGrupo grupoSelecionadoCadastroProduto;
        public static ParametroSistema parametroSistema;
        public static string tokenJuno = "A8A929D84C5E33F9E07993AE711C26AC0B60C0D2A03C6E1F086BC99472D4479B";
        public static string tokenJunoParceiros = "FDF7E05A8C91A2DEB5D09F112CEF61589D4A78D9096234141F3B6EF3E7C20612D5D52AF4B7C9CEBA";
        public static string syncfusion = "NzA1NDk1QDMxMzkyZTM0MmUzMEpUTFdHNW03NHVveVltU0wzdzhsMDRzOTgwdGx0M3pPZmlMc3QydE9hb2M9";
        public static PlanoConta planoContaRecebimentoContaReceber;
        public static PlanoConta planoContaRecebimentoContaPagar;
        public static PlanoConta planoContaVenda;

        public static bool teveRetornoApi = false;
        public static string serialPainel;
        public static string cnpjRegistro;

        public static decimal valorSinalOrdemServico = 0;
    }
}
