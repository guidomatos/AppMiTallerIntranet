using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA;
using System.Linq;

namespace AppMiTaller.Intranet.BL
{
    public class TallerBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public static string[] ListarContenidoInformativoTaller(int nid_taller)
        {
            string[] sResultado = new string[6];
            TallerContenidoBE Entidad = TallerContenidoDA.ListarContenidoInformativoTaller(nid_taller);
            if (!string.IsNullOrEmpty(Entidad.no_taller))
            {
                sResultado[0] = Entidad.no_taller.ToString().Trim();
                //sResultado[1] = Entidad.tx_promociones.ToString().Trim().Replace("images/","");
                //sResultado[2] = Entidad.tx_noticias.ToString().Trim().Replace("images/", "");
                //sResultado[3] = Entidad.tx_datos.ToString().Trim().Replace("images/", "");
                //sResultado[4] = Entidad.tx_fotos.ToString().Trim().Replace("images/", "");
            }
            else
            {
                sResultado[0] = "0";
            }

            return sResultado;
        }

        public static string ActualizarEstadoContenido(TallerContenidoBE oMaestroTallerContenidoBE)
        {
            return TallerContenidoDA.ActualizarEstadoContenido(oMaestroTallerContenidoBE);
        }
        public static string[] ListarContenidoTaller(int nid_taller_contenido, string sRutaServer)
        {
            string[] sResultado = new string[15];

            TallerContenidoBE Entidad = TallerContenidoDA.ListarContenidoTaller(nid_taller_contenido);
            if (!Entidad.nid_taller_contenido.Equals(0))
            {

                sResultado[0] = Entidad.nid_taller_contenido.ToString();
                sResultado[1] = Entidad.co_taller_contenido.ToString();
                sResultado[2] = Entidad.nid_taller.ToString();
                sResultado[3] = Entidad.nid_negocio_taller.ToString();
                sResultado[4] = Entidad.co_tipo.ToString();
                sResultado[5] = Entidad.no_titulo.ToString();
                sResultado[6] = Entidad.fe_inicio.ToString();
                sResultado[7] = Entidad.fe_fin.ToString();
                sResultado[8] = Entidad.tx_contenido.ToString();
                sResultado[9] = Entidad.tx_observacion.ToString();
                sResultado[10] = Entidad.co_estado.ToString();

                string sGrilla = string.Empty;
                int index = 0;
                foreach (string sDato in Entidad.co_fotos.Split('$'))
                {
                    if (string.IsNullOrEmpty(sDato)) continue;

                    index += 1;

                    string[] sDatoFoto = sDato.Split('|');

                    sGrilla += "<tr style=\"height:20px;cursor:pointer;" + "" + " \"  id='hdnDocId_" + index.ToString() + "' ";
                    sGrilla += " onclick=\"javascript: fc_SeleccionaFilaSimple(this);\"  ";
                    sGrilla += " class=\"textogrilla\" ";
                    sGrilla += " >";

                    sGrilla += "<td style=\"width: 5%;\" scope=\"col\" align=\"center\" >" + index.ToString() + "</td>";
                    sGrilla += "<td style=\"width:67%;\" scope=\"col\" align=\"left\" >";

                    if (Entidad.co_tipo.Trim().Equals("003"))
                        sGrilla += "<input type=\"text\"  class=\"ctxt\"  style=\"width:98%;\" maxlength=\"255\" value=\"" + sDatoFoto[3].ToString().Trim() + "\" />";
                    else
                        sGrilla += sDatoFoto[2].ToString().Trim();

                    sGrilla += "</td>";
                    sGrilla += "<td style=\"width:35%;\" scope=\"col\" align=\"center\" >" +
                        /*
                        "<span style='float:left; margin-left:10px; width:40px;' ><a href='#' class='dellink'  onclick='fc_EliminarFoto(\"" + "hdnDocId_" + index.ToString()+","+ sDatoFoto[0].ToString().Trim() +","+ Entidad.co_taller_contenido.Trim() + "," + sDatoFoto[2].ToString().Trim() + "\")'>Eliminar</a></span>" + // for deleting file
                        "<span style='float:left; margin-left:10px; width:40px;' ><a class='dellink'  target='_blank' href='FileUpload.ashx?filepath=" + sRutaServer + @"\" + Entidad.co_taller_contenido.Trim() + "&file=" + sDatoFoto[2].ToString().Trim() + "' >Ver</a></span>" + // for downloading file
                        */
                               "<span style='float:left; margin-left:10px; width:40px;' ><a href='#' class='dellink'  onclick='fc_EliminarFoto(\"" + "hdnDocId_" + index.ToString() + "," + sDatoFoto[0].ToString().Trim() + "," + Entidad.co_taller_contenido.Trim() + "," + sDatoFoto[2].ToString().Trim() + "\")'>Eliminar</a></span>" + // for deleting file
                               "<span style='float:left; margin-left:10px; width:40px;' ><a class='dellink'  target='_blank' href='FileUpload.ashx?filepath=" + Entidad.co_taller_contenido.Trim() + "&file=" + sDatoFoto[2].ToString().Trim() + "' >Ver</a></span>" + // for downloading file
                               "</td>";
                    sGrilla += "<td style=\"width:1px;display:none;\" scope=\"col\" align=\"left\" >" + sDatoFoto[2].ToString().Trim() + "</td>";
                    sGrilla += "<td style=\"width:1px;display:none;\" scope=\"col\" align=\"left\" >" + sDatoFoto[0].ToString().Trim() + "</td>";
                    sGrilla += "</tr>";
                }

                sResultado[11] = sGrilla;
                sResultado[12] = Entidad.no_estado.ToString();
            }
            else
            {
                sResultado[0] = "0";
            }

            return sResultado;
        }

