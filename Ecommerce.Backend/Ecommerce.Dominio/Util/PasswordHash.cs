namespace Ecommerce.Dominio.Util
{
    public static class PasswordHash
    {
        public static string HashPassword(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public static bool VerificarSenha(string senhaFornecida, string hashArmazenado)
        {
            return BCrypt.Net.BCrypt.Verify(senhaFornecida, hashArmazenado);
        }
    }
}
