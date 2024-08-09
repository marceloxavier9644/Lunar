using Lunar.Utils;
using Lunar.Utils.OrganizacaoNF;
using LunarBase.Classes;
using LunarBase.ControllerBO;
using LunarBase.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static LunarBase.Utilidades.ManifestoDownload;

namespace Lunar.Telas.ArquivosContabilidade
{
    public class NotaService
    {
        private readonly GenericaDesktop _genericaDesktop;
        private readonly LunarApiNotas _lunarApiNotas;
        private readonly NfeController _nfeController;

        public NotaService(GenericaDesktop genericaDesktop, LunarApiNotas lunarApiNotas, NfeController nfeController)
        {
            _genericaDesktop = genericaDesktop;
            _lunarApiNotas = lunarApiNotas;
            _nfeController = nfeController;
        }

        public async Task ProcessarNotasAsync(DateTime dataInicial, DateTime dataFinal)
        {
            IList<Nfe> listaNotas = _nfeController.selecionarNotasEmitidasPorPeriodo(
                dataInicial.ToString("yyyy-MM-dd 00:00:00"),
                dataFinal.ToString("yyyy-MM-dd 23:59:59")
            );

            if (listaNotas.Count > 0)
            {
                foreach (Nfe nfe in listaNotas)
                {
                    if (nfe.Modelo.Equals("65") && nfe.Status.Contains("Autorizado o uso"))
                    {
                        await ProcessarNota65Async(nfe);
                    }
                    if (nfe.Modelo.Equals("55") && nfe.Status.Contains("Autorizado o uso"))
                    {
                        await ProcessarNota55Async(nfe);
                    }
                }
            }
        }

        private async Task ProcessarNota65Async(Nfe nfe)
        {
            string retorno = await _lunarApiNotas.ConsultaNotaApiAsync(nfe.CnpjEmitente, nfe.Chave);

            if (retorno.Contains("NENHUMA_NOTA_LOCALIZADA"))
            {
                NFCeDownloadProc nfceDownload = await _genericaDesktop.ConsultaNFCeEmitidaAsync(nfe.EmpresaFilial.Cnpj, nfe.Chave.Trim());

                if (nfceDownload.motivo.Contains("realizado com sucesso"))
                {
                    string caminhoArquivo = Path.Combine(
                        @"Fiscal\XML\NFCe\",
                        nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0'),
                        @"Autorizadas\",
                        nfe.Chave + "-procNFCe.xml"
                    );

                    await GravarXMLAsync(nfceDownload.nfeProc.xml, nfe.Chave, caminhoArquivo);

                    string caminhoX = Path.Combine(
                        @"Fiscal\XML\NFCe\",
                        nfe.DataCadastro.Year + "-" + nfe.DataCadastro.Month.ToString().PadLeft(2, '0'),
                        @"Autorizadas\",
                        nfe.Chave + "-procNFCe.xml"
                    );

                    byte[] arquivo = await ReadFileBytesAsync(caminhoX);
                    await _lunarApiNotas.EnvioNotaParaNuvem(
                        Sessao.empresaFilialLogada.Cnpj,
                        nfe.Chave,
                        "NFCE",
                        "AUTORIZADAS",
                        nfe.DataEmissao.Month.ToString().PadLeft(2, '0'),
                        nfe.DataEmissao.Year.ToString(),
                        arquivo,
                        nfe
                    );
                }
            }
        }

        private async Task ProcessarNota55Async(Nfe nfe)
        {
            string retorno = await _lunarApiNotas.ConsultaNotaApiAsync(nfe.CnpjEmitente, nfe.Chave);

            if (retorno.Contains("NENHUMA_NOTA_LOCALIZADA"))
            {
                NFeDownloadProc55 nfeDownload = await _genericaDesktop.ConsultaNFeEmitidaAsync(nfe.EmpresaFilial.Cnpj, nfe.Chave.Trim());

                if (nfeDownload.motivo.Contains("realizado com sucesso") || nfeDownload.motivo.Contains("realizada com sucesso"))
                {
                    string caminhoArquivo = Path.Combine(
                        @"Fiscal\XML\NFe\",
                        nfe.DataEmissao.Year + "-" + nfe.DataEmissao.Month.ToString().PadLeft(2, '0'),
                        @"Autorizadas\",
                        nfe.Chave + "-procNFe.xml"
                    );

                    await GravarXMLAsync(nfeDownload.xml, nfe.Chave, caminhoArquivo);

                    string caminhoX = Path.Combine(
                        @"Fiscal\XML\NFe\",
                        nfe.DataCadastro.Year + "-" + nfe.DataCadastro.Month.ToString().PadLeft(2, '0'),
                        @"Autorizadas\",
                        nfe.Chave + "-procNFe.xml"
                    );

                    byte[] arquivo = await ReadFileBytesAsync(caminhoX);
                    await _lunarApiNotas.EnvioNotaParaNuvem(
                        Sessao.empresaFilialLogada.Cnpj,
                        nfe.Chave,
                        "NFE",
                        "AUTORIZADAS",
                        nfe.DataEmissao.Month.ToString().PadLeft(2, '0'),
                        nfe.DataEmissao.Year.ToString(),
                        arquivo,
                        nfe
                    );
                }
            }
        }
        public async Task<byte[]> ReadFileBytesAsync(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
            {
                byte[] fileBytes = new byte[fileStream.Length];
                await fileStream.ReadAsync(fileBytes, 0, (int)fileStream.Length);
                return fileBytes;
            }
        }
        private async Task GravarXMLAsync(string xmlContent, string chave, string caminhoArquivo)
        {
            await Task.Run(() =>
            {
                Directory.CreateDirectory(Path.GetDirectoryName(caminhoArquivo));
                File.WriteAllText(caminhoArquivo, xmlContent);
            });
        }


    }
}

