using DummyDataGenerator.Helper;
using Microsoft.AspNetCore.Mvc;

namespace DummyDataGenerator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CPFController : ControllerBase
{
    [HttpGet("cpf-generator")]
    public IActionResult CpfGenerator(bool ConvertToFormattedCPF)
    {
        if (ConvertToFormattedCPF)
        {
            var cpf = CPFHelper.GenerateValidCPF();
            
            // Formata o CPF com pontos e hífen (opcional)
            string formattedCpf = CPFHelper.ConvertToFormattedCPF(cpf);
            
            return Ok(formattedCpf);
        }
        else
        {
            var cpf = CPFHelper.GenerateValidCPF();
            return Ok(cpf);
        }
    }
    
    [HttpGet("cpf-valid")]
    public IActionResult CPFValid(string request)
    {
        var cpf = CPFHelper.IsCPFValid(request);
        return Ok(cpf);
    }
}