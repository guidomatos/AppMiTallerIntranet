using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

public partial class SGS_UserControl_UploadFile : System.Web.UI.UserControl
{
    public String CssClass
    {
        get { return this.fuArchivo.CssClass; }
        set { this.fuArchivo.CssClass = value; }
    }

    String ext = String.Empty;
    private String _Extension = ".jpg .gif .swf .png";
    private String _Error = "";
    private Boolean _blFileServer = false;

    private String FILE_SERVER = ConfigurationManager.AppSettings["TPWEBFileServer"].ToString();
    

    public Unit WithTD1
    {
        set
        {
            //this._WithTD1 = value; 
            ViewState["_WithTD1"] = value;
        }
        get
        {
            return (Unit)ViewState["_WithTD1"];
        }
    }
    public Unit WithTD2
    {
        set
        {
            //this._WithTD2 = value; 
            ViewState["_WithTD2"] = value;
        }
        get
        {
            return (Unit)ViewState["_WithTD2"];
        }
    }

    public Unit Width
    {
        set
        {
            //this.fuArchivo.Width = value; 
        }
    }

    public String xError
    {
        get { return _Error; }
    }

    public String Extension
    {
        get { return _Extension; }
        set { _Extension = value; }
    }

    public String ArchivoFileName
    {
        get { return this.fuArchivo.FileName.Trim(); }
    }

    public Int32 FileSize
    {
        get { return this.fuArchivo.FileBytes.Length; }
    }
    public void CrearArchivo(string path)
    {
        fuArchivo.SaveAs(path);
    }
    public String ArchivoActual
    {
        get { return this.lblArchivo.Text; }
        set
        {
            this.lblArchivo.Visible = false;
            this.imgVer.Visible = false;
            this.imgLimpiar.Visible = false;

            this.lblArchivo.Text = value;
            if (!this.lblArchivo.Text.Trim().Equals(""))
            {
                this.lblArchivo.Visible = true;
            }
        }
    }

    public bool Enabled
    {
        set { this.fuArchivo.Enabled = value; }
        get { return this.fuArchivo.Enabled; }
    }

    public Boolean blFileServer
    {
        get { return _blFileServer; }
        set { _blFileServer = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.imgLimpiar.Attributes.Add("onClick", String.Format("javascript: document.getElementById('{0}').innerHTML = '';document.getElementById('{1}').style.display = 'none';document.getElementById('{2}').style.display = 'none'; document.getElementById('{3}').value = '1'", this.lblArchivo.ClientID, this.imgVer.ClientID, this.imgLimpiar.ClientID, this.txhIndLimpio.ClientID));
            this.imgLimpiar.Style.Add("cursor", "pointer");

            /**/
            if (ViewState["_WithTD1"] != null)
            {
                this.td1UF.Width = ((Unit)ViewState["_WithTD1"]).ToString();
                this.fuArchivo.Width = (Unit)ViewState["_WithTD1"];
                tblUF.Width = ((Unit)ViewState["_WithTD1"]).ToString();//@001 NCP I/F
            }
            if (ViewState["_WithTD2"] != null) this.td2UF.Width = ((Unit)ViewState["_WithTD2"]).ToString();

        }
    }

    private String ObtenerNombre(String NomArchivo)
    {
        String nom = Path.GetFileNameWithoutExtension(NomArchivo);
        String ext = Path.GetExtension(NomArchivo);
        return String.Format("{0}_{1}{2}", nom, DateTime.Now.ToString("yyyyMMddhhmmss"), ext);
    }

    public String ObtenerNombreOriginal()
    {
        return Path.GetFileName(this.fuArchivo.PostedFile.FileName);
    }

    private Boolean ValidarExtension(String NomArchivo)
    {
        ext = Path.GetExtension(NomArchivo);
        ext = ext.ToLower();

        if (_Extension.IndexOf(".*") >= 0) return true;

        if (_Extension.IndexOf(ext, 0) >= 0)
        {

            return true;
        }
        else
        {
            _Error = "El archivo debe tener la siguientes extensiones: " + _Extension;
            return false;
        }
    }

