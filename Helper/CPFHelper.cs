using System.Text;

namespace DummyDataGenerator.Helper;

public class CPFHelper
{
    private static Random random = new Random();

    public static string GenerateValidCPF()
    {
        string cpf;
        do
        {
            cpf = GenerateCPF();
        }
        while (!IsCPFValid(cpf)); // Verifique se o CPF gerado é válido

        return cpf;
    }
    
    public static string GenerateCPF()
    {
        // Gera nove dígitos aleatórios para o CPF
        int[] cpfArray = new int[9];
        for (int i = 0; i < cpfArray.Length; i++)
        {
            cpfArray[i] = random.Next(10);
        }

        // Calcula os dígitos verificadores
        int[] checkerDigits = CalculateCheckDigits(cpfArray);

        // Cria o CPF formatado
        StringBuilder cpf = new StringBuilder();
        foreach (int digit in cpfArray)
        {
            cpf.Append(digit);
        }
        cpf.Append(checkerDigits[0]);
        cpf.Append(checkerDigits[1]);


        return cpf.ToString();
    }
    
    public static bool IsCPFValid(string cpf)
    {
        // Remove qualquer formatação (pontos e hífen) do CPF
        cpf = cpf.Replace(".", "").Replace("-", "");

        // Verifica se o CPF possui 11 dígitos
        if (cpf.Length != 11)
        {
            return false;
        }

        // Verifica se todos os dígitos do CPF são iguais; se sim, não é válido
        if (cpf.Distinct().Count() == 1)
        {
            return false;
        }

        // Calcula o primeiro dígito verificador
        int soma = 0;
        for (int i = 0; i < 9; i++)
        {
            soma += int.Parse(cpf[i].ToString()) * (10 - i);
        }
        int primeiroDigito = 11 - (soma % 11);

        // Se o primeiro dígito verificador for maior que 9, ele deve ser 0
        primeiroDigito = primeiroDigito > 9 ? 0 : primeiroDigito;

        // Calcula o segundo dígito verificador
        soma = 0;
        for (int i = 0; i < 10; i++)
        {
            soma += int.Parse(cpf[i].ToString()) * (11 - i);
        }
        int segundoDigito = 11 - (soma % 11);

        // Se o segundo dígito verificador for maior que 9, ele deve ser 0
        segundoDigito = segundoDigito > 9 ? 0 : segundoDigito;

        // Verifica se os dígitos verificadores calculados coincidem com os dígitos reais
        if (int.Parse(cpf[9].ToString()) != primeiroDigito || int.Parse(cpf[10].ToString()) != segundoDigito)
        {
            return false;
        }

        return true;
    }

    private static int[] CalculateCheckDigits(int[] cpfArray)
    {
        int[] digitosVerificadores = new int[2];

        for (int i = 0; i < 2; i++)
        {
            int soma = 0;
            int peso = i + 2;

            for (int j = 0; j < cpfArray.Length + 1; j++)
            {
                soma += (j < cpfArray.Length) ? cpfArray[j] * peso : digitosVerificadores[i] * peso;
                peso--;
            }

            int resto = soma % 11;

            if (resto < 2)
            {
                digitosVerificadores[i] = 0;
            }
            else
            {
                digitosVerificadores[i] = 11 - resto;
            }
        }

        return digitosVerificadores;
    }

    public static string ConvertToFormattedCPF(string cpf)
    {
        return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
    }

    public static void Main(string[] args)
    {
        string cpf = GenerateCPF();
        Console.WriteLine($"CPF Gerado: {cpf}");
    }
}