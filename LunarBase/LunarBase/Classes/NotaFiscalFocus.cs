using System.Text.Json.Serialization;

namespace LunarBase.Classes
{
    public class NotaFiscalFocus
    {

        [JsonPropertyName("natureza_operacao")]
        public string NaturezaOperacao { get; set; }

        [JsonPropertyName("data_emissao")]
        public string DataEmissao { get; set; }

        [JsonPropertyName("data_entrada_saida")]
        public string DataEntradaSaida { get; set; }

        [JsonPropertyName("tipo_documento")]
        public int TipoDocumento { get; set; }

        [JsonPropertyName("finalidade_emissao")]
        public int FinalidadeEmissao { get; set; }

        [JsonPropertyName("cnpj_emitente")]
        public string CnpjEmitente { get; set; }

        [JsonPropertyName("cpf_emitente")]
        public string CpfEmitente { get; set; }

        [JsonPropertyName("nome_emitente")]
        public string NomeEmitente { get; set; }

        [JsonPropertyName("nome_fantasia_emitente")]
        public string NomeFantasiaEmitente { get; set; }

        [JsonPropertyName("logradouro_emitente")]
        public string LogradouroEmitente { get; set; }

        [JsonPropertyName("numero_emitente")]
        public int NumeroEmitente { get; set; }

        [JsonPropertyName("bairro_emitente")]
        public string BairroEmitente { get; set; }

        [JsonPropertyName("municipio_emitente")]
        public string MunicipioEmitente { get; set; }

        [JsonPropertyName("uf_emitente")]
        public string UfEmitente { get; set; }

        [JsonPropertyName("cep_emitente")]
        public string CepEmitente { get; set; }

        [JsonPropertyName("inscricao_estadual_emitente")]
        public string InscricaoEstadualEmitente { get; set; }

        [JsonPropertyName("nome_destinatario")]
        public string NomeDestinatario { get; set; }

        [JsonPropertyName("cpf_destinatario")]
        public string CpfDestinatario { get; set; }

        [JsonPropertyName("inscricao_estadual_destinatario")]
        public string InscricaoEstadualDestinatario { get; set; }

        [JsonPropertyName("telefone_destinatario")]
        public long TelefoneDestinatario { get; set; }

        [JsonPropertyName("logradouro_destinatario")]
        public string LogradouroDestinatario { get; set; }

        [JsonPropertyName("numero_destinatario")]
        public int NumeroDestinatario { get; set; }

        [JsonPropertyName("bairro_destinatario")]
        public string BairroDestinatario { get; set; }

        [JsonPropertyName("municipio_destinatario")]
        public string MunicipioDestinatario { get; set; }

        [JsonPropertyName("uf_destinatario")]
        public string UfDestinatario { get; set; }

        [JsonPropertyName("pais_destinatario")]
        public string PaisDestinatario { get; set; }

        [JsonPropertyName("cep_destinatario")]
        public long CepDestinatario { get; set; }

        [JsonPropertyName("valor_frete")]
        public double ValorFrete { get; set; }

        [JsonPropertyName("valor_seguro")]
        public double ValorSeguro { get; set; }

        [JsonPropertyName("valor_total")]
        public double ValorTotal { get; set; }

        [JsonPropertyName("valor_produtos")]
        public double ValorProdutos { get; set; }

        [JsonPropertyName("modalidade_frete")]
        public int ModalidadeFrete { get; set; }

        [JsonPropertyName("items")]
        public List<ItemNotaFiscal> Items { get; set; } = new List<ItemNotaFiscal>();

        public void AdicionarItem(ItemNotaFiscal item)
        {
            Items.Add(item);
        }

        public bool Validar()
        {
            if (string.IsNullOrEmpty(NaturezaOperacao) || string.IsNullOrEmpty(CnpjEmitente))
            {
                return false;
            }
            return true;
        }
    }

    public class ItemNotaFiscal
    {
        [JsonPropertyName("numero_item")]
        public int NumeroItem { get; set; }

        [JsonPropertyName("codigo_produto")]
        public int CodigoProduto { get; set; }

        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        [JsonPropertyName("cfop")]
        public int Cfop { get; set; }

        [JsonPropertyName("unidade_comercial")]
        public string UnidadeComercial { get; set; }

        [JsonPropertyName("quantidade_comercial")]
        public int QuantidadeComercial { get; set; }

        [JsonPropertyName("valor_unitario_comercial")]
        public double ValorUnitarioComercial { get; set; }

        [JsonPropertyName("valor_unitario_tributavel")]
        public double ValorUnitarioTributavel { get; set; }

        [JsonPropertyName("unidade_tributavel")]
        public string UnidadeTributavel { get; set; }

        [JsonPropertyName("codigo_ncm")]
        public int CodigoNcm { get; set; }

        [JsonPropertyName("quantidade_tributavel")]
        public int QuantidadeTributavel { get; set; }

        [JsonPropertyName("valor_bruto")]
        public double ValorBruto { get; set; }

        [JsonPropertyName("icms_situacao_tributaria")]
        public int IcmsSituacaoTributaria { get; set; }

        [JsonPropertyName("icms_origem")]
        public int IcmsOrigem { get; set; }

        [JsonPropertyName("pis_situacao_tributaria")]
        public string PisSituacaoTributaria { get; set; }

        [JsonPropertyName("cofins_situacao_tributaria")]
        public string CofinsSituacaoTributaria { get; set; }

        // Método para validar um item
        public bool ValidarItem()
        {
            if (NumeroItem <= 0 || CodigoProduto <= 0 || QuantidadeComercial <= 0 || ValorUnitarioComercial <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
