using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.DTOs;

namespace Test.Wpf.Helpers
{
    internal class Api
    {
        internal ObservableCollection<PersonaModel> GetPersonas()
        {
            ObservableCollection<PersonaModel> collection;
            var result = ClientHttp.GetAll<PersonaModel>($"{Router.UrlPersona}");
            if (result == null)
            {
                collection = new ObservableCollection<PersonaModel>();
            }
            else
            {
                collection = new ObservableCollection<PersonaModel>(result);
            }
            return collection;
        }

        internal void PostPersona(PersonaModel persona)
        {
            ClientHttp.Post($"{Router.UrlPersona}", persona);
        }

        internal PersonaModel GetPersona(int id)
        {
            return ClientHttp.Get<PersonaModel>($"{Router.UrlPersona}/{id}");
        }

        internal void DeletePersona(int id)
        {
            ClientHttp.Delete($"{Router.UrlPersona}/{id}");
        }
    }
}
