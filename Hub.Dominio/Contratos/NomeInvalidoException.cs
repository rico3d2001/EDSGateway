namespace Hub.Dominio.Contratos
{
    public class NomeInvalidoException : Exception
    {
        public NomeInvalidoException() { }

        public NomeInvalidoException(string nome): base(String.Format("Nome inválido: {0}", nome)) { }
    }
}
