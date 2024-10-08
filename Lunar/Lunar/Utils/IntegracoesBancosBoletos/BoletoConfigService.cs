using LunarBase.Classes;
using LunarBase.ControllerBO;
using System;

namespace Lunar.Utils.IntegracoesBancosBoletos
{
    public class BoletoConfigService
    {
        public BoletoConfig GetBoletoConfigContaBancaria(ContaBancaria contaBancaria)
        {
            BoletoConfigController boletoConfigController = new BoletoConfigController();
            BoletoConfig boletoConfig = null;
            try
            {
                boletoConfig = boletoConfigController.selecionarBoletoConfigPorContaBancariaUnica(contaBancaria);
                if (boletoConfig != null)
                {
                    return boletoConfig;
                }
                else
                {
                    throw new Exception($"Nenhuma configuração de boleto foi encontrada para a conta bancária com ID: {contaBancaria.Id}");
                }
            }
            catch (Exception ex)
            {
                GenericaDesktop.ShowAlerta($"Erro ao buscar a configuração de boleto: {ex.Message}");
                return null;
            }
        }

    }
}