        public static string InsertarContenidoTaller(TallerContenidoBE oMaestroTallerContenidoBE)
        {
            return TallerContenidoDA.InsertarContenidoTaller(oMaestroTallerContenidoBE);
        }

        public static string ListarCodigoContenido()
        {
            return TallerContenidoDA.ListarCodigoContenido();
        }
        public static string ListarBandejaContenido(string opcion, System.Collections.Hashtable listado, int registros, int pagina)
        {
            System.Collections.Generic.List<TallerContenidoBE> oLista = TallerContenidoDA.ListarBandejaContenido(listado);
            string sGrilla = string.Empty;
            int cant = oLista.Count;

            if (oLista != null && oLista.Count > 0)
            {

                int numPaginas = 0;

                if (registros > 0)
                {
                    numPaginas = oLista.Count / registros;

                    if (oLista.Count > registros && oLista.Count % registros != 0)
                        numPaginas = numPaginas + 1;

                    oLista = oLista.Skip((pagina - 1) * registros).Take(registros).ToList();
                    //List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                    //var list2 = list.Skip(2).Take(5);
                }
                int index = 0;
                foreach (TallerContenidoBE oEntidad in oLista)
                {
                    string sRow = (oLista.IndexOf(oEntidad) % 2).Equals(0) ? "" : "background-color: #E3E7F2";
                    index += 1;

                    sGrilla += "<tr style=\"height:20px;cursor:pointer;" + sRow + " \"  ";
                    sGrilla += " class=\"textogrilla\" ";
                    sGrilla += " onclick=\"javascript: fc_SeleccionaFilaSimple(this);fc_SelContenido(" + oEntidad.nid_taller_contenido.ToString() + ");\"  ";
                    sGrilla += " ondblclick=\"javascript: fc_EditarContenido('" + oEntidad.nid_taller_contenido.ToString() + "','" + oEntidad.co_estado + "');\"  ";
                    sGrilla += " >";

                    sGrilla += "<td style=\"width: 3%;\" scope=\"col\" align=\"center\" >" + "<input type=\"checkbox\" onclick=\"javascript:Fc_SelCheckContenido('" + oEntidad.nid_taller_contenido.ToString() + "','" + index.ToString() + "','" + oEntidad.co_estado.ToString() + "');\" id=\"chkSel" + index.ToString() + "\">" + "</td>";
                    sGrilla += "<td style=\"width:8%;\" scope=\"col\" align=\"center\" >" + oEntidad.no_tipo.ToString() + "</td>";
                    sGrilla += "<td style=\"width:8%;\" scope=\"col\" align=\"center\" >" + oEntidad.no_negocio_taller.Trim().ToString() + "</td>";
                    sGrilla += "<td style=\"width:22%;\" scope=\"col\" align=\"left\" >" + oEntidad.no_titulo.Trim().ToString() + "</td>";
                    //sGrilla += "<td style=\"width:15%;\" scope=\"col\" align=\"center\" >" + oEntidad.tx_contenido.Trim().ToString() + "</td>";
                    sGrilla += "<td style=\"width:15%;\" scope=\"col\" align=\"center\" >" + (string.IsNullOrEmpty(oEntidad.fe_inicio.ToString() + oEntidad.fe_fin.ToString()) ? "" : oEntidad.fe_inicio.ToString() + " - " + oEntidad.fe_fin.ToString()) + "</td>";
                    sGrilla += "<td style=\"width:10%;\" scope=\"col\" align=\"center\" >" + oEntidad.fe_actualizacion.ToString() + "</td>";
                    sGrilla += "<td style=\"width:25%;\" scope=\"col\" align=\"center\" >" + oEntidad.tx_observacion.ToString() + "</td>";
                    sGrilla += "<td style=\"width:8%;\" scope=\"col\" align=\"center\" >" + oEntidad.no_estado.ToString() + "</td>";
                    sGrilla += "</tr>";
                }
                sGrilla += "<tr class='Footer'><td colspan='9'>&nbsp;</td></tr>";


                if (registros > 0)
                {
                    sGrilla += "<tr align='right' style='color:#555A6D;border-style:None;' class='Paginacion'>";
                    sGrilla += "<td colspan='8'><span>Total Reg. " + cant.ToString() + "</span><table cellspacing='0' cellpadding='0' border='0' style='border-collapse:collapse;'>";
                    sGrilla += "<tbody><tr>";

                    int inicio = pagina - 7;
                    int fin = numPaginas;

                    if (numPaginas > pagina + 7)
                        fin = pagina + 7;

                    sGrilla += "<tbody><tr><td>";

                    if (inicio <= 0)
                        inicio = 1;
                    else
                        sGrilla += "...&nbsp;";

                    for (int i = inicio; i <= fin; i++)
                    {
                        if (i == pagina)
                            sGrilla += i + "&nbsp;";
                        else
                            sGrilla += "<a style=color:#555A6D;' href='javascript:fc_Buscar(" + i + ");'>" + i + "</a>&nbsp;";
                    }

                    if (fin != numPaginas)
                        sGrilla += "...";

                    sGrilla += "</td></tr>";
                    sGrilla += "</tbody></table></td>";
                    sGrilla += "</tr>";
                }
            }

            return sGrilla;

        }

