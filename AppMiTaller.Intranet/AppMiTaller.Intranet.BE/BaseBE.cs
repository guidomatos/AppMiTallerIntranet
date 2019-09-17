using System;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class BaseBE
    {
        public char Estado { get; set; }
        public int UsuarioID { get; set; }
    }
}