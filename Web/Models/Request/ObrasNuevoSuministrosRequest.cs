using System;

namespace RowingApp.Common.Requests
{
    public class ObrasNuevoSuministrosRequest
    {
        //public int NROSUMINISTRO { get; set; }
        public int NROOBRA { get; set; }
        public DateTime? FECHA { get; set; }
        public string APELLIDONOMBRE { get; set; }
        public string DNI { get; set; }
        public string TELEFONO { get; set; }
        public string EMAIL { get; set; }
        public string CUADRILLA { get; set; }
        public string GRUPOC { get; set; }
        public string CAUSANTEC { get; set; }
        public string DIRECTA { get; set; }
        public string DOMICILIO { get; set; }
        public string BARRIO { get; set; }
        public string LOCALIDAD { get; set; }
        public string PARTIDO { get; set; }
        public byte[] ImageArrayANTESFOTO1 { get; set; }
        public byte[] ImageArrayANTESFOTO2 { get; set; }
        public byte[] ImageArrayDESPUESFOTO1 { get; set; }
        public byte[] ImageArrayDESPUESFOTO2 { get; set; }
        public byte[] ImageArrayFOTODNIFRENTE { get; set; }
        public byte[] ImageArrayFOTODNIREVERSO { get; set; }
        public byte[] ImageArrayFIRMACLIENTE { get; set; }
        public string ENTRECALLES1 { get; set; }
        public string ENTRECALLES2 { get; set; }
        public string MEDIDORCOLOCADO { get; set; }
        public string MEDIDORVECINO { get; set; }
        public string TIPORED { get; set; }
        public string CORTE { get; set; }
        public string DENUNCIA { get; set; }
        public string ENRE { get; set; }
        public string OTRO { get; set; }
        public string CONEXIONDIRECTA { get; set; }
        public string RETIROCONEXION { get; set; }
        public string RETIROCRUCECALLE { get; set; }
        public int? MTSCABLERETIRADO { get; set; }
        public string TRABAJOCONHIDRO { get; set; }
        public string POSTEPODRIDO { get; set; }
        public string OBSERVACIONES { get; set; }
        public int? POTENCIACONTRATADA { get; set; }
        public int? TENSIONCONTRATADA { get; set; }
        public int? KITNRO { get; set; }
        public int? IDCERTIFMATERIALES { get; set; }
        public int? IDCERTIFBAREMO { get; set; }
        public string PosX { get; set; }
        public string PosY { get; set; }
        public int? IDUserCarga { get; set; }
    }
}
