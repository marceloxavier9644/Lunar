using LunarBase.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Utils.OrganizacaoNF
{
    public class ValidadorNotaSaida
    {
        public bool validarClienteNota(Pessoa pessoa)
        {
            if (string.IsNullOrWhiteSpace(pessoa.RazaoSocial))
            {
                throw new Exception("O campo \"Nome/Razão Social\" é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(pessoa.Cnpj) || pessoa.Cnpj.Length < 11 || pessoa.Cnpj.Length > 14)
            {
                throw new Exception("O campo \"CPF ou CNPJ\" é obrigatório!");
            }
            if (pessoa.EnderecoPrincipal == null)
            {
                throw new Exception("O \"Endereço do Cliente/Fornecedor\" deve ser completo com Logradouro, Numero, Bairro, Cidade!");
            }
            if(pessoa.EnderecoPrincipal != null)
            {
                if (string.IsNullOrWhiteSpace(pessoa.EnderecoPrincipal.Logradouro))
                {
                    throw new Exception("O campo \"Logradouro (Rua)\" é obrigatório!");
                }
                if (string.IsNullOrWhiteSpace(pessoa.EnderecoPrincipal.Numero))
                {
                    throw new Exception("O campo \"Número do endereço\" é obrigatório!");
                }
                if (string.IsNullOrWhiteSpace(pessoa.EnderecoPrincipal.Bairro))
                {
                    throw new Exception("O campo \"Bairro\" é obrigatório!");
                }
            }
            return true;
        }
        public bool validarProdutosNota(IList<NfeProduto> listaProdutos)
        {
            if (listaProdutos.Count > 0)
            {
                foreach (NfeProduto nfeProduto in listaProdutos)
                {
                    if (string.IsNullOrWhiteSpace(nfeProduto.Produto.Descricao))
                    {
                        throw new Exception("O campo \"Descrição do Produto\" é obrigatório! Código do Produto: " + nfeProduto.Produto.Id.ToString() + " - " + nfeProduto.Produto.Descricao);
                    }
                    if (string.IsNullOrWhiteSpace(nfeProduto.Ncm))
                    {
                        throw new Exception("O campo \"NCM do Produto\" é obrigatório! Código do Produto: " + nfeProduto.Produto.Id.ToString() + " - " + nfeProduto.Produto.Descricao);
                    }
                    if (string.IsNullOrWhiteSpace(nfeProduto.Cfop))
                    {
                        throw new Exception("O campo \"CFOP do Produto\" é obrigatório! Código do Produto: " + nfeProduto.Produto.Id.ToString() + " - " + nfeProduto.Produto.Descricao);
                    }
                    if (string.IsNullOrWhiteSpace(nfeProduto.CstIcms))
                    {
                        throw new Exception("O campo \"CSOSN do Produto\" é obrigatório! Código do Produto: " + nfeProduto.Produto.Id.ToString() + " - " + nfeProduto.Produto.Descricao);
                    }
                    if (nfeProduto.Produto.UnidadeMedida == null)
                    {
                        throw new Exception("O campo \"Unidade de Medida do Produto\" é obrigatório! Código do Produto: " + nfeProduto.Produto.Id.ToString() + " - " + nfeProduto.Produto.Descricao);
                    }
                    if (nfeProduto.Cfop.Equals("5405") || nfeProduto.Cfop.Equals("6403"))
                    {
                        if (string.IsNullOrWhiteSpace(nfeProduto.Produto.Cest))
                        {
                            throw new Exception("O campo \"CEST\" é obrigatório para produtos ST! Código do Produto: " + nfeProduto.Produto.Id.ToString() + " - " + nfeProduto.Produto.Descricao);
                        }
                    }
                    if (nfeProduto.ValorFinal <= 0)
                    {
                        throw new Exception("O campo \"Valor do Produto\" é obrigatório ser maior que R$ 0,00! Código do Produto: " + nfeProduto.Produto.Id.ToString() + " - " + nfeProduto.Produto.Descricao);
                    }
                }
                return true;
            }
            else
            {
                throw new Exception("Produtos não inseridos!");
            }
        }
    }
}
