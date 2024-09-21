using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarBase.Utilidades.NFSe
{
    public class NFSeRequest
    {
        public string data_emissao { get; set; }
        public string natureza_operacao { get; set; }
       // public string regime_especial_tributacao { get; set; }
        public Boolean optante_simples_nacional { get; set; }

        public Prestador prestador { get; set; }
        public Tomador tomador { get; set; }
        public ServicoNFS servico { get; set; }
    }

    public class Prestador
    {
        public string cnpj { get; set; }
        public string inscricao_municipal { get; set; }
        public string codigo_municipio { get; set; }
    }

    public class Tomador
    {
        public string cpf { get; set; }
        public string cnpj { get; set; }
        public string razao_social { get; set; }
        public string email { get; set; }
        public EnderecoNFs endereco { get; set; }
    }

    public class EnderecoNFs
    {
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string codigo_municipio { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
    }

    public class ServicoNFS
    {
        public decimal valor_servicos { get; set; } // valor_servicos(*)
        public decimal valor_deducoes { get; set; } // valor_deducoes
        public decimal valor_pis { get; set; } // valor_pis
        public decimal valor_cofins { get; set; } // valor_cofins
        public decimal valor_inss { get; set; } // valor_inss
        public decimal valor_ir { get; set; } // valor_ir
        public decimal valor_csll { get; set; } // valor_csll
        public bool iss_retido { get; set; } // iss_retido(*)
        public decimal valor_iss { get; set; } // valor_iss
        public decimal valor_iss_retido { get; set; } // valor_iss_retido
        public decimal outras_retencoes { get; set; } // outras_retencoes
        public decimal base_calculo { get; set; } // base_calculo
        public decimal aliquota { get; set; } // aliquota
        public decimal desconto_incondicionado { get; set; } // desconto_incondicionado
        public decimal desconto_condicionado { get; set; } // desconto_condicionado
        public string item_lista_servico { get; set; } // item_lista_servico(*)
        public string codigo_cnae { get; set; } // codigo_cnae
        //public string codigo_tributario_municipio { get; set; } // codigo_tributario_municipio
        public string discriminacao { get; set; } // discriminacao(*)
        public string codigo_municipio { get; set; } // codigo_municipio(*)
        public decimal percentual_total_tributos { get; set; } // percentual_total_tributos
        public string fonte_total_tributos { get; set; } // fonte_total_tributos
    }

    public class NFSeResponse
    {
        public string cnpj_prestador { get; set; }
        [JsonProperty("ref")]
        public string referencia { get; set; }
        public string numero_rps { get; set; }
        public string serie_rps { get; set; }
        public string status { get; set; }
        public string numero { get; set; }
        public string codigo_verificacao { get; set; }
        public DateTime data_emissao { get; set; }
        public string url { get; set; }
        public string caminho_xml_nota_fiscal { get; set; }
    }



}
