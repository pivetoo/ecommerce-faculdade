using System.Text.RegularExpressions;

namespace Ecommerce.Dominio.Util
{
    public static partial class CpfTools
    {
        public static bool Validar(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = RemoverCaracteresEspeciais(cpf);

            if (!ValidarNumerico(cpf))
                return false;

            cpf = ZeroEsquerda(cpf, 11);

            if (NumerosIguais(cpf))
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        private static string RemoverCaracteresEspeciais(string numero)
        {
            return Regex.Replace(numero, "[^0-9a-zA-Z]+", "");
        }

        private static string ZeroEsquerda(string numero, int qtdValorCompleto)
        {
            numero = numero.PadLeft(qtdValorCompleto, '0');

            return numero;
        }

        private static bool ValidarNumerico(string numero)
        {
            if (numero.All(char.IsDigit))
                return true;

            return false;
        }

        private static bool NumerosIguais(string numero)
        {
            char comparar = numero.ToString()[0];
            foreach (var n in numero.ToString())
            {
                if (comparar != n)
                    return false;
            }
            return true;
        }
    }
}