        public static string ListarTipoContenido(int opcion_sel, string texto_sel)
        {
            string sCombo = "";
            TallerContenidoBEList oLista = TallerContenidoDA.ListarTipoContenido();
            if (opcion_sel == 1)
                sCombo = "<option value='0'>" + texto_sel + "</option>";
            if (oLista != null)
                foreach (TallerContenidoBE oEntidad in oLista)
                    sCombo += "<option value='" + oEntidad.co_tipo.ToString() + "'>" + oEntidad.no_tipo.Trim() + "</option>";
            return sCombo;
        }
        public static string ListarEstadosContenido(int opcion_sel, string texto_sel)
        {
            string sCombo = "";
            TallerContenidoBEList oLista = TallerContenidoDA.ListarEstadoContenido();
            if (opcion_sel == 1)
                sCombo = "<option value='0'>" + texto_sel + "</option>";
            if (oLista != null)
                foreach (TallerContenidoBE oEntidad in oLista)
                    sCombo += "<option value='" + oEntidad.co_estado.ToString() + "'>" + oEntidad.no_estado.Trim() + "</option>";
            return sCombo;
        }
        public static string ListarNegocioTaller(int nid_taller, int Nid_usuario, int opcion_sel, string texto_sel)
        {
            string sCombo = "";
            TallerContenidoBEList oLista = TallerContenidoDA.ListarNeogiciosTaller(nid_taller, Nid_usuario);

            if (oLista.Count == 1) opcion_sel = 2;
            if (opcion_sel == 1)
                sCombo = "<option value='0'>" + texto_sel + "</option>";
            if (oLista != null)
                foreach (TallerContenidoBE oEntidad in oLista)
                    sCombo += "<option value='" + oEntidad.nid_negocio_taller.ToString() + "'>" + oEntidad.no_negocio_taller.Trim() + "</option>";
            return sCombo;
        }

