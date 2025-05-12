using CalculoSimples.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculoSimples.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalcController : Controller
    {
        [HttpGet("soma-progressiva/{indice}")]
        public IActionResult SomaProgressiva(int indice = 13)
        {
            int SOMA = 0, K = 0;

            while (K < indice)
            {
                K++;
                SOMA += K;
            }
            return Ok(SOMA);
        }

        [HttpGet("fibonacci/{numero}")]
        public IActionResult VerificaFibonacci(int numero)
        {
            int a = 0, b = 1;
            if (numero == 0 || numero == 1) return Ok(true);

            while (b < numero)
            {
                int temp = b;
                b = a + b;
                a = temp;
            }
            return Ok(b == numero);
        }

        [HttpPost("faturamento-diario")]
        public IActionResult AnalisaFaturamentoDiario([FromBody] List<FaturamentoDiario> dados)
        {
            var validos = dados.Where(d => d.Valor > 0).ToList();
            double media = validos.Average(d => d.Valor);
            return Ok(new
            {
                Menor = validos.Min(d => d.Valor),
                Maior = validos.Max(d => d.Valor),
                DiasAcimaDaMedia = validos.Count(d => d.Valor > media)
            });
        }

        [HttpPost("faturamento-estados")]
        public IActionResult PercentualPorEstado([FromBody] List<FaturamentoEstado> dados)
        {
            double total = dados.Sum(d => d.Valor);
            var resultado = dados.Select(d => new {
                d.Estado,
                Percentual = Math.Round((d.Valor / total) * 100, 2)
            });
            return Ok(resultado);
        }

        [HttpGet("inverter-string/{texto}")]
        public IActionResult InverterString(string texto)
        {
            string invertida = "";
            for (int i = texto.Length - 1; i >= 0; i--)
            {
                invertida += texto[i];
            }
            return Ok(invertida);
        }
    }
}
