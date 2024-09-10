namespace TM.Domain.Entities
{
    public class RequestUser
    {
        private readonly string requestingUser;
        private bool isAdmin;

        public RequestUser(string requestingUser)
        {
            this.requestingUser = requestingUser;
        }

        public bool IsAdmin()
        {
            //TODO Integrar com serviço de autenticação externo
            isAdmin = true;
            return isAdmin;
        }
    }
}
