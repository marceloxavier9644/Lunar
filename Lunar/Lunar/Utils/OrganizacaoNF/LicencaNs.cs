using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lunar.Utils.OrganizacaoNF.RetornoJsonVariados;

namespace Lunar.Utils.OrganizacaoNF
{
    public class LicencaNs
    {
        public int situacao { get; set; }
        public bool envia_email_sendgrid { get; set; } = false;
        public int liberado { get; set; }
        public bool usarcertns { get; set; } = false;
        public bool manifesta_auto { get; set; } = false;
        public bool buscacte { get; set; } = false;
        public bool buscanfse { get; set; } = false;
        public bool receber90dias { get; set; } = false;
        public int idprojeto { get; set; }
        public CertificadoNs certificado { get; set; }
        public PessoaNs pessoa { get; set; }
        public Csc csc { get; set; }
        public LogotipoNs logotipo { get; set; }
    }
    public class CertificadoNs
    {
        public string certificado { get; set; } // Certificado em Base64
        public string senha { get; set; }       // Senha do certificado
    }
    public class LogotipoNs
    {
        public string arquivo { get; set; } // Logo em Base64
        
    }

    public class Csc
    {
        public string csc { get; set; } 
        public string codcsc { get; set; }
        public int tpamb { get; set; }
    }
    public class PessoaNs
    {
        public string cnpj { get; set; }
        public string ie { get; set; }
        public string razao { get; set; }
        public string fantasia { get; set; }
        public int tpicms { get; set; }
        public string site { get; set; }
        public string datanasc { get; set; }
        public string skype { get; set; }
        public EmailNs[] emails { get; set; }
        public EnderecoNs[] enderecos { get; set; }
        public TelefoneNs[] telefones { get; set; }
    }
    public class EmailNs
    {
        public string email { get; set; }
    }

    public class EnderecoNs
    {
        public string endereco { get; set; }
        public int numero { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public CidadeNs cidadeNs { get; set; }
    }

    public class CidadeNs
    {
        public int cIBGE { get; set; }
        public string nome { get; set; }
    }

    public class TelefoneNs
    {
        public string numero { get; set; }
    }
}
