namespace CarteiraDeCambio.DTO
{
    public class SaldoDTO
    {
        public string valor { get; set; }
        
        public string siglaMoeda { get; set; }

        public string NomeMoeda { get; set; }

        public string GetDescricaoDoSaldo()
        {
            return string.Format("Saldo de {0} {1}", valor, NomeMoeda);
        }

    }
}