        //@002-F

        public String ExisteCodigo(TallerBE ent)
        {
            return new TallerDA().ExisteCodigo(ent);
        }

        public TallerBEList GETListarTalleres(TallerBE ent)
        {
            try
            {
                return new TallerDA().GETListarTalleres(ent);
            }
            catch (Exception ex)
            {
                this.ErrorEvent(this, ex);
                return null;
            }
        }

        public TallerBEList GETListarUbigeo(int Nid_usuario)
        {
            return new TallerDA().GETListarUbigeo(Nid_usuario);
        }

        public CombosBEList GETListarDepartamento(int Nid_usuario)
        {
            return new TallerDA().GETListarDepartamento(Nid_usuario);
        }


        public TallerBEList GETListarUbicacion(TallerBE ent)
        {
            return new TallerDA().GETListarUbicacion(ent);
        }

        public CombosBEList GETListarProvincia(TallerBE ent, int Nid_usuario)
        {
            return new TallerDA().GETListarProvincia(ent, Nid_usuario);
        }

        public CombosBEList GETListarDistrito(TallerBE ent, int Nid_usuario)
        {
            return new TallerDA().GETListarDistrito(ent, Nid_usuario);
        }

        public CombosBEList GETListarPuntoRed(TallerBE ent, int Nid_usuario)
        {
            return new TallerDA().GETListarPuntoRed(ent, Nid_usuario);
        }

        //HORARIOS

        public TallerBEList GETListarDiasDisp()
        {
            return new TallerDA().GETListarDiasDisp();
        }

        public TallerBEList GETListarIntervalosAtencion()
        {
            return new TallerDA().GETListarIntervalosAtencion();
        }

        public TallerBEList GETListarFeriados()
        {
            return new TallerDA().GETListarFeriados();
        }

        /*@001 I*/
        public CombosBEList GETListarIndicadorHGSI()
        {
            return new TallerDA().GETListarIndicadorHGSI();
        }
        /*@001 F*/

        //SERVICIOS

        public TallerBEList GETListarServicios()
        {
            return new TallerDA().GETListarServicios();
        }

        //MARCAS Y MODElOS

        public TallerBEList GETListarMarcasModelos(TallerBE ent)
        {
            return new TallerDA().GETListarMarcaModelo(ent);
        }
        public TallerBEList GETListarCapacidadTallerModelo(TallerBE ent)
        {
            return new TallerDA().GETListarCapacidadTallerModelo(ent);
        }

        //PARA UPDATE

        public TallerBEList GETDetalleTaller(TallerBE ent)
        {
            return new TallerDA().GETDetalleTaller(ent);
        }

        public TallerBEList GETDetallePorPuntoRed(TallerBE ent)
        {
            return new TallerDA().GETDetallePorPuntoRed(ent);
        }

        public TallerBEList GETDiasExcepPorTaller(TallerBE ent)
        {
            return new TallerDA().GETDiasExcepPorTaller(ent);
        }

        public TallerBEList GETServiciosPorTaller(TallerBE ent)
        {
            return new TallerDA().GETServiciosPorTaller(ent);
        }

        public TallerBEList GETModelosPorTaller(TallerBE ent)
        {
            return new TallerDA().GETModelosPorTaller(ent);
        }

        public TallerBEList GETDiasHabiles_Hora_PorTaller(TallerBE ent)
        {
            return new TallerDA().GETDiasHabiles_Hora_PorTaller(ent);
        }

        //--------------

