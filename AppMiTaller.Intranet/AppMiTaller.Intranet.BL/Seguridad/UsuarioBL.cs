using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA.Seguridad;

namespace AppMiTaller.Intranet.BL
{
    public class UsuarioBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public UsuarioBEList GetAllUsuarioBandeja(UsuarioBE oUsuario, String aplicacionID)
        {
            try
            {
                return new UsuarioDA().GetAllUsuarioBandeja(oUsuario, aplicacionID);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }
        public Int32 EliminarUsuarioMasivo(UsuarioBE oUsuario, String cadena, Int32 nuRegistros)
        {
            try
            {
                return new UsuarioDA().EliminarUsuarioMasivo(oUsuario, cadena, nuRegistros);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return 0;
        }

        public Int32 ActivarUsuarioMasivo(UsuarioBE oUsuario, String cadena, Int32 nuRegistros)
        {
            try
            {
                return new UsuarioDA().ActivarUsuarioMasivo(oUsuario, cadena, nuRegistros);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return 0;
        }

        public Int32 GrabarUsuario(UsuarioBE oUsuario, String aplicacionID)
        {
            UsuarioDA oUsuarioDA = new UsuarioDA();
            try
            {
                if (oUsuario.NID_USUARIO == 0)
                {
                    return oUsuarioDA.InsertarUsuario(oUsuario, aplicacionID);
                }
                else
                {
                    return oUsuarioDA.ModificarUsuario(oUsuario, aplicacionID);
                }
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return 0;
        }


        public UsuarioBE GetUsuarioById(Int32 usuarioID)
        {
            try
            {
                return new UsuarioDA().GetUsuarioById(usuarioID);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }

        public Int32 ModificarPassWord(String usuario, String passWord, String codUsuario, String nomUsuarioRed, String estacioRed, String indActReset, String passDesencriptado)
        {
            String passMD5, passEnc;
            SeguridadBL oSeguridadBL = new SeguridadBL();
            UsuarioBE oUsuario = new UsuarioBE();
            try
            {
                if (passWord.Trim().Equals(String.Empty))
                {
                    passEnc = oSeguridadBL.GetEncripta(usuario);

                    passDesencriptado = passEnc;
                    passMD5 = oUsuario.LoginGetMD5(passEnc);
                    passEnc = oSeguridadBL.GetEncripta(passEnc);
                }
                else
                {
                    passEnc = oSeguridadBL.GetEncripta(passWord);
                    passMD5 = oUsuario.GetMD5(passWord);
                }

                return new UsuarioDA().ModificarPassWord(usuario, passEnc, passMD5, codUsuario, nomUsuarioRed, estacioRed, indActReset, passDesencriptado);

            }
            catch (Exception ex)
            {
                this.ErrorEvent(this, ex);
            }
            return 0;
        }
    }
}