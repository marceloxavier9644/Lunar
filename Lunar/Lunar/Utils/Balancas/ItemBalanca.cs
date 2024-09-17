namespace Lunar.Utils.Balancas
{
    /// <summary>
    /// Classe que representa os itens para exportação em formato específico Prix.
    /// </summary>
    public class ItemBalanca
    {
        /// <summary>
        /// Código do departamento (DD).
        /// Deve ter 2 bytes.
        /// </summary>
        public string CodigoDepartamento { get; set; }

        /// <summary>
        /// Código do som (CS).
        /// Deve ter 4 bytes.
        /// </summary>
        public string CodigoSom { get; set; }

        /// <summary>
        /// Tipo de produto (T).
        /// - "0" = Venda por peso
        /// - "1" = Venda por unidade
        /// - "2" = EAN-13 por peso
        /// - "3" = Venda por peso glaciado
        /// - "4" = Venda por peso drenado
        /// - "5" = EAN-13 por unidade
        /// Deve ter 1 byte.
        /// </summary>
        public string TipoProduto { get; set; }

        /// <summary>
        /// Código de tara pré-determinada (CT).
        /// Deve ter 4 bytes.
        /// "0000" = Não haverá associação.
        /// </summary>
        public string CodigoTara { get; set; }

        /// <summary>
        /// Código do fracionador (FR).
        /// Deve ter 4 bytes.
        /// "0000" = Não haverá associação.
        /// </summary>
        public string CodigoFracionador { get; set; }

        /// <summary>
        /// Código do campo extra 1 (CE1).
        /// Deve ter 4 bytes.
        /// "0000" = Não haverá associação.
        /// </summary>
        public string CodigoCampoExtra1 { get; set; }

        /// <summary>
        /// Código do campo extra 2 (CE2).
        /// Deve ter 4 bytes.
        /// "0000" = Não haverá associação.
        /// </summary>
        public string CodigoCampoExtra2 { get; set; }
        /// <summary>
        /// Código do campo extra 3 (CE3).
        /// Deve ter 4 bytes.
        /// "0000" = Não haverá associação.
        /// </summary>
        public string CodigoCampoExtra3 { get; set; }
        /// <summary>
        /// Código do campo extra 4 (CE5).
        /// Deve ter 4 bytes.
        /// "0000" = Não haverá associação.
        /// </summary>
        public string CodigoCampoExtra4 { get; set; }

        /// <summary>
        /// Código do item (CCCCCC).
        /// Deve ter 6 bytes.
        /// </summary>
        public string CodigoItem { get; set; }

        /// <summary>
        /// Preço por kg ou unidade (PPPPPP).
        /// Deve ter 6 bytes.
        /// </summary>
        public string Preco { get; set; }

        /// <summary>
        /// Dias de validade do produto (VVV).
        /// Deve ter 3 bytes.
        /// </summary>
        public string DiasValidade { get; set; }

        /// <summary>
        /// Código da conservação (CON).
        /// Deve ter 4 bytes.
        /// "0000" = Não haverá associação.
        /// </summary>
        public string CodigoConservacao { get; set; }

        /// <summary>
        /// EAN-13 de fornecedor (EAN).
        /// Deve ter 12 bytes.
        /// </summary>
        public string EANFornecedor { get; set; }

        /// <summary>
        /// Percentual de glaciamento (GL).
        /// Deve ter 6 bytes.
        /// "000000" = Não haverá associação.
        /// </summary>
        public string PercentualGlaciamento { get; set; }

        /// <summary>
        /// Sequência de departamentos associados (DA).
        /// Exemplo: Para associar departamentos 2 e 5: |0205|
        /// </summary>
        public string DepartamentosAssociados { get; set; }

        /// <summary>
        /// Descritivo do item – Primeira linha (D1).
        /// Deve ter 25 bytes.
        /// </summary>
        public string Descritivo1 { get; set; }

        /// <summary>
        /// Descritivo do item – Segunda linha (D2).
        /// Deve ter 25 bytes.
        /// </summary>
        public string Descritivo2 { get; set; }

        /// <summary>
        /// Descritivo do item – Terceira linha (D3).
        /// Deve ter 35 bytes.
        /// </summary>
        public string Descritivo3 { get; set; }

        /// <summary>
        /// Descritivo do item – Quarta linha (D4).
        /// Deve ter 35 bytes.
        /// </summary>
        public string Descritivo4 { get; set; }

        /// <summary>
        /// Código da informação extra do item (RRRRRR).
        /// Deve ter 6 bytes.
        /// "000000" = Não haverá associação.
        /// </summary>
        public string CodigoInfoExtra { get; set; }

        /// <summary>
        /// Código da imagem do item (FFFF).
        /// Deve ter 4 bytes.
        /// "0000" = Não haverá associação.
        /// </summary>
        public string CodigoImagem { get; set; }

        /// <summary>
        /// Código da informação nutricional (IIIIII).
        /// Deve ter 6 bytes.
        /// "000000" = Não haverá associação.
        /// </summary>
        public string CodigoInfoNutricional { get; set; }

        /// <summary>
        /// Código da mídia (MIDIA).
        /// Deve ter 6 bytes.
        /// "000000" = Não haverá associação.
        /// </summary>
        public string CodigoMidia { get; set; }

        /// <summary>
        /// Impressão da data de validade (DV).
        /// Deve ter 1 byte.
        /// - "1" => Imprime data de validade
        /// - "0" => Não imprime data de validade
        /// </summary>
        public string DataValidade { get; set; }

        /// <summary>
        /// Preço promocional - Preço/kg ou preço/unidade do item (PPPPPP).
        /// Deve ter 6 bytes.
        /// </summary>
        public string PrecoPromocional { get; set; }

        /// <summary>
        /// Utilização do fornecedor associado (SF).
        /// Deve ter 1 byte.
        /// - "0" => Utiliza o fornecedor associado
        /// - "1" => Balança solicita fornecedor após chamada do PLU
        /// </summary>
        public string SolicitacaoFornecedor { get; set; }

        /// <summary>
        /// Impressão da data de embalagem (DE).
        /// Deve ter 1 byte.
        /// - "1" => Imprime data de embalagem
        /// - "0" => Não imprime data de embalagem
        /// </summary>
        public string DataEmbalagem { get; set; }

        /// <summary>
        /// Código de fornecedor associado (|FFFF|).
        /// Utilizado no cadastro de fornecedores do MGV.
        /// Deve ter 4 bytes por fornecedor.
        /// Exemplo: Para associar fornecedores 2 e 5: |00020005|
        /// </summary>
        public string CodigoFornecedorAssociado { get; set; }

        /// <summary>
        /// Código do fornecedor (CF).
        /// Deve ter 4 bytes.
        /// "0000" = Não haverá associação.
        /// </summary>
        public string CodigoFornecedor { get; set; }

        /// <summary>
        /// Solicitação de tara na balança (ST).
        /// Deve ter 1 byte.
        /// - "0" => Não solicita tara na balança
        /// - "1" => Solicita tara na balança
        /// </summary>
        public string SolicitacaoTara { get; set; }

        /// <summary>
        /// Lote (L).
        /// Deve ter 12 bytes.
        /// </summary>
        public string Lote { get; set; }

        /// <summary>
        /// Sequência de balanças onde o item não estará ativo (BNA).
        /// Deve ter 2 bytes por balança.
        /// Exemplo: Para associar balanças 2 e 5 com itens inativos: |0205|
        /// </summary>
        public string BalancasInativas { get; set; }

        /// <summary>
        /// Código EAN-13 especial (G).
        /// Deve ter 11 bytes.
        /// </summary>
        public string CodigoEANEspecial { get; set; }

        /// <summary>
        /// Versão do preço (Z).
        /// Deve ter 1 byte.
        /// </summary>
        public string VersaoPreco { get; set; }

        /// <summary>
        /// Código EAN-13 especial (G1).
        /// Caso seja informado este campo, a opção anterior de envio (G) será ignorada.
        /// Deve ter 12 bytes.
        /// </summary>
        public string CodigoEANEspecialG1 { get; set; }

        /// <summary>
        /// Percentual de glaciamento (PG).
        /// Informação utilizada apenas para integração com o MGV Cloud.
        /// Exemplo: 12,33% deve ser representado como "1233".
        /// Deve ter 4 bytes.
        /// </summary>
        public string PercentualGlaciamentoPG { get; set; }
    }

}