        public Int32 InsertTaller(TallerBE ent)
        {
            try
            {
                return new TallerDA().GETInsertarTaller(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 InsertTallerHorario(TallerBE ent)
        {
            try
            {
                return new TallerDA().GETInsertarTallerHorario(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 InsertTallerDiaExceptuado(TallerBE ent)
        {
            try
            {
                return new TallerDA().GETInsertarTallerDiaExceptuado(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 InsertTallerServicio(TallerBE ent)
        {
            try
            {
                return new TallerDA().GETInsertarTallerServicio(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 InsertTallerModelo(TallerBE ent)
        {
            try
            {
                return new TallerDA().GETInsertarTallerModelo(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 InsertarTallerModeloCapacidad(TallerBE ent)
        {
            try
            {
                return new TallerDA().GETInsertarTallerModeloCapacidad(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 ActualizarTaller(TallerBE ent)
        {
            try
            {
                return new TallerDA().ActualizarTaller(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 ActualizarHorario(TallerBE ent)
        {
            try
            {
                return new TallerDA().ActualizarHorario(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 ActualizarDiasExcep(TallerBE ent)
        {
            try
            {
                return new TallerDA().ActualizarDiasExcep(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 ActualizarTallerServicios(TallerBE ent)
        {
            try
            {
                return new TallerDA().ActualizarTallerServicios(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 ActualizarTallerModelos(TallerBE ent)
        {
            try
            {
                return new TallerDA().ActualizarTallerModelos(ent);
            }
            catch
            {
                return 0;
            }
        }


        public Int32 MantenimientoTallerServicios(TallerBE ent)
        {
            try
            {
                return new TallerDA().MantenimientoTallerServicios(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 MantenimientoTallerModelos(TallerBE ent)
        {
            try
            {
                return new TallerDA().MantenimientoTallerModelos(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 MantenimientoTallerModelosCapacidad(TallerBE ent)
        {
            try
            {
                return new TallerDA().MantenimientoTallerModelosCapacidad(ent);
            }
            catch
            {
                return 0;
            }
        }


        public Int32 MantenimientoTallerDiasExceptuados(TallerBE ent)
        {
            try
            {
                return new TallerDA().MantenimientoTallerDiasExceptuados(ent);
            }
            catch
            {
                return 0;
            }
        }

        public TallerBEList ListarDias_Taller(TallerBE ent)
        {
            return new TallerDA().ListarDias_Taller(ent);
        }
        public TallerBEList ListarHorario_Taller(TallerBE ent)
        {
            return new TallerDA().ListarHorario_Taller(ent);
        }

        #region "AGREGADO POR VICTOR GALARZA"

        public TallerHorariosExcepcionalBEList GetListHorarioExcepcional(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().GetListHorarioExcepcional(ent);
        }

        public TallerHorariosExcepcionalBEList GetListDiasXTllrHorarioExcepcional(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().GetListDiasXTllrHorarioExcepcional(ent);
        }

        public TallerHorariosExcepcionalBEList GetListHorXDiaXTllrHorarioExcepcional(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().GetListHorXDiaXTllrHorarioExcepcional(ent);
        }

        public Int32 InsertarCabeHorExcepcional(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().InsertarCabeHorExcepcional(ent);
        }

        public Int32 ActualizarCabeHorExcepcional(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().ActualizarCabeHorExcepcional(ent);
        }

        public Int32 InsertarDetaHorExcepcional(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().InsertarDetaHorExcepcional(ent);
        }

        public Int32 EliminarDetaHorExcepcional(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().EliminarDetaHorExcepcional(ent);
        }

        public TallerHorariosExcepcionalBEList GetListHorarioExcepcionalXHorario(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().GetListHorarioExcepcionalXHorario(ent);
        }

        public TallerHorariosExcepcionalBEList GetListDetaHorarioExcepcionalXHorario(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().GetListDetaHorarioExcepcionalXHorario(ent);
        }
        #endregion


        // V 2.0

        public Int32 InhabilitarCapacidadAtencion_Taller(TallerHorariosBE ent)
        {
            return new TallerHorariosDA().InhabilitarCapacidadAtencion_Taller(ent);
        }

        public Int32 MantenimientoCapacidadAtencion_Taller(TallerHorariosBE ent)
        {
            return new TallerHorariosDA().MantenimientoCapacidadAtencion_Taller(ent);
        }

        public TallerHorariosBEList GETListarCapacidadAtencion_PorTaller(TallerHorariosBE ent)
        {
            return new TallerHorariosDA().GETListarCapacidadAtencion_PorTaller(ent);

        }
    }
}