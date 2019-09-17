using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA.Seguridad;

namespace AppMiTaller.Intranet.BL
{
    public class PerfilBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        #region "Perfil"

        //DAC - 22/12/2010 - Inicio
        public PerfilBEList GetPerfilesBandeja(String aplicacionID, String dscPerfil, String Estado, String strFlConcesionario)
        {
            try
            {
                return new PerfilDA().GetPerfilesBandeja(aplicacionID, dscPerfil, Estado, strFlConcesionario);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }
        public PerfilBEList GetPerfilesBandejaConcesionario(String aplicacionID, String strCodUsuario)
        {
            try
            {
                return new PerfilDA().GetPerfilesBandejaConcesionario(aplicacionID, strCodUsuario);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }
        //DAC - 22/12/2010 - Fin

        public PerfilBE GetPerfilById(Int32 perfilID)
        {
            try
            {
                return new PerfilDA().GetPerfilById(perfilID);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }

        public Int32 GrabarPerfil(PerfilBE oPerfilBE)
        {
            PerfilDA oPerfilDA = new PerfilDA();
            try
            {
                if (oPerfilBE.NID_PERFIL == 0) return oPerfilDA.InsertarPerfil(oPerfilBE);
                else return oPerfilDA.ModificarPerfil(oPerfilBE);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return 0;
        }

        public Int32 EliminarPerfil(PerfilBE oPerfilBE)
        {
            try
            {
                return new PerfilDA().EliminarPerfil(oPerfilBE);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return 0;
        }

        #endregion

        #region "Usuarios Relacionados"

        public UsuarioBEList GetBandejaUsuariosRelacionados(Int32 perfilID, String aplicacionID)
        {
            try
            {
                return new PerfilDA().GetBandejaUsuariosRelacionados(perfilID, aplicacionID);
            }
            catch (Exception ex)
            {
                this.ErrorEvent(this, ex);
            }

            return null;
        }

        public UsuarioBEList GetAsignacionUsuariosRelacionados(UsuarioBE oUsuario, String aplicacionID)
        {
            try
            {
                return new PerfilDA().GetAsignacionUsuariosRelacionados(oUsuario, aplicacionID);
            }
            catch (Exception ex)
            {
                this.ErrorEvent(this, ex);
            }

            return null;
        }

        public Int32 EliminarUsuariosRelacionados(UsuarioBE oUsuarioBE)
        {
            Int32 retorno = 0;
            try
            {
                return new PerfilDA().EliminarUsuariosRelacionados(oUsuarioBE);
            }
            catch (Exception ex)
            {
                this.ErrorEvent(this, ex);
            }

            return retorno;
        }

        public Int32 InsertarUsuariosRelacionado(UsuarioBE oUsuarioBE, String aplicacionID
                                                , String usuariosSel)
        {
            PerfilDA oPerfilDA = new PerfilDA();
            Int32 retorno, nidUsuario, retornoDA;
            String[] arrUsuario;
            String flagErrores = "0";

            retorno = -1;

            try
            {
                arrUsuario = usuariosSel.Split('|');
                for (int i = 0; i < arrUsuario.Length; i++)
                {
                    if (Int32.TryParse(arrUsuario[i], out nidUsuario))
                    {
                        oUsuarioBE.NID_USUARIO = nidUsuario;
                        retornoDA = oPerfilDA.InsertarUsuariosRelacionado(oUsuarioBE, aplicacionID);
                        if (retornoDA == -2)
                        {
                            flagErrores = "2";
                        }
                        else if (retornoDA < 0 && flagErrores.Equals("0"))
                        {
                            flagErrores = "1";
                        }
                    }
                }
                if (flagErrores.Equals("0")) retorno = 1;
                else if (flagErrores.Equals("2")) retorno = -2;
            }
            catch (Exception ex)
            {
                this.ErrorEvent(this, ex);
            }

            return retorno;
        }

        #endregion

        #region "Opcion"

        public OpcionSeguridadBEList GetAllOpciones(OpcionSeguridadBE oOpcionSeguridadBE, Int32 perfilID, Int32 idUsuario)
        {
            try
            {
                return new PerfilDA().GetAllOpciones(oOpcionSeguridadBE, perfilID, idUsuario);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }
        //I @001
        public Int32 InsertarOpcionByPerfil(PerfilBE oPerfil, String XML)
        {
            //DAC - 01/04/2011 - Inicio
            Int32 xrpta = 0;
            //Int32 opcionID;
            //String[] arrIndRelacion = listaIndRelacion.Split('|');
            //bool flagPrimerIngreso = true;
            //DAC - 01/04/2011 - Fin
            try
            {
                OpcionSeguridadBE oOpcion = new OpcionSeguridadBE();
                oOpcion.CCOAPL = oPerfil.CCOAPL;
                oOpcion.CO_USUARIO = oPerfil.CO_USUARIO_CREA;
                oOpcion.NO_ESTACION_RED = oPerfil.NO_ESTACION_RED;
                oOpcion.NO_USUARIO = oPerfil.NO_USUARIO_RED;
                oOpcion.nid_perfil = oPerfil.NID_PERFIL;
                oOpcion.cad_lista_opciones = XML;

                PerfilDA oPerfilDA = new PerfilDA();
                xrpta = oPerfilDA.InsertarOpcionByPerfil(oOpcion);

                //for (int i = 0; i < arrOpciones.Length; i++)
                //{
                //    if (Int32.TryParse(arrOpciones[i], out opcionID))
                //    {
                //        oOpcion.NID_OPCION = opcionID;
                //        oOpcion.IND_REL = arrIndRelacion[i];

                //        if (flagPrimerIngreso)
                //        {
                //            oPerfilDA.InsertarOpcionByPerfil(oOpcion, oPerfil.NID_PERFIL, "1");
                //            flagPrimerIngreso = false;
                //        }
                //        else oPerfilDA.InsertarOpcionByPerfil(oOpcion, oPerfil.NID_PERFIL, "0");
                //    }
                //}
                //oPerfilDA.InsertarOpcionByPerfil(oOpcion, oPerfil.NID_PERFIL, "2");
                //return 1;
                //DAC - 01/04/2011 - Fin
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            //DAC - 04/04/2011 - Inicio
            //return 0;
            return xrpta;
            //DAC - 04/04/2011 - Fin
        }
        //F @001
        //DAC - 30/03/2011 - Inicio
        public OpcionSeguridadBEList GetAllOpcionesConcesionario(OpcionSeguridadBE oOpcionSeguridadBE)
        {
            try
            {
                return new PerfilDA().GetAllOpcionesConcesionario(oOpcionSeguridadBE);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }
        //DAC - 30/03/2011 - Fin

        #endregion
    }
}
