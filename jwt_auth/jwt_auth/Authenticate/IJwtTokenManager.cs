namespace jwt_auth.Authenticate
{
    public interface IJwtTokenManager
    {
        public string Authenticate(string username);
    }
}
