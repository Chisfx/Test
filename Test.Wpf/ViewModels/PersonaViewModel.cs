using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Test.Domain.DTOs;
using Test.Wpf.Commands;
using Test.Wpf.Helpers;

namespace Test.Wpf.ViewModels
{
    internal class PersonaViewModel : ViewModelBase
    {
        private ICommand _saveCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private ICommand _resetCommand;
        private ObservableCollection<PersonaModel> _personas;
        private PersonaModel _persona;
        private readonly Api api;

        public PersonaViewModel()
        {
            api = new Api();
            _persona = new PersonaModel();
            GetAll();
        }

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(param => SaveData(), null);
                }
                return _saveCommand;
            }
        }
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(param => EditData((int)param), null);
                }
                return _editCommand;
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(param => DeleteData((int)param), null);
                }
                return _deleteCommand;
            }
        }
        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                {
                    _resetCommand = new RelayCommand(param => ResetData(), null);
                }
                return _resetCommand;
            }
        }
        #endregion

        public void SaveData()
        {
            if (Persona != null)
            {
                try
                {
                    api.PostPersona(Persona);
                    if (Persona.Id <= 0)
                    {
                        MessageBox.Show("Nuevo registro guardado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("Registro actualizado exitosamente.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Se produjo un error al guardar: {ex.Message}");
                }
                finally
                {
                    GetAll();
                    ResetData();
                }
            }
        }
        public void EditData(int id)
        {
            try
            {
                Persona = api.GetPersona(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error al seleccionar: {ex.Message}");
            }
        }
        public void DeleteData(int id)
        {
            if (MessageBox.Show("¿Confirmar la eliminación de este registro?", "Persona", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                try
                {
                    var aa = id;
                    api.DeletePersona(id);
                    MessageBox.Show("Registro eliminado exitosamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Se produjo un error al eliminar: {ex.Message}");
                }
            }
        }
        public void ResetData()
        {
            Persona = new PersonaModel();
        }
        private void GetAll()
        {
            Personas = api.GetPersonas();
        }

        public PersonaModel Persona
        {
            get => _persona;
            set
            {
                if (_persona != value)
                {
                    _persona = value;
                    OnPropertyChanged(nameof(Persona));
                }
            }
        }
        public ObservableCollection<PersonaModel> Personas
        {
            get => _personas;
            set
            {
                if (_personas != value)
                {
                    _personas = value;
                    OnPropertyChanged(nameof(Personas));
                }
            }
        }
    }
}
