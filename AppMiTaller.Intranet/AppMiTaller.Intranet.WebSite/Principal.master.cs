using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.WebEvents;
using AppMiTaller.Intranet.BE;

public partial class Principal : System.Web.UI.MasterPage
{
    public bool onError { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MenuResponsive();
        }
        ValidaHorarioAcceso();
    }

    protected void btnCerrarSesion_OnClick(object sender, EventArgs e)
    {
        Session.Abandon();
        FormsAuthentication.SignOut();
        Response.Redirect(FormsAuthentication.LoginUrl, false);
        Response.End();
    }
    
    #region "Validacion de Acceso al sistema"
    private void ValidaHorarioAcceso()
    {
        try
        {
            if (!RangoFechasValido(this.Profile.Usuario.SFE_INICIO_ACCESO_PERFIL, this.Profile.Usuario.SFE_FIN_ACCESO_PERFIL))
            {
                JavaScriptHelper.Funcion(this.Page, "fc_FechasAccesoPerfilCaducado", "", String.Empty);
            }
            else if (!RangoHorasValido(this.Profile.Usuario.HR_INICIO_ACCESO_PERFIL, this.Profile.Usuario.HR_FIN_ACCESO_PERFIL))
            {
                JavaScriptHelper.Funcion(this.Page, "fc_HorarioAccesoPerfilCaducado", "", String.Empty);
            }
            else if (!RangoFechasValido(this.Profile.Usuario.SFE_INICIO_ACCESO, this.Profile.Usuario.SFE_FIN_ACCESO))
            {
                JavaScriptHelper.Funcion(this.Page, "fc_FechasAccesoUsuarioCaducado", "", String.Empty);
            }
            else if (!RangoHorasValido(this.Profile.Usuario.HR_INICIO_ACCESO, this.Profile.Usuario.HR_FIN_ACCESO))
            {
                JavaScriptHelper.Funcion(this.Page, "fc_HorarioAccesoUsuarioCaducado", "", String.Empty);
            }
        }
        catch (Exception ex)
        {
            Web_ErrorEvent(this, ex);
        }
    }

    private Boolean RangoFechasValido(String feInicio, String feFin)
    {
        Int32 intFeIni = 0, intFeFIn = 0, intFeActual = 0;

        try
        {
            String[] arrFeIni = feInicio.Split('/');
            String[] arrFeFin = feFin.Split('/');
            String[] arrFeAct = DateTime.Now.ToShortDateString().Split('/');

            if (arrFeIni.Length == 3)
            {
                Int32.TryParse(arrFeIni[2] + arrFeIni[1] + arrFeIni[0], out intFeIni);
            }

            if (arrFeFin.Length == 3)
            {
                Int32.TryParse(arrFeFin[2] + arrFeFin[1] + arrFeFin[0], out intFeFIn);
            }

            if (arrFeAct.Length == 3)
            {
                Int32.TryParse(arrFeAct[2] + arrFeAct[1] + arrFeAct[0], out intFeActual);
            }

            if (intFeIni > 0 && intFeActual < intFeIni)
            {
                return false;
            }

            if (intFeFIn > 0 && intFeFIn < intFeActual)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            this.Web_ErrorEvent(this, ex);
        }

        return true;
    }

    private Boolean RangoHorasValido(String horaInicio, String horaFin)
    {
        Int32 intHoraInicio = 0, intHoraFin = 0, intHoraActual;

        try
        {
            String[] arrHoraIni = horaInicio.Split(':');
            String[] arrHoraFin = horaFin.Split(':');
            intHoraActual = (DateTime.Now.Hour * 100) + DateTime.Now.Minute;

            if (arrHoraIni.Length == 2)
            {
                Int32.TryParse(arrHoraIni[0] + arrHoraIni[1], out intHoraInicio);
            }

            if (arrHoraFin.Length == 2)
            {
                Int32.TryParse(arrHoraFin[0] + arrHoraFin[1], out intHoraFin);
            }

            if (intHoraInicio > 0 && intHoraActual < intHoraInicio)
            {
                return false;
            }

            if (intHoraFin > 0 && intHoraFin < intHoraActual)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            this.Web_ErrorEvent(this, ex);
        }

        return true;
    }

    #endregion

    #region "Menu"

    private void GetHijosMenuHorizontal(ref MenuItem oMenuItemPadre, int nivel, OpcionSeguridadBEList oOpcionSeguridadBEList)
    {
        MenuItem oMenuItemHijo;
        OpcionSeguridadBE oSubOpSeg;
        OpcionSeguridadBEList oOpcionSeguridadBEListHijos;
        for (int i = 0; i < oOpcionSeguridadBEList.Count; i++)
        {
            oSubOpSeg = oOpcionSeguridadBEList[i];
            if (oSubOpSeg.CSTRUCT.Length == (nivel * 2) &&//Solo los item de la raiz
                oSubOpSeg.fl_ind_visible.Trim().Equals("1") &&
                oOpcionSeguridadBEList.DebeVerOpcion(oSubOpSeg.CSTRUCT))
            {
                CreaOpcionHorizontal(out oMenuItemHijo, oSubOpSeg);
                if (oSubOpSeg.fl_ind_ver_hijos.Trim().Equals("1"))
                {
                    oOpcionSeguridadBEListHijos = new OpcionSeguridadBEList();
                    for (int j = i; j < oOpcionSeguridadBEList.Count; j++)
                    {
                        if (oOpcionSeguridadBEList[j].CSTRUCT.IndexOf(oSubOpSeg.CSTRUCT) == 0)
                            oOpcionSeguridadBEListHijos.Add(oOpcionSeguridadBEList[j]);
                    }
                    GetHijosMenuHorizontal(ref oMenuItemHijo, nivel + 1, oOpcionSeguridadBEListHijos);
                }
                oMenuItemPadre.ChildItems.Add(oMenuItemHijo);
            }
        }
    }

    private void CreaOpcionHorizontal(out MenuItem oMenuItem, OpcionSeguridadBE oOpSeguridad)
    {
        oMenuItem = new MenuItem();
        int alto, ancho;
        GetDimensionesImagen(oOpSeguridad.NO_IMG_OFF, out ancho, out alto);

        if (!oOpSeguridad.NO_IMG_OFF.Trim().Equals(String.Empty) && ancho > 0 && alto > 0)
        {
            //Opcion de tipo imagen
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htmWriter = new HtmlTextWriter(sw);
            Image imgOpcion = new Image();
            imgOpcion.BorderWidth = 0;
            imgOpcion.ImageUrl = String.Format("../{0}", oOpSeguridad.NO_IMG_OFF);
            if (!oOpSeguridad.NO_IMG_ON.Trim().Equals(String.Empty))
            {
                imgOpcion.Attributes["onmouseover"] = String.Format("javascript: this.src='../{0}';", oOpSeguridad.NO_IMG_ON);
                imgOpcion.Attributes["onmouseout"] = String.Format("javascript: this.src='../{0}';", oOpSeguridad.NO_IMG_OFF);
            }
            imgOpcion.Width = ancho;
            imgOpcion.Height = alto; //Unit.Pixel(24);
            imgOpcion.Style["cursor"] = "pointer";

            imgOpcion.RenderControl(htmWriter);
            oMenuItem.Text = sw.ToString();
            //oMenuItem.ImageUrl = String.Format("~/{0}", oOpSeguridad.NO_IMG_OFF);
        }
        else
        { //Opcion de tipo texto
            oMenuItem.Text = oOpSeguridad.VDEMEN;
        }

        if (!oOpSeguridad.NO_URL_WEB.Trim().Equals(String.Empty))
        {
            oMenuItem.NavigateUrl = String.Format("{0}?fc=1", oOpSeguridad.NO_URL_WEB); ;
        }
        else
        {
            oMenuItem.Selectable = false;
        }
    }

    public void CreaOpcionVertical(out Panel oMenuItem, OpcionSeguridadBE oOpSeguridad, int nivelEstilo)
    {
        oMenuItem = new Panel();
        HyperLink link = new HyperLink();
        int alto, ancho;
        GetDimensionesImagen(oOpSeguridad.NO_IMG_OFF, out ancho, out alto);

        if (!oOpSeguridad.NO_IMG_OFF.Trim().Equals(String.Empty) && ancho > 0 && alto > 0)
        { //Opcion de tipo imagen
            Image imgOpcion = new Image
            {
                BorderWidth = 0,
                ImageUrl = String.Format("../{0}", oOpSeguridad.NO_IMG_OFF)
            };
            if (!oOpSeguridad.NO_IMG_ON.Trim().Equals(String.Empty))
            {
                imgOpcion.Attributes["onmouseover"] = String.Format("javascript: this.src='../{0}';", oOpSeguridad.NO_IMG_ON);
                imgOpcion.Attributes["onmouseout"] = String.Format("javascript: this.src='../{0}';", oOpSeguridad.NO_IMG_OFF);
            }
            imgOpcion.Width = ancho;
            imgOpcion.Height = alto;
            imgOpcion.Style["cursor"] = "pointer";

            link.Controls.Add(imgOpcion);
        }
        else
        {

            //Viñeta
            Image imgVineta = new Image
            {
                ImageUrl = "Images/Menu_Izquierda/mboff.gif"
            };
            link.Controls.Add(imgVineta);
            //Texto
            Label txtOpcion = new Label
            {
                Text = oOpSeguridad.VDEMEN
            };
            link.Controls.Add(txtOpcion);
        }

        if (nivelEstilo > 3) nivelEstilo = 3;
        oMenuItem.CssClass = String.Format("clsMenuNivel{0}Ver", nivelEstilo);

        if (!oOpSeguridad.NO_URL_WEB.Trim().Equals(String.Empty))
        {
            link.NavigateUrl = String.Format("{0}?fc=1", oOpSeguridad.NO_URL_WEB);
        }
        /*Cargamos Estilo*/
        oMenuItem.Attributes.Add("onmouseover", String.Format("javascript:this.className='clsMenuNivel{0}VerHover'", nivelEstilo));
        oMenuItem.Attributes.Add("onmouseout", String.Format("javascript:this.className='clsMenuNivel{0}Ver'", nivelEstilo));
        oMenuItem.ID = oOpSeguridad.CSTRUCT;
        oMenuItem.Controls.Add(link);
    }

    protected void GetDimensionesImagen(String nombreImagen, out int anchoImg, out int altoImg)
    {
        anchoImg = 0;
        altoImg = 0;

        try
        {
            if (nombreImagen.Trim() == String.Empty) return;
            nombreImagen = nombreImagen.Replace("/", "\\");
            String rutaImagen = String.Format("{0}\\{1}", Request.ServerVariables["APPL_PHYSICAL_PATH"], nombreImagen);
            float ancho = 0, alto = 0;

            System.Drawing.Image srcImage = System.Drawing.Image.FromFile(rutaImagen);
            ancho = srcImage.PhysicalDimension.Width;
            alto = srcImage.PhysicalDimension.Height;

            anchoImg = Convert.ToInt32(Math.Round(Convert.ToDecimal(ancho), 0));
            altoImg = Convert.ToInt32(Math.Round(Convert.ToDecimal(alto), 0));
        }
        catch
        {
            anchoImg = 0;
            altoImg = 0;
        }
    }

    //public void GetHijosMenuVertical(out Panel oMenuContPadre, int nivel, int nivelEstilo, OpcionSeguridadBEList oOpcionSeguridadBEList, out String codigosDetalle)
    public void GetHijosMenuVertical(out Panel oMenuContPadre, int nivel, int nivelEstilo, OpcionSeguridadBEList oOpcionSeguridadBEList)
    {
        oMenuContPadre = null;
        Panel oMenuItemHijo, oMenuContHijo;
        OpcionSeguridadBE oSubOpSeg;
        OpcionSeguridadBEList oOpcionSeguridadBEListHijos;

        //String codigosDetalleHijo;
        //codigosDetalle = String.Empty;
        for (int i = 0; i < oOpcionSeguridadBEList.Count; i++)
        {
            oSubOpSeg = oOpcionSeguridadBEList[i];
            if (oSubOpSeg.CSTRUCT.Length == (nivel * 2) &&//Solo los item de la raiz
                oSubOpSeg.fl_ind_visible.Trim().Equals("1") &&
                oOpcionSeguridadBEList.DebeVerOpcion(oSubOpSeg.CSTRUCT))
            {
                /*Cargamos Opcion Hijo*/
                CreaOpcionVertical(out oMenuItemHijo, oSubOpSeg, nivelEstilo);
                oMenuContHijo = null;
                //codigosDetalle = String.Format("{0}{1}|", codigosDetalle, oMenuItemHijo.ID);

                if (oSubOpSeg.fl_ind_ver_hijos.Trim().Equals("1"))
                {
                    oOpcionSeguridadBEListHijos = new OpcionSeguridadBEList();
                    for (int j = i; j < oOpcionSeguridadBEList.Count; j++)
                    {
                        if (oOpcionSeguridadBEList[j].CSTRUCT.IndexOf(oSubOpSeg.CSTRUCT) == 0)
                            oOpcionSeguridadBEListHijos.Add(oOpcionSeguridadBEList[j]);
                    }
                    GetHijosMenuVertical(out oMenuContHijo, nivel + 1, nivelEstilo + 1, oOpcionSeguridadBEListHijos);
                    //GetHijosMenuVertical(out oMenuContHijo, nivel + 1, nivelEstilo + 1, oOpcionSeguridadBEListHijos, out codigosDetalleHijo);
                    //codigosDetalle = String.Format("{0}{1}|", codigosDetalle, codigosDetalleHijo);
                }
                if (oMenuContPadre == null) oMenuContPadre = new Panel();
                oMenuContPadre.Controls.Add(oMenuItemHijo);

                if (oMenuContHijo != null)
                {
                    oMenuContPadre.Controls.Add(oMenuContHijo);
                }
            }
        }
    }
    #endregion

    #region "Control de Acceso a Páginas"

    /**
     * codEstructPagina: Estructura de pagína.
     * Retorno:
     *  '' => No tiene acceso a esta opción.
     *  '0' => Acceso de Solo Lectura.
     *  '1' => Acceso de Lectura y Escritura.
     **/
    public String ValidaTipoAccesoPagina(Page sender, String codEstructPagina)
    {
        String retorno = String.Empty;
        OpcionSeguridadBEList opciones = null;
        OpcionSeguridadBE opcion = null;

        try
        {
            opciones = Profile.Opciones;

            if (opciones != null)
            {
                opcion = opciones.Find(delegate(OpcionSeguridadBE p) { return p.CSTRUCT.Trim().Equals(codEstructPagina); });
                if (opcion != null)
                {
                    if (opcion.IND_REL.Trim().Equals("A")) retorno = CONSTANTE_SEGURIDAD.AccesoEdicion;
                    else if (opcion.IND_REL.Trim().Equals("C")) retorno = CONSTANTE_SEGURIDAD.AccesoConsulta;
                    else
                    {
                        JavaScriptHelper.Alert(sender, Message.keyAccesoPaginaDenegado, String.Empty);
                        Response.Redirect("../Inicio/Default.aspx", false);
                        Response.End();
                    }
                }
                else
                {
                    JavaScriptHelper.Alert(sender, Message.keyAccesoPaginaDenegado, String.Empty);
                    Response.Redirect("../Inicio/Default.aspx", false);
                    Response.End();
                }
            }
            else
            {
                btnCerrarSesion_OnClick(null, null);
            }
        }
        catch (Exception ex)
        {
            Web_ErrorEvent(this, ex);
        }
        return retorno;
    }

    /**
     * codEstructPagina: Estructura de pagína.
     * Retorno:
     *  '' => No tiene acceso a esta opción.
     *  '0' => Acceso de Solo Lectura.
     *  '1' => Acceso de Lectura y Escritura.
     **/
    public String ValidaAccesoOpcion(String codEstructPagina)
    {
        String retorno = String.Empty;
        OpcionSeguridadBEList opciones = null;
        OpcionSeguridadBE opcion = null;

        try
        {
            opciones = Profile.Opciones;

            if (opciones != null)
            {
                opcion = opciones.Find(delegate(OpcionSeguridadBE p) { return p.CSTRUCT.Trim().Equals(codEstructPagina); });
                if (opcion != null)
                {
                    if (opcion.IND_REL.Trim().Equals("A")) retorno = CONSTANTE_SEGURIDAD.AccesoEdicion;
                    else if (opcion.IND_REL.Trim().Equals("C")) retorno = CONSTANTE_SEGURIDAD.AccesoConsulta;
                }
            }
        }
        catch (Exception ex)
        {
            this.Web_ErrorEvent(this, ex);
        }
        return retorno;
    }
    #endregion
    #region "Menu Bostrap"
    String ok2 = "<ul>";
    private void MenuResponsive()
    {
        OpcionSeguridadBEList oOpcionSeguridadBEList = new OpcionSeguridadBEList();
        OpcionSeguridadBEList oOpcionSeguridadBEListHijos;
        OpcionSeguridadBE oOpSeguridad;
        oOpcionSeguridadBEList = Profile.Opciones;
        String ok = "", ok3 = "";

        for (int i = 0; i < oOpcionSeguridadBEList.Count; i++)
        {
            oOpSeguridad = oOpcionSeguridadBEList[i];
            if (oOpSeguridad.CSTRUCT.Length == 2 &&//Solo los item de la raiz
                oOpSeguridad.fl_ind_visible.Trim().Equals("1") &&
                oOpcionSeguridadBEList.DebeVerOpcion(oOpSeguridad.CSTRUCT))
            {
                ok += "<li>";
                ok += CreaOpcionHorizontalResponsive1(oOpSeguridad);
                if (oOpSeguridad.fl_ind_ver_hijos.Trim().Equals("1"))
                {
                    ok += "<ul class='dropdown-menu'>";
                    ok2 = "";
                    oOpcionSeguridadBEListHijos = new OpcionSeguridadBEList();
                    for (int j = i; j < oOpcionSeguridadBEList.Count; j++)
                    {
                        if (oOpcionSeguridadBEList[j].CSTRUCT.IndexOf(oOpSeguridad.CSTRUCT) == 0)
                            oOpcionSeguridadBEListHijos.Add(oOpcionSeguridadBEList[j]);
                    }
                    ok3 = MenuResponsiveHijos(2, oOpcionSeguridadBEListHijos);

                }

                ok = ok + ok3 + "</ul></li>";
            }
        }
        ok = ok.Replace("<ul class='dropdown-menu'></ul>", "");
        this.id_menu.InnerHtml = ok;

    }
    private String MenuResponsiveHijos(int nivel, OpcionSeguridadBEList oOpcionSeguridadBEList)
    {
        OpcionSeguridadBE oSubOpSeg;
        OpcionSeguridadBEList oOpcionSeguridadBEListHijos;


        for (int i = 0; i < oOpcionSeguridadBEList.Count; i++)
        {
            oSubOpSeg = oOpcionSeguridadBEList[i];
            if (oSubOpSeg.CSTRUCT.Length == (nivel * 2) &&//Solo los item de la raiz
                oSubOpSeg.fl_ind_visible.Trim().Equals("1") &&
                oOpcionSeguridadBEList.DebeVerOpcion(oSubOpSeg.CSTRUCT))
            {
                if (!String.IsNullOrEmpty(oSubOpSeg.NO_URL_WEB))
                {
                    ok2 += "<li>";

                }
                else
                {
                    ok2 += "<li>";
                }
                ok2 += CreaOpcionHorizontalResponsive(oSubOpSeg);
                if (oSubOpSeg.fl_ind_ver_hijos.Trim().Equals("1"))
                {
                    oOpcionSeguridadBEListHijos = new OpcionSeguridadBEList();
                    for (int j = i; j < oOpcionSeguridadBEList.Count; j++)
                    {
                        if (oOpcionSeguridadBEList[j].CSTRUCT.IndexOf(oSubOpSeg.CSTRUCT) == 0)
                            oOpcionSeguridadBEListHijos.Add(oOpcionSeguridadBEList[j]);
                    }
                    if (oOpcionSeguridadBEListHijos.Count > 1)
                    {
                        ok2 += "<ul class='dropdown-menu'>";
                    }
                    MenuResponsiveHijos(nivel + 1, oOpcionSeguridadBEListHijos);

                    if (oOpcionSeguridadBEListHijos.Count > 1)
                    {
                        ok2 += "</ul>";
                    }

                }
                ok2 += "</li>";
            }
        }
        return ok2;
    }
    private String CreaOpcionHorizontalResponsive(OpcionSeguridadBE oOpSeguridad)
    {
        String oMenuItem;
        if (!String.IsNullOrEmpty(oOpSeguridad.NO_URL_WEB))
        {
            String URL_Page = Page.ResolveUrl("~/" + oOpSeguridad.NO_URL_WEB);
            oMenuItem = String.Format("<a href='{0}?fc=1'><span>{1}</span></a>", URL_Page, oOpSeguridad.VDEMEN);
        }
        else
        {
            oMenuItem = String.Format("<a href='#'>{0}&nbsp;&nbsp;&nbsp;<span class='caret-right'></span></a>", oOpSeguridad.VDEMEN);
        }
        return oMenuItem;
    }
    private String CreaOpcionHorizontalResponsive1(OpcionSeguridadBE oOpSeguridad)
    {
        String oMenuItem = "";
        if (!String.IsNullOrEmpty(oOpSeguridad.NO_URL_WEB))
        {
            String URL_Page = Page.ResolveUrl("~/" + oOpSeguridad.NO_URL_WEB);
            oMenuItem = String.Format("<a href='{0}?fc=1' data-toggle='dropdown' class='dropdown-toggle'><span>{1}</span><span class='caret'></span></a>", URL_Page, oOpSeguridad.VDEMEN);
        }
        else
        {
            oMenuItem = String.Format("<a href='#' data-toggle='dropdown' class='dropdown-toggle'><span>{0}</span><span class='caret'></span></a>", oOpSeguridad.VDEMEN);
        }
        return oMenuItem;
    }
    #endregion

    #region "Excepciones"
    public void Transaction_ErrorEvent(object sender, Exception ex)
    {
        TransactionFailureEvent input = new TransactionFailureEvent(sender, Profile.Usuario.CUSR_ID, ex.Message);
        input.Raise();
        onError = true;
    }
    public void Web_ErrorEvent(object sender, Exception ex)
    {
        WebFailureEvent input = new WebFailureEvent(sender, Profile.Usuario.CUSR_ID, ex.Message);
        input.Raise();
        onError = true;
    }

    #endregion
}