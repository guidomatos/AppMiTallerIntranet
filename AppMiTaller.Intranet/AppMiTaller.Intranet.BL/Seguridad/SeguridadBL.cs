using System;
using AppMiTaller.Intranet.DA.Seguridad;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.BL
{
    
    public class SeguridadBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public UsuarioBE ValidaLogeoUsuario(String usuario, String contrasenha, String aplicaionID, out String indLogeo, out String msgLogeo)
        {
            SeguridadDA oSeguridad = new SeguridadDA();
            UsuarioBE oUsuario = null;
            indLogeo = "0";
            msgLogeo = String.Empty;
            try
            {

                oUsuario = oSeguridad.GetDatosUsuario(usuario, aplicaionID);

                if (oUsuario != null)
                {

                    //1.- Validamos que la contrasea sea diferente a la cuenta de usuario.
                    if (!oUsuario.VPASSMD5.Equals(oUsuario.LoginGetMD5(contrasenha)))
                    {
                        
                        //msgLogeo = "La contraseña ingresada no es correcta.";
                        msgLogeo = "El usuario y / o la contraseña ingresada no son correctos, favor de validar la información ingresada y volver a intentar";
                        
                        indLogeo = "-5";
                        return oUsuario;
                    }

                    //2.- Validamos si el aplicativo se encuentra activo.
                    if (oUsuario.CESTBLQ.Equals("1"))
                    {
                        if (oUsuario.VMSGBLQ == null || oUsuario.VMSGBLQ.Equals(String.Empty))
                        {
                            msgLogeo = "El usuario se encuentra bloqueado.";
                        }
                        else
                        {
                            msgLogeo = oUsuario.VMSGBLQ;
                        }
                        indLogeo = "-2";
                        return oUsuario;
                    }

                    //3.- Validamos que el usuario no se encuentre bloqueado
                    if (oUsuario.NID_TIPO != ConstanteBE.NID_TIPO_ADMINISTRADOR)
                    {
                        if (oUsuario.CESTBLQ.Equals("1"))
                        {
                            if (oUsuario.VMSGBLQ == null || oUsuario.VMSGBLQ.Equals(String.Empty))
                                // @005 I
                                //msgLogeo = "Usuario se encuentra bloqueado.";
                                msgLogeo = "El usuario se encuentra bloqueado.";
                                // @005 F
                            else msgLogeo = oUsuario.VMSGBLQ;

                            indLogeo = "-3";
                            return oUsuario;
                        }
                        else
                        {
                            if (oUsuario.VMSGBLQ != null && !oUsuario.VMSGBLQ.Equals(String.Empty))
                            {
                                msgLogeo = oUsuario.VMSGBLQ;
                            }
                        }
                    }

                    //4.- Validamos que la contrasea sea diferente a la cuenta de usuario. 
                    if (oUsuario.LoginGetMD5(usuario.ToUpper().Trim()).Equals(oUsuario.VPASSMD5))
                    {
                        // @005 I
                        //msgLogeo = "Debe cambiar su contraseña como medida de seguridad.";
                        msgLogeo = "Su contraseña no puede ser igual a la cuenta de usuario. Debe cambiar su contraseña como medida de seguridad.";
                        // @005 F
                        indLogeo = "-4";
                        return oUsuario;
                    }
                                        

                    //6.- Se valida que el usuario.
                    if (oUsuario.fl_reset.Trim().Equals("1"))
                    {
                        // @005 I
                        //msgLogeo = "El usuario debe cambiar su contraseña.";
                        msgLogeo = "Por favor, cambie su contraseña e ingrese nuevamente.";
                        // @005 F                        
                        indLogeo = "-6";
                        return oUsuario;
                    }
                    //I @002
                    //7.- Caducidad de tiempo de contraseña
                    if (oUsuario.int_dias_caducidad <= 0)
                    {
                        // @005 I
                        //msgLogeo = "Su contraseña ha caducado y debe cambiarse.";
                        msgLogeo = "Su contraseña ha caducado. Por favor, cambiela e ingrese nuevamente.";
                        // @005 F                        
                        indLogeo = "-7";
                        return oUsuario;
                    }
                    //F @002
                }
                else
                {
                    indLogeo = "-9";
                    //@006 I 
                    //msgLogeo = "El usuario ingresado no existe.";
                    msgLogeo = "El usuario y / o la contraseña ingresada no son correctos, favor de validar la información ingresada y volver a intentar.";
                    //@006 F 
                }
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return oUsuario;
        }
        public bool UpdateLogeoBloqueo(String usuario, String indBloqueo, String msgBloqueo)
        {
            return new SeguridadDA().UpdateLogeoBloqueo(usuario, indBloqueo, msgBloqueo);
        }

        
        public Int32 InsertUsuarioOpcion(Int32 IdUsuario, String xml, String coUsuario, String noEstacioRed, String noUsuario)
        {
            try
            {
                return new SeguridadDA().InsertUsuarioOpcion(IdUsuario, xml, coUsuario, noEstacioRed, noUsuario);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return 0;
        }
        #region "Encripta - Desencripta"

        public String GetEncripta(String strinput)
        {
            int li_idx;
            String ls_result = String.Empty;

            for (li_idx = 0; li_idx < strinput.Length; li_idx++)
            {
                ls_result = String.Format("{0}{1}", ls_result, EncriptaChar(strinput.Substring(li_idx, 1), strinput.Length, li_idx + 1));
            }
            //uf_encripta_char(Mid(as_cadena,li_idx, 1), Len(as_cadena), li_idx) Next  

            return ls_result;
        }

        private String EncriptaChar(String as_caracter, int ai_variable, int ai_indice)
        {
            String ls_caracterEncriptado = String.Empty;
            //String ls_caracterEncriptado1 = String.Empty;
            int li_indice;

            if (ConstanteBE.PATRON_BUSQUEDA.IndexOf(as_caracter) > -1)
            {
                li_indice = ConstanteBE.PATRON_BUSQUEDA.IndexOf(as_caracter) + ai_variable + ai_indice;
                li_indice = li_indice % ConstanteBE.PATRON_BUSQUEDA.Length;
                ls_caracterEncriptado = ConstanteBE.PATRON_ENCRIPTA.Substring(li_indice, 1);
                //ls_caracterEncriptado1 = ConstanteBE.PATRON_ENCRIPTA.Substring(li_indice - 1, 1);
            }

            return ls_caracterEncriptado;
        }

        #endregion
    }
}
