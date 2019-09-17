using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA.Mantenimiento;

namespace AppMiTaller.Intranet.BL
{
    public class ModeloBL : BaseBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public ModeloBE GetById(Int32 idModelo)
        {
            ModeloDA oModeloDA = new ModeloDA();
            try
            {
                return oModeloDA.GetById(idModelo);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }
                
        public Int32 Grabar(ModeloBE oModeloBE)
        {
            ModeloDA oModeloDA = new ModeloDA();
            try
            {
                if (oModeloBE.nid_modelo == 0)
                {
                    return oModeloDA.Insertar(oModeloBE);
                }
                else
                {
                    return oModeloDA.Modificar(oModeloBE);
                }
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return 0;
        }
        public Int32 Eliminar(ModeloBE oModeloBE)
        {
            ModeloDA oModeloDA = new ModeloDA();
            try
            {
                return oModeloDA.Eliminar(oModeloBE);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return 0;
        }

        public ModeloBEList GetAllBandeja(Int32 idMarca, String codModelo, String nomModelo, String codEstado, Int32 idUsuario)
        {
            ModeloDA oModeloDA = new ModeloDA();
            try
            {
                return oModeloDA.GetAllBandejas(idMarca, codModelo, nomModelo, codEstado, idUsuario);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }

        public ModeloBEList GETListarModelos(ModeloBE ent)
        {
            try
            {
                return new ModeloDA().GETListarModelos(ent);
            }
            catch (Exception ex)
            {
                this.ErrorEvent(this, ex);
                return null;
            }

        }
        public CombosBEList GETListarFamiliasByNegocio(ModeloBE ent)
        {
            return new ModeloDA().GETListarFamiliasByNegocio(ent);
        }
        public CombosBEList GETListarNegocios()
        {
            return new ModeloDA().GETListarNegocios();
        }
        public CombosBEList GETListarNegocios_X_Marca(string s_nid_marca)
        {
            return new ModeloDA().GETListarNegocios_X_Marca(s_nid_marca);
        }
        public CombosBEList GETListarMarcas(int nid_usuario)
        {
            return new ModeloDA().GETListarMarcas(nid_usuario);
        }
        public string[] GETListarParamByModelo(ModeloBE ent)
        {
            return new ModeloDA().GETListarParamByModelo(ent);
        }
        public int GETInserActuParamByModelo(string[] lista)
        {
            return new ModeloDA().GETInserActuParamByModelo(lista);
        }

    }
}