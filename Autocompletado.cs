//Paula Sierra Luque
//Cynthia Tristán Álvarez
using System.Text.RegularExpressions;
using Listas;
using System.Windows;

internal class AutocompletadoComandos
//Autocompletado de comandos: en los prompts textuales es útil tener una opción 
//de autocompletado con el tabulador: 
//se teclean uno o varios caracteres y con el tabulador se buscan comandos
//con ese prefijo; si el comando es único (no hay ambigüedad) se completa, si hay varios se dan
//las opciones.
{
    public string BuscarComandos(string prefijo)
    //al pulsar tab, se lee el string y miramos si coincide con algún comando
    {
        string retorno = "Write Help to show available commands";
        string[] comandos = {"Help", "Inventory", "Look", "Items", "Take", "Drop"};

        for(int i = 0; i < comandos.Length; i++)
        {
            if (prefijo.StartsWith(comandos[i]))
            {
                retorno = comandos[i];
            }
            else retorno = "No commands starting with" + prefijo
                        + ". Write Help to show available commands";
        }
        return retorno;
    }
  }