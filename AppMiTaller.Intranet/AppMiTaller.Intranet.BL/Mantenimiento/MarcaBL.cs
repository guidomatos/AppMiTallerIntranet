using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA.Mantenimiento;

namespace AppMiTaller.Intranet.BL
{
    public class MarcaBL : BaseBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public MarcaBEList GetAll(String nomMarca, String codEstado)
        {
            MarcaDA oMarcaDA = new MarcaDA();
            try
            {
                return oMarcaDA.GetAll(nomMarca, codEstado);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }
        public MarcaBE GetById(Int32 id)
        {
            MarcaDA oMarcaDA = new MarcaDA();
            try
            {
                return oMarcaDA.GetById(id);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }  
        public Int32 Grabar(MarcaBE oMarcaBE)
        {
            MarcaDA oMarcaDA = new MarcaDA();
            try
            {
                if (oMarcaBE.nid_marca == 0)
                {
                    return oMarcaDA.Insertar(oMarcaBE);
                }
                else
                {
                    return oMarcaDA.Modificar(oMarcaBE);
                }
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return 0;
        }
        public Int32 Eliminar(MarcaBE oMarcaBE)
        {
            MarcaDA oMarcaDA = new MarcaDA();
            try
            {
                return oMarcaDA.Eliminar(oMarcaBE);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return 0;
        }
        public MarcaBEList GetListaMarca()
        {
            MarcaDA oMarcaDA = new MarcaDA();
            try
            {
                return oMarcaDA.GetListaMarca();
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }
    }
}

