using System.Collections.Generic;
using System.Collections.ObjectModel;
using ContactosModel.Model;
using MvvmLibrary.Factorias;
using RedContactos.Service;

namespace RedContactos.ViewModel.Mensajes
{
    public class MisMensajesViewModel:GeneralViewModel
    {
        private ObservableCollection<MensajeModel> _mensajes;
        public ObservableCollection<MensajeModel> Mensajes
        {
            get { return _mensajes; }
            set { SetProperty(ref  _mensajes ,value); }
        }

        public MensajeModel MensajeSeleccionado
        {
            get { return _mensajeSeleccionado; }
            set
            {
                if(value!=null)
                    VerDetalleMensaje();

                SetProperty(ref _mensajeSeleccionado, value);
            }
        }

        private MensajeModel _mensajeSeleccionado;


        public MisMensajesViewModel(INavigator navigator, IServicioMovil servicio) : base(navigator, servicio)
        {
        }

        private async void VerDetalleMensaje()
        {
            if (_mensajeSeleccionado != null)
            {
                if (!_mensajeSeleccionado.leido)
                {
                    _mensajeSeleccionado.leido = true;
                    await _servicio.UpdateMensaje(_mensajeSeleccionado);
                }

                await _navigator.PushAsync<DetalleMensajeViewModel>(viewModel =>
                {
                    viewModel.Mensaje = _mensajeSeleccionado;
                    viewModel.Titulo = _mensajeSeleccionado.asunto;
                });
                MensajeSeleccionado = null;
            }
        }
       
    }
}