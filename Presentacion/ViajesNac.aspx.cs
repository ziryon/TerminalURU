﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EntidadesCompartidas;
using Logica;


namespace Presentacion
{
    public partial class ViajesNac : System.Web.UI.Page
    {
        void Limpiar()
        {
            txtCompania.Text = "";
            txtEmpleado.Text = "";
            txtFechaPartida.Text = "";
            txtFechaArribo.Text = "";
            txtTerminal.Text = "";
            txtCantidadAsientos.Text = "";
            ddParadas.SelectedValue = Convert.ToString(0);
            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }
        bool ControlVacio()
        {
            var vacio = false;
            if (String.IsNullOrEmpty(txtCompania.Text) || String.IsNullOrEmpty(txtEmpleado.Text) || String.IsNullOrEmpty(txtFechaPartida.Text) || String.IsNullOrEmpty(txtFechaArribo.Text) || String.IsNullOrEmpty(ddParadas.SelectedValue) || String.IsNullOrEmpty(txtTerminal.Text) || String.IsNullOrEmpty(txtCantidadAsientos.Text))
                vacio = true;
            return vacio;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtNum.Text))
                    throw new Exception("Debe completar el campo de busqueda");
                Session["ViajesNacionales"] = FabricaLogica.GetLogicaViajes().BuscarViaje(Convert.ToInt32(txtNum.Text.Trim()));


                if ((ViajesNac)Session["ViajesNacionales"] != null)
                {
                    
                    txtCompania.Text = ((ViajesNacionales)Session["ViajesNacional"])._Com._Nombre;
                     txtTerminal.Text = ((ViajesNacionales)Session["ViajesNacional"])._Ter._Codigo;
                     txtFechaPartida.Text = ((ViajesNacionales)Session["ViajesNacional"])._FechaPartida.ToString();
                     txtFechaArribo.Text = ((ViajesNacionales)Session["ViajesNacional"])._FechaArribo.ToString();
                     txtCantidadAsientos.Text = ((ViajesNacionales)Session["ViajesNacional"])._CantidadAsientos.ToString();
                     txtEmpleado.Text = ((ViajesNacionales)Session["ViajesNacional"])._Emp._Cedula;
                     ddParadas.Text =Convert.ToString(((ViajesNacionales)Session["ViajesNacional"])._ParadasIntermedias);
                     btnEliminar.Enabled = true;
                     btnModificar.Enabled = true;
                }
                else
                {
                    Limpiar();
                    btnAgregar.Enabled = true;
                    lblMensaje.Text = "No existe el viaje internacional, si desea puede agregarlo.";
                }
            }
            catch (Exception ex)
            { lblMensaje.Text = ex.Message; }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ControlVacio())
                    throw new Exception("Debe completar todos los campos");
                Compania compania = Logica.FabricaLogica.GetLogicaCompania().Buscar(txtCompania.Text);
                if (compania == null)
                    throw new Exception("La compañia ingresada no se encuentra registrada");
                Empleado empleado = Logica.FabricaLogica.GetLogicaEmpleado().Buscar(txtEmpleado.Text);
                if (empleado == null)
                    throw new Exception("El empleado ingresado no se encuentra registrado");
                Terminal terminal = Logica.FabricaLogica.GetLogicaTerminales().Buscar(txtTerminal.Text);
                if (terminal == null)
                    throw new Exception("La terminal ingresada no se encuentra registrada");
                ViajesNacionales ViajeNac = new ViajesNacionales(Convert.ToInt32(txtNum.Text), compania, terminal, Convert.ToDateTime(txtFechaPartida.Text), Convert.ToDateTime(txtFechaArribo.Text), Convert.ToInt32(txtCantidadAsientos.Text), empleado,Convert.ToInt32(ddParadas.SelectedValue));
                Limpiar();
                
                Logica.FabricaLogica.GetLogicaViajes().Agregar(ViajeNac);
            }
            catch (Exception ex)
            { lblMensaje.Text = ex.Message; }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ControlVacio())
                    throw new Exception("Debe completar todos los campos");
                ((ViajesNacionales)Session["ViajesNacionales"])._Com = Logica.FabricaLogica.GetLogicaCompania().Buscar(txtCompania.Text);
                ((ViajesNacionales)Session["ViajesNacionales"])._Emp = Logica.FabricaLogica.GetLogicaEmpleado().Buscar(txtEmpleado.Text);
                ((ViajesNacionales)Session["ViajesNacionales"])._Ter = Logica.FabricaLogica.GetLogicaTerminales().Buscar(txtTerminal.Text);
                ((ViajesNacionales)Session["ViajesNacionales"])._FechaPartida = Convert.ToDateTime(txtFechaPartida.Text);
                ((ViajesNacionales)Session["ViajesNacionales"])._FechaArribo = Convert.ToDateTime(txtFechaArribo.Text);
                ((ViajesNacionales)Session["ViajesNacionales"])._CantidadAsientos = Convert.ToInt32(txtCantidadAsientos.Text);
                ((ViajesNacionales)Session["ViajesNacionales"])._ParadasIntermedias = Convert.ToInt32(ddParadas.SelectedValue);
                Limpiar();
                Logica.FabricaLogica.GetLogicaViajes().Modificar((ViajesNacionales)Session["ViajesNacionales"]);
                
            }
            catch (Exception ex)
            { lblMensaje.Text = ex.Message; }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Limpiar();
                ViajesNacionales viaje = (ViajesNacionales)Session["ViajesNacionales"];
                FabricaLogica.GetLogicaViajes().Eliminar(viaje);


            }
            catch (Exception ex)
            { lblMensaje.Text = ex.Message; }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}