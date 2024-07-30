
namespace LunarBase.Classes
{
    public interface ICartaoMovimento
    {
        AdquirenteCartao AdquirenteCartao { get; set; }
        BandeiraCartao BandeiraCartao { get; set; }
        DateTime DataCredito { get; set; }
        DateTime DataLancamento { get; set; }
        string Descricao { get; set; }
        EmpresaFilial EmpresaFilial { get; set; }
        FormaPagamento FormaPagamento { get; set; }
        int Id { get; set; }
        Parcelamento Parcelamento { get; set; }
        double Taxa { get; set; }
    }
}