namespace Ecommerce.Dominio.Util
{
    public static class PasswordPolicy
    {
        public static bool ValidarSenha(string senha)
        {
            if (senha.Length < 8 ||
                !senha.Any(char.IsUpper) ||
                !senha.Any(char.IsLower) ||
                !senha.Any(char.IsDigit) ||
                !senha.Any(c => "!@#$%^&*()".Contains(c)))
            {
                return false;
            }

            return true;
        }
    }
}
