namespace GesDoc.Models
{
    public class Equipamento : TipoEquipamento
    {
        public int CodEquipamento { get; set; }
        public int CodCliente { get; set; }
        public int CodSetor { get; set; }
        public int CodSala { get; set; }
        public string NomeSala { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string NumeroSerie { get; set; }
        public string NumeroPatrimonio { get; set; }
        public string RegistroAnvisa { get; set; }
        public string AnoFabricacao { get; set; }
        public string StatusEquip { get; set; }
        public string DescricaoEquipamento { get; set; }
    }
}