    public Boolean GrabarArchivo(ref String NombreArchivo)
    {


        try
        {

            if (this.fuArchivo.PostedFile.FileName == null)
            {
                _Error = "El archivo no ha sido enviado";
                return false;
            }

            if (this.fuArchivo.PostedFile.FileName == "" && this.txhIndLimpio.Value.Trim().Equals("1"))
            {
                NombreArchivo = "";
                return true;
            }
            else if (this.fuArchivo.PostedFile.FileName == "" && !this.txhIndLimpio.Value.Trim().Equals("1"))
            {
                NombreArchivo = this.lblArchivo.Text.Trim();
                return true;
            }
            if (this.fuArchivo.PostedFile.ContentLength <= 0)
            {
                _Error = "El archivo no puede ser adjuntado";
                return false;
            }
            if (!ValidarExtension(this.fuArchivo.PostedFile.FileName)) return false;

            NombreArchivo = ObtenerNombre(this.fuArchivo.PostedFile.FileName);

            this.fuArchivo.PostedFile.SaveAs(FILE_SERVER + NombreArchivo);
            this.txhIndLimpio.Value = "0";
        }
        catch
        {
            _Error = "No se pudo adjuntar el archivo.";
            return false;
        }
        return true;
    }

    public Boolean GrabarArchivo(ref Byte[] myData, ref String NombreArchivo)
    {
        HttpPostedFile myFile;
        int nFileLen;
        try
        {

            if (this.fuArchivo.PostedFile.FileName == null)
            {
                _Error = "El archivo no ha sido enviado.";
                return false;
            }

            if (this.fuArchivo.PostedFile.FileName == "" && this.txhIndLimpio.Value.Trim().Equals("1"))
            {
                NombreArchivo = "";
                return true;
            }
            else if (this.fuArchivo.PostedFile.FileName == "" && !this.txhIndLimpio.Value.Trim().Equals("1"))
            {
                NombreArchivo = this.lblArchivo.Text.Trim();
                return true;
            }
            if (this.fuArchivo.PostedFile.ContentLength <= 0)
            {
                _Error = "El archivo no puede ser adjuntado";
                return false;
            }
            if (!ValidarExtension(this.fuArchivo.PostedFile.FileName)) return false;

            NombreArchivo = ObtenerNombre(this.fuArchivo.PostedFile.FileName);

            String ext = Path.GetExtension(NombreArchivo);
            ext.ToLower();


            myFile = this.fuArchivo.PostedFile;
            nFileLen = myFile.ContentLength;
            myData = new byte[nFileLen];

            myFile.InputStream.Read(myData, 0, nFileLen);

            this.txhIndLimpio.Value = "0";
        }
        catch
        {
            _Error = "No se pudo adjuntar el archivo.";
            return false;
        }
        return true;
    }

