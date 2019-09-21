namespace AppMiTaller.Intranet.BE
{
    public enum direccionOrden
    {
        Ascending,
        Descending
    }
    public enum eTipoMensaje : int
    {
        OK = 1,
        Warning = 2,
        Error = 3,
        Question = 4,
        InfoReport = 5
    }
    public enum TipoTabla
    {
        MARCA = 2,
        MODELO = 3
    }
    public enum TipoAdjunto
    {
        todos = 0,
        imagenMEI,
        imagenSpecCode,
        informatico,
        soloDOC,
        soloDOCyPDF,
        soloDOCyPDFAndImagenes,
        soloDOCX
    }
}
