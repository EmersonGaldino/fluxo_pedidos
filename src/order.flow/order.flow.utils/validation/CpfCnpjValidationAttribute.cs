using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace order.flow.utils.validation;

public class CpfCnpjValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            return new ValidationResult("O CPF/CNPJ não pode ser vazio.");

        var document = value.ToString().Trim().Replace(".", "").Replace("-", "").Replace("/", "");

        if (document.Length == 11)
        {
            if (!IsValidCpf(document))
                return new ValidationResult("CPF inválido.");
        }
        else if (document.Length == 14)
        {
            if (!IsValidCnpj(document))
                return new ValidationResult("CNPJ inválido.");
        }
        else
        {
            return new ValidationResult("O documento deve ter 11 dígitos (CPF) ou 14 dígitos (CNPJ).");
        }

        return ValidationResult.Success;
    }

    private bool IsValidCpf(string cpf)
    {
        if (!Regex.IsMatch(cpf, @"^\d{11}$") || new string(cpf[0], 11) == cpf)
            return false;

        int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int sum = 0;

        for (int i = 0; i < 9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * mult1[i];

        int remainder = sum % 11;
        string digit = (remainder < 2 ? 0 : 11 - remainder).ToString();

        tempCpf += digit;
        sum = 0;

        for (int i = 0; i < 10; i++)
            sum += int.Parse(tempCpf[i].ToString()) * mult2[i];

        remainder = sum % 11;
        digit += (remainder < 2 ? 0 : 11 - remainder).ToString();

        return cpf.EndsWith(digit);
    }

    private bool IsValidCnpj(string cnpj)
    {
        if (!Regex.IsMatch(cnpj, @"^\d{14}$") || new string(cnpj[0], 14) == cnpj)
            return false;

        int[] mult1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] mult2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj.Substring(0, 12);
        int sum = 0;

        for (int i = 0; i < 12; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * mult1[i];

        int remainder = sum % 11;
        string digit = (remainder < 2 ? 0 : 11 - remainder).ToString();

        tempCnpj += digit;
        sum = 0;

        for (int i = 0; i < 13; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * mult2[i];

        remainder = sum % 11;
        digit += (remainder < 2 ? 0 : 11 - remainder).ToString();

        return cnpj.EndsWith(digit);
    }
}