    public Boolean GrabarArchivoyFileServer(ref Byte[] myData
                , ref String NombreArchivo
                , String RutaArchivoFS
                , TipoAdjunto tipoAdjunto)
    {
        HttpPostedFile myFile;
        int nFileLen;
        string RutaPathFile;
        System.Drawing.Image imagen = null;
        switch (tipoAdjunto)
        {
            case TipoAdjunto.imagenMEI: this._Extension = ConstanteBE.ARCHIVOS_IMAGENES; break;
            case TipoAdjunto.imagenSpecCode: this._Extension = ConstanteBE.ARCHIVOS_IMAGENES; break;
            case TipoAdjunto.informatico: this._Extension = ConstanteBE.ARCHIVOS_INFORMATICOS; break;
            case TipoAdjunto.todos: this._Extension = ConstanteBE.ARCHIVOS_TODOS; break;
            case TipoAdjunto.soloDOC: this._Extension = ConstanteBE.ARCHIVOS_DOC; break;
            case TipoAdjunto.soloDOCyPDF: this._Extension = ConstanteBE.ARCHIVOS_DOC_PDF; break;
            case TipoAdjunto.soloDOCyPDFAndImagenes: this._Extension = ConstanteBE.ARCHIVOS_DOC_PDF_AND_IMAGENES; break;
            case TipoAdjunto.soloDOCX: this._Extension = ConstanteBE.ARCHIVOS_DOCX; break;
        }

        try
        {
            String strUlFile = String.Empty, strCadenaImg = String.Empty, strCadenaRuta = String.Empty;
            strUlFile = this.fuArchivo.PostedFile.FileName;
            strUlFile = strUlFile.Replace(" ", "").Trim();   //quito los blancos

            string[] strArrayA = strUlFile.Split('\\');
            String strNomImg = strArrayA[strArrayA.Length - 1];

            string[] strArrayS = strNomImg.Split('.');
            String strExtension = "." + strArrayS[strArrayS.Length - 1];
            for (Int32 xi = 0; xi < strArrayS.Length - 1; xi++)
            {
                strCadenaImg = strArrayS[xi] + strCadenaImg;
            }

            if (strCadenaImg.Length > 30)
            {
                strCadenaImg = strCadenaImg.Substring(0, 30);

                strNomImg = strCadenaImg + strExtension;   //Maximo en Total 30 caracteres del NombreImg con Extension.

                for (Int32 xs = 0; xs < strArrayA.Length - 2; xs++)
                {
                    if (xs == 0)
                    {
                        strCadenaRuta = strArrayA[xs];
                    }
                    else
                    {
                        strCadenaRuta = strCadenaRuta + "\\" + strArrayA[xs];
                    }
                }
                strUlFile = strCadenaRuta + "\\" + strNomImg;
            }

            //Validaciones General
            if (strUlFile == null)
            {
                _Error = "El archivo no ha sido enviado.";
                return false;
            }

            if (strUlFile == "" && this.txhIndLimpio.Value.Trim().Equals("1"))
            {
                NombreArchivo = "";
                return true;
            }
            else if (strUlFile == "" && !this.txhIndLimpio.Value.Trim().Equals("1"))
            {
                return true;
            }

            if (this.fuArchivo.PostedFile.ContentLength <= 0)
            {
                _Error = Message.keyArchivoNoPuedeSerAdj;
                return false;
            }
            //Fin Validaciones en General

            //Validaciones de Extensión
            if (!ValidarExtension(strUlFile))
            {
                _Error = "'" + _Error + "'";
                return false;
            }

            Boolean strUlFileCaracter = Regex.IsMatch(strNomImg.Trim(), "^[A-Za-z0-9-_.]+$");
            if (strUlFileCaracter == false)
            {
                _Error = Message.keyArchivoPuedeTenerExpReg;
                return false;
            }

            myFile = this.fuArchivo.PostedFile;
            nFileLen = myFile.ContentLength;
            myData = new byte[nFileLen];
            myFile.InputStream.Read(myData, 0, nFileLen);

            //Falta almacenar el nombre del archivo, para que una vez grabado en bd se elimine de file server
            if (NombreArchivo != null && !NombreArchivo.Trim().Equals(String.Empty))
            {
                ViewState["_NombreArchivo"] = NombreArchivo;
            }
            NombreArchivo = ObtenerNombre(strUlFile);
            ViewState["_NombreArchivoGenerado"] = NombreArchivo;

            if (blFileServer)
            {
                RutaPathFile = String.Concat(Convert.ToString(ConfigurationManager.AppSettings["FileServerPath"]), RutaArchivoFS);
                RutaPathFile = String.Concat(Utility.CrearCarpetaFileServer(RutaPathFile), NombreArchivo);
            }
            else
                RutaPathFile = Convert.ToString(ConfigurationManager.AppSettings["FileServerPath"]) + RutaArchivoFS + NombreArchivo;

            //Fin Validaciones de Extensión

            //Validaciones por Tipo de Adjunto
            TipoTablaDetalleBEList oTipoTablaDetalleBEList = null;
            Int32 PesoMaxImagen = 0, AnchoImagen = 0, AltoImagen = 0, PesoMaxGeneral = 0;
            TipoTablaDetalleBL oTipoTablaDetalleBL = new TipoTablaDetalleBL();
            TipoTablaDetalleBE oTipoTablaDetalle = null;
            //oTipoTablaDetalleBL.ErrorEvent += new TipoTablaDetalleBL.ErrorDelegate(General_ErrorEvent);
            String codTabla = String.Empty;

            if ((Int32)tipoAdjunto == (Int32)TipoAdjunto.imagenMEI)
                codTabla = ConstanteBE.ID_TABLA_CONF_IMG_MARCA_EMP_IMP.ToString();
            else if ((Int32)tipoAdjunto == (Int32)TipoAdjunto.imagenSpecCode)
                codTabla = ConstanteBE.ID_TABLA_CONF_IMG_SPEC_CODE.ToString();

            if (!codTabla.Equals(String.Empty))
            {
                oTipoTablaDetalleBEList = oTipoTablaDetalleBL.ListarTipoTablaDetalle(codTabla, String.Empty, String.Empty
                                        , String.Empty, String.Empty, String.Empty, String.Empty);

                oTipoTablaDetalle = oTipoTablaDetalleBEList.Find(delegate (TipoTablaDetalleBE p) { return p.Valor2.Trim().Equals(ConstanteBE.COD_TABLA_DET_MEDIDAS_ALTO); });
                if (oTipoTablaDetalle != null) AltoImagen = Convert.ToInt32(oTipoTablaDetalle.Valor3.ToString());

                oTipoTablaDetalle = oTipoTablaDetalleBEList.Find(delegate (TipoTablaDetalleBE p) { return p.Valor2.Trim().Equals(ConstanteBE.COD_TABLA_DET_MEDIDAS_ANCHO); });
                if (oTipoTablaDetalle != null) AnchoImagen = Convert.ToInt32(oTipoTablaDetalle.Valor3.ToString());

                oTipoTablaDetalle = oTipoTablaDetalleBEList.Find(delegate (TipoTablaDetalleBE p) { return p.Valor2.Trim().Equals(ConstanteBE.COD_TABLA_DET_MEDIDAS_PESO); });
                if (oTipoTablaDetalle != null) PesoMaxImagen = Convert.ToInt32(oTipoTablaDetalle.Valor3.ToString());

                if (myData.Length >= (PesoMaxImagen * ConstanteBE.ARCHIVOS_1024))
                {
                    _Error = Message.keyEncimaMaxMarcEmpImp;
                    return false;
                }

                /*Validando Alto y ancho de la imagen*/
                imagen = System.Drawing.Image.FromStream(this.fuArchivo.PostedFile.InputStream);
                if (imagen.Width > AnchoImagen || imagen.Height > AltoImagen)
                {
                    imagen = ResizeImage(imagen, AnchoImagen, AltoImagen);
                    //imagen = imagen.GetThumbnailImage(AnchoImagen, AltoImagen, null, IntPtr.Zero);
                    //_Error = Message.keyArchivoExcedioDim;
                }
            }

            oTipoTablaDetalleBEList = oTipoTablaDetalleBL.ListarTipoTablaDetalle(ConstanteBE.ID_TABLA, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            oTipoTablaDetalle = oTipoTablaDetalleBEList.Find(delegate (TipoTablaDetalleBE p) { return p.Valor2.Trim().Equals(ConstanteBE.COD_TABLA_DET_MEDIDAS_PESO); });
            if (oTipoTablaDetalle != null) PesoMaxGeneral = Convert.ToInt32(oTipoTablaDetalle.Valor3.ToString());
            if (myData.Length >= (PesoMaxGeneral * ConstanteBE.ARCHIVOS_1024))//Esto es para todos los archivos
            {
                _Error = Message.keyEncima4Megas;
                return false;
            }

            //Fin Validaciones por Tipo de Adjunto
            this.txhIndLimpio.Value = "0";
            if ((Int32)tipoAdjunto == (Int32)TipoAdjunto.imagenMEI ||
                (Int32)tipoAdjunto == (Int32)TipoAdjunto.imagenSpecCode)
            {
                switch (ext)
                {
                    case ".jpg": imagen.Save(RutaPathFile, ImageFormat.Jpeg); break;
                    case ".jpeg": imagen.Save(RutaPathFile, ImageFormat.Jpeg); break;
                    case ".png": imagen.Save(RutaPathFile, ImageFormat.Png); break;
                    default: imagen.Save(RutaPathFile, ImageFormat.Gif); break;
                }

                myData = File.ReadAllBytes(RutaPathFile);
                nFileLen = myData.Length;

                imagen.Dispose();
            }
            else
            {
                this.fuArchivo.SaveAs(RutaPathFile);
            }

        }
        catch
        {
            _Error = Message.keyArchivoNoPuedeSerAdj;
            return false;
        }
        return true;
    }

    public Boolean ActualizaFileServer(bool exitoGrabar, String RutaArchivoFS)
    {
        Boolean retorno;
        try
        {
            String NombreArchivo = String.Empty;
            //Si grabo exitosamente entonces eliminamos el archivo anterios
            if (exitoGrabar && ViewState["_NombreArchivo"] != null)
                NombreArchivo = (String)ViewState["_NombreArchivo"];

            //Si no se grabo exitosamente se elimna el archivo nuevo generado
            if (!exitoGrabar && ViewState["_NombreArchivoGenerado"] != null)
                NombreArchivo = (String)ViewState["_NombreArchivoGenerado"];

            if (!NombreArchivo.Equals(String.Empty))
            {
                String RutaPathFile = ConfigurationManager.AppSettings["FileServerPath"] + RutaArchivoFS + NombreArchivo;
                FileInfo fs = new FileInfo(RutaPathFile);
                if (fs.Exists)
                {
                    fs.Delete();
                }
            }
            ViewState["_NombreArchivo"] = null;
            ViewState["_NombreArchivoGenerado"] = null;
            retorno = true;
        }
        catch
        {
            retorno = false;
        }
        return retorno;
    }

    public String TamanoRecomendado(TipoAdjunto tipoAdjunto)
    {
        String Retorno = String.Empty;
        TipoTablaDetalleBEList oTipoTablaDetalleBEList = null;
        Int32 AnchoImagen = 0, AltoImagen = 0, PesoMaxImagen = 0;
        TipoTablaDetalleBL oTipoTablaDetalleBL = new TipoTablaDetalleBL();
        TipoTablaDetalleBE oTipoTablaDetalle = null;
        //oTipoTablaDetalleBL.ErrorEvent += new TipoTablaDetalleBL.ErrorDelegate(General_ErrorEvent);
        String codTabla = String.Empty;

        if ((Int32)tipoAdjunto == (Int32)TipoAdjunto.imagenMEI)
            codTabla = ConstanteBE.ID_TABLA_CONF_IMG_MARCA_EMP_IMP.ToString();
        else if ((Int32)tipoAdjunto == (Int32)TipoAdjunto.imagenSpecCode)
            codTabla = ConstanteBE.ID_TABLA_CONF_IMG_SPEC_CODE.ToString();

        if (!codTabla.Equals(String.Empty))
        {
            try
            {
                oTipoTablaDetalleBEList = oTipoTablaDetalleBL.ListarTipoTablaDetalle(codTabla, String.Empty, String.Empty
                                        , String.Empty, String.Empty, String.Empty, String.Empty);

                oTipoTablaDetalle = oTipoTablaDetalleBEList.Find(delegate (TipoTablaDetalleBE p) { return p.Valor2.Trim().Equals(ConstanteBE.COD_TABLA_DET_MEDIDAS_ALTO); });
                if (oTipoTablaDetalle != null) AltoImagen = Convert.ToInt32(oTipoTablaDetalle.Valor3.ToString());

                oTipoTablaDetalle = oTipoTablaDetalleBEList.Find(delegate (TipoTablaDetalleBE p) { return p.Valor2.Trim().Equals(ConstanteBE.COD_TABLA_DET_MEDIDAS_ANCHO); });
                if (oTipoTablaDetalle != null) AnchoImagen = Convert.ToInt32(oTipoTablaDetalle.Valor3.ToString());

                oTipoTablaDetalle = oTipoTablaDetalleBEList.Find(delegate (TipoTablaDetalleBE p) { return p.Valor2.Trim().Equals(ConstanteBE.COD_TABLA_DET_MEDIDAS_PESO); });
                if (oTipoTablaDetalle != null) PesoMaxImagen = Convert.ToInt32(oTipoTablaDetalle.Valor3.ToString());

                Retorno = String.Format("Se recomienda tamaño aprox. de {0}x{1} px y peso {2}Kb", AnchoImagen, AltoImagen, PesoMaxImagen);
            }
            catch
            {
                Retorno = String.Empty;
            }
        }


        return Retorno;
    }

    #region "ResizeImage"
    static System.Drawing.Image ResizeImage(System.Drawing.Image imgPhoto, int destWidth, int destHeight)
    {
        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;

        int destX = 0;
        int destY = 0;

        Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                                imgPhoto.VerticalResolution);


        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
        grPhoto.CompositingQuality = CompositingQuality.HighQuality;
        grPhoto.DrawImage(imgPhoto,
            new Rectangle(destX, destY, destWidth, destHeight),
            new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            GraphicsUnit.Pixel);
        grPhoto.Dispose();
        return bmPhoto;
    }
    #endregion
}