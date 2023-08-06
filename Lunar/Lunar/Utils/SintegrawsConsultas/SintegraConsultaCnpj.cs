using System;

namespace Lunar.Utils.SintegrawsConsultas
{
    public class SintegraConsultaCnpj
    {
        public String status { get; set; }
        public String code { get; set; }
        public String message { get; set; }
        public String nome_empresarial { get; set; }
        public String cnpj { get; set; }
        public String inscricao_estadual { get; set; }
        public String tipo_inscricao { get; set; }
        public String data_situacao_cadastral { get; set; }
        public String situacao_cnpj { get; set; }
        public String situacao_ie { get; set; }
        public String nome_fantasia { get; set; }
        public String data_inicio_atividade { get; set; }
        public String regime_tributacao { get; set; }
        public String informacao_ie_como_destinatario { get; set; }
        public String porte_empresa { get; set; }
        public cnae_principal cnae_principal { get; set; }
        public String data_fim_atividade { get; set; }
        public String uf { get; set; }
        public String municipio { get; set; }
        public String logradouro { get; set; }
        public String complemento { get; set; }
        public String cep { get; set; }
        public String numero { get; set; }
        public String bairro { get; set; }
        public Ibge ibge { get; set; }
    }
    public class cnae_principal
    {
        public string code { get; set; }
        public string text { get; set; }
    }

    public class Ibge
    {
        public string codigo_municipio { get; set; }
        public string codigo_uf { get; set; }
    }
}
